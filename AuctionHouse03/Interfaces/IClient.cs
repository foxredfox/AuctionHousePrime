using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse03
{
    public interface IClient
    {
        bool IsActive();
        string Read();
        void Write(string v);
        void Disconnect();
    }
}
