using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse03
{
    public class Client
    {
        private bool Active { get; set; }
        private Object _lock = new Object();
        protected Socket socket;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;

        public Client(Socket socket)
        {
            this.socket = socket;
            stream = new NetworkStream(socket);
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.AutoFlush = true;
            SetActive(true);

            writer.WriteLine("Joined the server...");
        }

        private void SetActive(bool v)
        {
            //Console.WriteLine(socket.RemoteEndPoint + " is setting active to " + v.ToString());
            lock (_lock)
            {
                Active = v;
            }
        }
        public bool IsActive()
        {
            lock (_lock)
            {
                return Active;
            }
        }

        public void Write(string v)
        {
            try { writer.WriteLine(v); }
            catch (Exception e) { Disconnect(); }
        }

        public string Read()
        {
            try { return reader.ReadLine(); }
            catch (Exception e)
            {
                Disconnect();
                return "ERROR";
            }
        }
        
        public void Disconnect()
        {
            Console.WriteLine("Client disconnecting: " + socket.RemoteEndPoint);
            writer.Close();
            reader.Close();
            stream.Close();
            SetActive(false);
        }
    }
         
}
