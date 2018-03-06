using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint02
{
    class Menu
    {
        // This class will be used to create the Menu of MenuItems from which a user can choose from
        // MenuItems will contain the MenuItem objects
        // OrderId will be used to keep track of the number of orders and will be incremented after each order
        public List<MenuItem> MenuItems { get; set; }
        public int OrderId { get; private set; }
        private string _Menu { get; set; } = "1,Cheesebuger,2.99:2,Salad,1.99:3,French Fries,.99:4,Chicken Sandwich,2.49:5,Soft Drink,.99:6,Hamburger,1.49";

        public Menu()
        {
            MenuItems = new List<MenuItem>();
            OrderId = 1;
        }

        // This method will create the menu by using the _Menu items
        public void CreateMenu()
        {
            //Loop through the _Menu string and split it by ':' then ',' to create menuItems and add them to the MenuItems list
            int tempId;
            double tempPrice;
            foreach(string item in _Menu.Split(':'))
            {
                int.TryParse(item.Split(',')[0], out tempId);
                double.TryParse(item.Split(',')[2], out tempPrice);
                MenuItems.Add(new MenuItem(tempId, item.Split(',')[1].ToString(), tempPrice));
                
            }
        }

        // This method will update the orderId that will be assigned to the next Order
        public void UpdateOrderId()
        {
            OrderId++;
        }
    }
}
