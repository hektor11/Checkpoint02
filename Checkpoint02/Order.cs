using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint02
{
    class Order
    {
        //This class represents what an order would consist of
        //The order will have an Id which will be provided when the menu is created
        //The order will contain an OrderList which will be used to keep track of the menuItems
        //SubTotal, Tax and total properties will be used to provide the user with the order total
        //TimeOrdered will keep track of the time the order was placed
        public int Id {get; private set;}
        public List<MenuItem> OrderList { get; set; }
        public double SubTotal { get; private set; } = 0;
        public double Tax { get; private set; } = 0;
        public double Total { get; private set; } = 0;
        public DateTime TimeOrdered { get; private set; }
        public bool isEmpty { get; private set; } = true;

        //Public constructor takes in orderId which the Menu will initialize and update based on additional orders
        public Order(int orderId)
        {
            OrderList = new List<MenuItem>();
            Id = orderId;
        }

        // This method will add a MenuItem to the OrderList
        public void AddToOrder(MenuItem item)
        {
            OrderList.Add(item);
            SubTotal += item.Price;
            isEmpty = false;

        }

        // This method will remove a MenuItem from the OrderList
        public bool RemoveFromOrder(int index)
        {
            //Check if the OrderList is empty
            //if(OrderList.Count > 0)
            if(isEmpty)
            {
                return false;
            }

            //orderList is not empty
            //subtract item price from subtotal
            //remove item from OrderList
            else
            {

                SubTotal -= OrderList[index].Price;
                OrderList.RemoveAt(index);
                //Check if the list is now empty
                //and set isEmpty flag to true
                if (OrderList.Count == 0)
                {
                    isEmpty = true;
                }
                return true;

                
            }

        }

        // Once the order is finalized the TimeOrdered variable will be initialized
        public void PlaceOrder()
        {
            TimeOrdered = DateTime.Now;
        }

        // Once the order is finalized this method will calculate the total
        public void CalculateTotal()
        {
            Tax = SubTotal * .0825;
            Total = SubTotal + Tax;
        }

        // This method will list out the menuItems in the OrderList after a MenuItem is added
        public void PrintOrderItems()
        {
            for (int index = 0; index < OrderList.Count; index++)
            {
                Console.WriteLine("Item of ID {0}: {1}", index, OrderList[index].Name);
            }
        }
    }
}
