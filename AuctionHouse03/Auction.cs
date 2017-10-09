using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse03
{
    class Auction
    {
        private Object _lock = new object();
        private object currentItem = new object();
        private IBroadcaster broadcaster;

        public Auction(IBroadcaster broadcaster)
        {
            this.broadcaster = broadcaster;
        }

        public void Start()
        {

        }

        public void PlayerJoin(Bidder bidder)
        {
            try
            {
                bidder.Write("Please enter an alias");
                while (bidder.Name = bidder.Read() == "") { }

                bidder.Write("Welcome to the auction, please place a bid on;");
                lock (_lock) { bidder.Write(currentItem.ToString()); }            

                while (bidder.IsActive()) { ReadResponse(bidder.Read()); }
            }
            catch (Exception e) { }
        }

        private void ReadResponse(string v)
        {
            throw new NotImplementedException();
        }
    }
}
