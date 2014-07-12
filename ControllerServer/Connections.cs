﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControllerServer
{
    public class Connections
    {
        private static Socket _mySocket;
        public static Socket MySocket
        {
            get
            {
                return _mySocket;
            }
            set
            {
                if (value != _mySocket)
                {
                    _mySocket = value;
                }
            }
        }

        private IPAddress _ip;
        public IPAddress IP
        {
            get
            {
                return _ip;
            }
            private set
            {
                if (value != _ip)
                {
                    _ip = value;
                }
            }
        }

        private TcpListener _listener;

        private string context = string.Empty;
        private const string FILE_BROWSER = "FILE_BROWSER";
        private const string MOUSE_CONTROL = "MOUSE_CONTROL";

        private Thread connectThread = null;

        private void SetMyIp()
        {

            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }

            _ip = IPAddress.Parse(localIP);
        }

        public static bool IsConnected()
        {
            if (_mySocket == null)
                return false;

            bool part1 = _mySocket.Poll(1000, SelectMode.SelectRead);
            bool part2 = (_mySocket.Available == 0);
            if (part1 & part2)
                return false;
            else
                return true;
        }

        public void StartConnection()
        {
            SetMyIp();
            _listener = new TcpListener(_ip, 1212);

            if (connectThread == null)
            {
                connectThread = new Thread(new ThreadStart(Connect));
                connectThread.Start();
                Console.WriteLine(connectThread.ManagedThreadId + " Thread Started(Connect)");
            }
            else
            {
                connectThread.Abort();
                connectThread = new Thread(new ThreadStart(Connect));
                connectThread.Start();
                Console.WriteLine(connectThread.ManagedThreadId + " Thread Started(Connect)");
            }
        }

        private void Connect()
        {
            try
            {
                _listener.Start();
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }


            while (true)
            {
                if (!IsConnected())
                {
                    try
                    {
                        _mySocket = _listener.AcceptSocket();

                        Thread startreceiver = new Thread(() => Receiver.StartReceiver(_mySocket));
                        startreceiver.Start();
                        Console.WriteLine(startreceiver.ManagedThreadId + " Thread Started(StartReceiver)");

                        Thread serverequest = new Thread(new ThreadStart(ServeRequest));
                        serverequest.Start();
                        Console.WriteLine(serverequest.ManagedThreadId + " Thread Started(ServeRequest)");
                    }
                    catch (Exception) { }
                }

            }
        }

        private void ServeRequest()
        {
            string requestString;

            while (true)
            {
                if (!Receiver.IsValueChanged)
                    continue;
                requestString = Receiver.Message;
                Console.WriteLine(requestString);
                if (requestString != null)
                {
                    if (requestString.Equals(FILE_BROWSER))
                    {
                        Receiver.IsValueChanged = false;
                        // create a file browser object and start.
                        Form1.SystemDetails.sendSystemDetails(ref _mySocket);
                        new FileExplorer().Start();
                    }
                    else if (requestString.Equals(MOUSE_CONTROL))
                    {
                        MessageBox.Show(requestString);
                        Receiver.IsValueChanged = false;
                        new MouseSimulator().StartSimulation();
                    }
                }
            }
        }
    }
}
