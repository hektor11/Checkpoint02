using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint02
{
    class Receipt
    {
        //The receipt class will contain an order
        //This is a base class for the ReceiptGenerator class
        public Order CustomerOrder { get; private set; }

        //This constructor takes in an Order
        protected Receipt(Order order)
        {
            CustomerOrder = order;
        }

    }
}
