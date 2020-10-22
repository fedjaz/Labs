using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Second
{
    public partial class MyETL : ServiceBase
    {
        FileSystemWatcher watcher;
        string targetDirectory;
        string archiveDirectory;
        Logger logger;

        public MyETL()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string source = "C:\\FileWatcher\\source";
            string target = "C:\\FileWatcher\\target";
            string logFile = "C:\\FileWatcher\\target\\log.txt";
            string variablesLoadStatus;
            try
            {
                string[] variable = Environment.GetEnvironmentVariable("FileWatcher").Split(';');
                source = variable[0];
                target = variable[1];
                logFile = variable[2];
                variablesLoadStatus = "Variavles loaded successfully";
            }
            catch(Exception ex)
            {
                variablesLoadStatus = $"Failed to load variables, using default directories - {ex}";
            }
            Config(source, target, logFile);

            logger.Log(variablesLoadStatus);
            logger.Log("Service started succsessfully");
        }

        void Config(string sourceDirectory, string targetDirectory, string logFile)
        {
            logger = new Logger(logFile, true);
            this.targetDirectory = targetDirectory;
            archiveDirectory = $"{targetDirectory}\\archive";
            try
            {
                if(!Directory.Exists(archiveDirectory))
                {
                    Directory.CreateDirectory(archiveDirectory);
                }
                if(!Directory.Exists(sourceDirectory))
                {
                    Directory.CreateDirectory(sourceDirectory);
                }
            }
            catch(Exception ex)
            {
                logger.Log($"Fatal error ocured while reaching source or target directory - {ex}");
                return;
            }

            watcher = new FileSystemWatcher(sourceDirectory);
            watcher.Filter = "*.txt";
            watcher.Created += Created;
            watcher.EnableRaisingEvents = true;
        }
        protected override void OnStop()
        {
        }

        private void Created(object sender, FileSystemEventArgs e)
        {
            FileInfo file = new FileInfo(e.FullPath);
            string encrypted;
            byte[] key = Encryption.GenerateKey(16);
            WaitUntilFileIsReady(e.FullPath);
            using(StreamReader sr = new StreamReader(e.FullPath))
            {
                encrypted = Encryption.Encrypt(sr.ReadToEnd(), key);
            }
            using(StreamWriter sw = new StreamWriter(e.FullPath))
            {
                sw.Write(encrypted);
            }

            string td = $"{targetDirectory}\\{file.LastWriteTime:yyyy\\\\MM\\\\dd}";
            string newName = $"{Path.GetFileNameWithoutExtension(e.FullPath)}_{file.LastWriteTime:yyyy_MM_dd_HH_mm_ss}";
            int i = 0;
            while(File.Exists($"{td}\\{newName}.txt"))
            {
                i++;
                newName = $"{Path.GetFileNameWithoutExtension(e.FullPath)}({i})_{file.LastWriteTime:yyyy_MM_dd_HH_mm_ss}";
            }
            if(!Directory.Exists(td))
            {
                Directory.CreateDirectory(td);
            }
            string newPath = $"{td}\\{newName}";
            try
            {
                Archive.Compress(e.FullPath, newPath + ".gz");
                Archive.Compress(e.FullPath, $"{archiveDirectory}\\{newName}.gz");
                Archive.Decompress(newPath + ".gz", newPath + ".txt");
            }
            catch(Exception ex)
            {
                logger.Log($"Fatal error ocured while compressing - {ex}");
                return;
            }
            File.Delete(e.FullPath);
            File.Delete(newPath + ".gz");
            using(StreamReader sr = new StreamReader(newPath + ".txt"))
            {
                encrypted = sr.ReadToEnd();
            }
            using(StreamWriter sw = new StreamWriter(newPath + ".txt"))
            {
                sw.Write(Encryption.Decrypt<string>(encrypted, key));
            }
            logger.Log($"File {newName} sent successfully");

        }

        private void WaitUntilFileIsReady(string path)
        {
            while(true)
            {
                try
                {
                    using(FileStream fs = File.Open(path, FileMode.Open,
                          FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        return;
                    }
                }
                catch
                {

                }
            }
        }
    }
}
