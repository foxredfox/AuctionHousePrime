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
        private Item currentItem;
        private IBroadcaster broadcaster;
        private int currentBid;

        public Auction(IBroadcaster broadcaster)
        {
            this.broadcaster = broadcaster;
        }

        public void Start()
        {
            currentItem = new Item();
            currentBid = 0;
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
            int numberResponse;
            bool r = Int32.TryParse(v, out numberResponse);

            lock (_lock)
            {
                if (r && numberResponse > currentBid)
                {
                    broadcaster.Broadcast("Current bid set to " + numberResponse);
                    currentBid = numberResponse;
                }
            }
            
        }
    }
}
