using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionHouse03
{
    public class Server : IBroadcaster
    {
        private TcpListener tcpListener;
        private List<IClient> clientList;
        private int activeClients;
        private Auction auction;
        public bool Running { get; private set; }

        public Server(TcpListener tcpListener)
        {
            clientList = new List<IClient>();
            this.tcpListener = tcpListener;
            auction = new Auction(this);
        }        

        internal void Start()
        {
            Console.WriteLine("Server starting.");
            Running = true;
            tcpListener.Start();
            auction.Start();
            activeClients = 0;

            new Thread(()=> UpdateClientList()).Start();
        }

        internal void Listen()
        {
            Console.WriteLine("Server listening...");
            ConnectClient(tcpListener.AcceptSocket());            
        }

        private void ConnectClient(Socket socket)
        {
            Console.WriteLine("New Client Connected : " + socket.RemoteEndPoint);
            IClient client = new Bidder(socket);
            clientList.Add(client);

            new Thread(() => auction.PlayerJoin((Bidder)client)).Start();
        }

        public void Broadcast(string v)
        {
            try
            {
                foreach (Client client in clientList)
                {
                    client.Write(v);
                }
            }
            catch (Exception e) { }
        }

        public void BroadcastExcept(string v, IClient clientExcept)
        {
            try
            {
                foreach (IClient client in clientList)
                {
                    if (!ReferenceEquals(client, clientExcept))
                        client.Write(v);
                }
            }
            catch (Exception e) { }
        }

        void UpdateClientList()
        {
            while (Running)
            {
                IClient inactive = null;
                                
                foreach (IClient client in clientList)
                {
                    if (!client.IsActive())
                        inactive = client;
                }

                if (inactive != null)
                    clientList.Remove(inactive);

                if (activeClients != clientList.Count())
                {
                    activeClients = clientList.Count();
                    Console.WriteLine("Active Clients: " + activeClients);
                }

                if (inactive == null)
                    Thread.Sleep(5000);
            }
        }        
    }
}
