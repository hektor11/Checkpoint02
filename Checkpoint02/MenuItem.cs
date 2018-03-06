using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint02
{
    class MenuItem
    {
        //This class represents an item in the menu(Ex: Cheesebuger)
        //Each menu item will have an ID, Title, Price and Quantity property
        public int ID { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }
        //Not yet implemented
        public double Quantity { get; private set; } = 0;
        
        //Public constructor for a menu item and sets properties except for quantity(not yet implemented)
        public MenuItem(int _id, string _title, double _price)
        {
            ID = _id;
            Name = _title;
            Price = _price;
        }

    }
}
