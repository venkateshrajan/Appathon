using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerServer
{
    class FolderOrFileDetails
    {
      
        private String[] _folderOrFilename;
        public String[] FolderOrFilename
        {
            get
            {
                return _folderOrFilename;
            }

            set
            {
                if (value != _folderOrFilename)
                {
                    _folderOrFilename = value;
                }
            }
        }

        private String[] _fileExtension;
        public String[] FileExtension
        {
            get
            {
                return _fileExtension;
            }

            set
            {
                if (value != _fileExtension)
                {
                    _fileExtension = value;
                }
            }
        }

        private String[] _folderorFilePath;
        public String[] FolderOrFilePath
        {
            get
            {
                return _folderorFilePath;
            }

            set
            {
                if (value != _folderorFilePath)
                {
                    _folderorFilePath = value;
                }
            }
        }

        private bool[] _isFolder;
        public bool[] IsFolder
        {
            get
            {
                return _isFolder;
            }

            set
            {
                if (value != _isFolder)
                {
                    _isFolder = value;
                }
            }
        }

        public void Start()
        {
            string message;

            while (true)
            {
                message = Receiver.Message;

                if (String.IsNullOrEmpty(message) || !Receiver.IsValueChanged || String.IsNullOrWhiteSpace(message))
                    continue;
                if (!(message.Contains(":") && message.Contains("\\")))
                    continue;

                if (message.Equals("End"))
                    break;
                else if (message.Contains(":") && message.Contains("\\"))
                {
                    message.Replace('\\', '/');
                    Receiver.IsValueChanged = false;
                    String[] allfolders = System.IO.Directory.GetDirectories(message, "*", System.IO.SearchOption.TopDirectoryOnly);
                    String[] allfiles = System.IO.Directory.GetFiles(message, "*", System.IO.SearchOption.TopDirectoryOnly);

                    _folderOrFilename = new String[allfiles.Length + allfolders.Length];
                    _folderorFilePath = new String[allfiles.Length + allfolders.Length];
                    _fileExtension = new String[allfiles.Length + allfolders.Length];
                    _isFolder = new bool[allfiles.Length + allfolders.Length];
                    
                    int i = 0;
                    foreach (String folder in allfolders)
                    {
                        _folderOrFilename[i] = Path.GetFileName(folder);
                        _fileExtension[i] = "";
                        _isFolder[i] = true;
                        _folderorFilePath[i] = folder;
                        i++;
                    }
                    
                    foreach (String file in allfiles)
                    {
                        _folderOrFilename[i] = Path.GetFileNameWithoutExtension(file);
                        _fileExtension[i] = Path.GetExtension(file);
                        _isFolder[i] = false;
                        _folderorFilePath[i] = file;
                        i++;
                    }
                    try
                    {
                        Connections.MySocket.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this)));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("FileExplorer : JsonConvert Exception");
                    }
                }
            } // end of while(true)
        }// end of Start()
        
    }
}
