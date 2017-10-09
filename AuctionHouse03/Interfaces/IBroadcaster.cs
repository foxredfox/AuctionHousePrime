using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse03
{
    public interface IBroadcaster
    {
        void Broadcast(string v);
        void BroadcastExcept(string v, IClient clientExcept);
    }
}
