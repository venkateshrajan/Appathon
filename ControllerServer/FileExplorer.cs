using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerServer
{
    class FileExplorer
    {

        public void Start()
        {
            string message;
            bool end = false;

            while(!end) {
                message = Receiver.Message;

                if (String.IsNullOrEmpty(message) || !Receiver.IsValueChanged || String.IsNullOrWhiteSpace(message))
                    continue;
                FolderOrFileDetails ffDetails = null;

                try
                {
                    ffDetails = JsonConvert.DeserializeObject<FolderOrFileDetails>(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("JSONException : " + e);
                    Console.WriteLine("JSONException : " + message);
                    continue;
                }
            }
        }
    }
}
