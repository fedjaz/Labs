﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Third
{
    static class Validator
    {
        public static string Validate(ETLOptions options)
        {
            string report = "";
            #region Sending Validation
            SendingOptions sending = options.SendingOptions;
            if(!CreateDirectoryIfNotExist(sending.SourceDirectory))
            {
                sending.SourceDirectory = "C:\\FileWatcher\\source";
                CreateDirectoryIfNotExist(sending.SourceDirectory);
                report += "Cannot open source directory, using default. ";
            }
            if(!CreateDirectoryIfNotExist(sending.TargetDirectory))
            {
                sending.TargetDirectory = "C:\\FileWatcher\\target";
                report += "Cannot open target directory, using default. ";
                CreateDirectoryIfNotExist(sending.TargetDirectory);
            }
            if(!CreateDirectoryIfNotExist(sending.ArchiveDirectory))
            {
                sending.ArchiveDirectory = "C:\\FileWatcher\\target\\archive";
                report += "Cannot open source archive, using default. ";
                CreateDirectoryIfNotExist(sending.ArchiveDirectory);
            }
            #endregion
            #region Logging Validation
            LoggingOptions logging = options.LoggingOptions;
            if(!CreateFileIfNotExist(logging.LogPath))
            {
                logging.LogPath = "C:\\FileWatcher\\target\\log.txt";
                report += "Cannot open source log file, using default. ";
                CreateFileIfNotExist(logging.LogPath);
            }
            #endregion
            #region Encryption Validation
            EncryptionOptions encryption = options.EncryptionOptions;
            if(!encryption.RandomKey && encryption.Key.Length != 16)
            {
                report += "Encryption key's length must be 16, using random key. ";

            }
            #endregion
            #region Archive Validation
            ArchiveOptions archive = options.ArchiveOptions;
            if((int)archive.CompressionLevel < 0 || (int)archive.CompressionLevel > 2)
            {
                archive.CompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
                report += "Compression level value can't be below zero and abowe 2, using default value. ";
            }
            #endregion
            return report;
        }

        public static bool CreateDirectoryIfNotExist(string path)
        {
            try
            {
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public static bool CreateFileIfNotExist(string path)
        {
            try
            {
                string name = path.Trim('\\').Split('\\').Last();
                string p = path.Substring(0, path.Length - name.Length);
                if(!Directory.Exists(p))
                {
                    Directory.CreateDirectory(p);
                }
                if(!File.Exists(path))
                {
                    File.Create(path).Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
