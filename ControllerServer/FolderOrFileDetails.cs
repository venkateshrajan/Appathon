using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerServer
{
    class FolderOrFileDetails
    {
        private String[] _FolderOrFilename;
        public String[] FileOrFoldername
        {
            get
            {
                return _FolderOrFilename;
            }

            set
            {
                if (value != _FolderOrFilename)
                {
                    _FolderOrFilename = value;
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

        private String[] _FolderorFilePath;
        public String[] FileOrFolderPath
        {
            get
            {
                return _FolderorFilePath;
            }

            set
            {
                if (value != _FolderorFilePath)
                {
                    _FolderorFilePath = value;
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

    }
}
