using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Checkpoint02
{
    class ReceiptGenerator : Receipt 
    {
        //The ReceiptPrinter class inherits from the Receipt class which sets the Order property
        //This class specifies the location of where the receipts will be outputted to - ReceiptsPath will need to be updated
        protected string ReceiptsPath { get; private set; } = @"C:\Users\Owner\source\repos\Checkpoint02\Checkpoint02\Receipts";

        public ReceiptGenerator(Order order) : base(order)
        {
        }

        public void WriteReceipt()
        {
            //Append the orderId to the path in order to create the file in that location with the orderID as a name
            string receiptPath = ReceiptsPath + "\\" + CustomerOrder.Id + ".txt";
            //Create the file and write the order details to it
            FileStream fs = new FileStream(receiptPath, FileMode.Create, FileAccess.Write);
            if (fs.CanWrite)
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("ID: {0}", CustomerOrder.Id);
                sw.WriteLine("---------------------------");
                sw.WriteLine("{0}", CustomerOrder.TimeOrdered.ToLongDateString());
                sw.WriteLine("---------------------------");
                foreach (var orderItem in CustomerOrder.OrderList)
                {
                    sw.WriteLine("{0}           $ {1}", orderItem.Name, Math.Round(Convert.ToDecimal(orderItem.Price), 2));
                }
                sw.WriteLine("---------------------------");
                sw.WriteLine("Subtotal          $ {0}", Math.Round(Convert.ToDecimal(CustomerOrder.SubTotal), 2));
                sw.WriteLine("TAX               $ {0}", Math.Round(Convert.ToDecimal(CustomerOrder.Tax), 2));
                sw.WriteLine("Total             $ {0}", Math.Round(Convert.ToDecimal(CustomerOrder.Total), 2));
                sw.Flush();
                sw.Close();
            }
        }
    }
}
