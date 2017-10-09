using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse03
{
    class Bidder : Client, IClient
    {
        public Bidder(Socket socket) : base(socket)
        {
        }

        public bool Name { get; internal set; }
    }
}
