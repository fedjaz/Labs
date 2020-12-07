using System;
using Converter;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    partial class MyETL : ServiceBase
    {
        FileSystemWatcher watcher;
        Logger logger;
        OptionsManager.OptionsManager<ETLOptions> optionsManager;
        Validator validator;
        IParser parser;

        public MyETL()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Config();

            logger.Log("Service started succsessfully");
        }

        protected override void OnStop()
        {
            logger.Log("Service stopped");
        }

        void Config()
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            validator = new Validator();
            parser = new Converter.Converter();
            optionsManager = new OptionsManager.OptionsManager<ETLOptions>(directory, parser, validator);
            ETLOptions options = optionsManager.GetOptions<ETLOptions>() as ETLOptions;
            logger = new Logger(options.LoggingOptions);
            logger.Log(optionsManager.Report);


            watcher = new FileSystemWatcher(options.SendingOptions.SourceDirectory);
            watcher.Filter = "*.txt";
            watcher.Created += Created;
            watcher.EnableRaisingEvents = true;
        }
        private void Created(object sender, FileSystemEventArgs e)
        {
            WaitUntilFileIsReady(e.FullPath);
            FileInfo file = new FileInfo(e.FullPath);
            EncryptionOptions encryptionOptions = optionsManager.GetOptions<EncryptionOptions>() as EncryptionOptions;

            Encryption.EncryptFile(e.FullPath, encryptionOptions, logger);

            string newPath = SendFile(file,
                                      optionsManager.GetOptions<SendingOptions>() as SendingOptions,
                                      logger,
                                      optionsManager.GetOptions<ArchiveOptions>() as ArchiveOptions);
            Encryption.DecryptFile(newPath, encryptionOptions, logger);

        }

        string SendFile(FileInfo file, SendingOptions sendingOptions, Logger logger, ArchiveOptions archiveOptions)
        {
            string targetDirectory = $"{sendingOptions.TargetDirectory}\\{file.LastWriteTime:yyyy\\\\MM\\\\dd}";
            validator.CreateDirectoryIfNotExist(targetDirectory);
            string newName = $"{Path.GetFileNameWithoutExtension(file.FullName)}_{file.LastWriteTime:yyyy_MM_dd_HH_mm_ss}";
            int i = 0;
            while(File.Exists($"{targetDirectory}\\{newName}.txt"))
            {
                i++;
                newName = $"{Path.GetFileNameWithoutExtension(file.FullName)}({i})_{file.LastWriteTime:yyyy_MM_dd_HH_mm_ss}";
            }

            string newPath = $"{targetDirectory}\\{newName}";

            Archive.Compress(file.FullName, newPath + ".gz", archiveOptions);
            if(sendingOptions.EnableArchiveDirectory)
            {
                validator.CreateDirectoryIfNotExist(sendingOptions.ArchiveDirectory);
                Archive.Compress(file.FullName, $"{sendingOptions.ArchiveDirectory}\\{newName}.gz", archiveOptions);
            }

            Archive.Decompress(newPath + ".gz", newPath + ".txt");
            File.Delete(file.FullName);
            File.Delete(newPath + ".gz");
            logger.Log($"File {newName} sent successfully");
            return newPath + ".txt";
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
