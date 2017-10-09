using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse03
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(new TcpListener(IPAddress.Any,20000));
            server.Start();

            while (server.Running) { server.Listen(); }

            Console.WriteLine("Server terminated");
            Console.Read();
        }
    }
}
