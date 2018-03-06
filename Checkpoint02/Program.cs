using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint02
{
    class Program
    {
        static string ValidateUserInput()
        {
            string userOption = Console.ReadLine();
            bool isValid = false;
            while(!isValid)
            {
                if (userOption == "a")
                {
                    isValid = true;
                    return "a";
                }

                else if (userOption == "r")
                {
                    isValid = true;
                    return "r";
                }

                else if (userOption == "c")
                {
                    isValid = true;
                    return "c";
                }

                else
                {
                    Console.WriteLine("Please enter the letter of the option you would like to continue with on your order:");
                    Console.WriteLine("[a] add an item to your order");
                    Console.WriteLine("[r] remove an item to your order");
                    Console.WriteLine("[c] complete your order");
                    userOption = Console.ReadLine();
                }

            }
            
            return userOption;
        }
 
        static void Main(string[] args)
        {
            Console.WriteLine("You can add the items from the menu below by entering the item ID.");
            Console.WriteLine("Removing items can be done after you have finished adding items to your order.");
            Console.WriteLine();
            ///Create the menu containing the menu items
            Menu menu = new Menu();
            menu.CreateMenu();
            ///Initialize variables to keep track of order status
            ///orderAgain is used to denote if after placing one order, the user would like to place a new order
            ///orderComplete is used to denote when a user has finished adding items to their order
            bool orderAgain = true;
            bool orderComplete = false;
            //UserInput and UserInputString are used to store the menuItem Ids of the items being added to the OrderList
            int userInput;
            int itemToRemove;
            string userInputString;
            string itemToRemoveString;
            string receiptOption;
            //Create an order Object to store the user selection
            Order order = new Order(menu.OrderId);
            menu.printMenu();
            while (orderAgain)
            {
                while (!orderComplete)
                {
                    Console.WriteLine("Please enter the letter of the option you would like to continue with on your order:");
                    Console.WriteLine("[a] add an item to your order");
                    Console.WriteLine("[r] remove an item to your order");
                    Console.WriteLine("[c] complete your order");
                    //This switch statement handles the option a user can choose
                    //a - add another item to order
                    //r - remove item from order
                    //c - complete order and print receipt
                    switch (ValidateUserInput())
                    {
                        //This case is for adding an item to the order
                        case "a":
                            Console.Clear();
                            //Print out the menu so the user can add more items to their order
                            menu.printMenu();
                            //Begin validating if userInput is valid in order to add menuItem to the orderList
                            userInput = 0;
                            while (userInput < 1 || userInput > menu.MenuItems.Count())
                            {
                                Console.WriteLine("Please enter a valid item id of the item you would like to order from the menu above:");
                                userInputString = Console.ReadLine();
                                int.TryParse(userInputString, out userInput);

                            }
                            //After userInput has been validated, add the menuItem to the orderList
                            order.AddToOrder(menu.MenuItems[userInput - 1]);
                            Console.WriteLine("---------------------------------------------------------------------------------");
                            Console.WriteLine("Your order currently contains:");
                            order.PrintOrderItems();
                            Console.WriteLine("Your subtotal is: ${0}", Math.Round(Convert.ToDecimal(order.SubTotal), 2));
                            break;

                        //This case is for removing an item from the order
                        case "r":
                            //Check if the order is empty before removing items 
                            if (!order.isEmpty)
                            {
                                Console.Clear();
                                Console.WriteLine("Your order currently contains:");
                                order.PrintOrderItems();
                                Console.WriteLine("Your subtotal is: ${0}", Math.Round(Convert.ToDecimal(order.SubTotal), 2));
                                Console.WriteLine("Please enter the cart Item ID of the item you would like to remove");
                                itemToRemoveString = Console.ReadLine();
                                int.TryParse(itemToRemoveString, out itemToRemove);
                                // Validate that the itemToRemove is indexable in the OrderList
                                while (itemToRemove < 0 || itemToRemove >= order.OrderList.Count)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Invalid cart item id. Please enter a valid id from below");
                                    order.PrintOrderItems();
                                    itemToRemoveString = Console.ReadLine();
                                    int.TryParse(itemToRemoveString, out itemToRemove);
                                }
                                if (order.RemoveFromOrder(itemToRemove))
                                {
                                    Console.WriteLine("Item was successfully removed from your order");
                                }
                                order.PrintOrderItems();
                                Console.WriteLine("Your subtotal is: ${0}", Math.Round(Convert.ToDecimal(order.SubTotal), 2));
                                Console.WriteLine();
                                menu.printMenu();
                            }

                            //The order is empty so inform the user and break to ask for another option
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Your order is currently empty. Please add items before removing.");
                                
                            }
                            
                            break;
                        
                        //This case is for marking the order as complete and generating a receipt
                        case "c":
                            orderComplete = true;
                            // Once the order is marked as complete, call the PlaceOrder method to simulate sending it to the kitchen
                            order.PlaceOrder();
                            Console.WriteLine("Thank you for shopping with us. Your order was placed at {0}", order.TimeOrdered.ToShortTimeString());
                            order.CalculateTotal();
                            Console.WriteLine("Your total is ${0}", Math.Round(Convert.ToDecimal(order.Total), 2));
                            Console.WriteLine("------------------------------------------------------------------");
                            //Check if the user would like a receipt
                            Console.WriteLine("Would you like a receipt?[y/n]");
                            receiptOption = Console.ReadLine();
                            if (receiptOption.ToLower() == "y")
                            {
                                //Create a ReceiptPrinter object that will take in the current Order Object and write the information to a file
                                ReceiptGenerator receipt = new ReceiptGenerator(order);
                                receipt.WriteReceipt();
                                Console.WriteLine("Receipt has been sent to you...");
                            }
                            else
                            {
                                Console.WriteLine("No Receipt sent...");
                            }
                            //Check if the user will place a new order
                            Console.WriteLine("Would you like to place another order?");
                            userInputString = Console.ReadLine();
                            if (userInputString.ToLower() == "y")
                            {
                                Console.WriteLine("Your order of ID {0} is ready!", order.Id);
                                Console.WriteLine("Please press enter to continue with your next order.");
                                Console.Clear();
                                //Update the OrderId that will be assigned to the new order
                                menu.UpdateOrderId();
                                //Create a new order using the updated OrderId and reset the orderComplete flag to false
                                order = new Order(menu.OrderId);
                                orderComplete = false;
                            }

                            else
                            {
                                orderAgain = false;
                                Console.WriteLine("Your order of ID {0} is ready!", order.Id);
                                Console.WriteLine("Thank you and come again!");
                                Console.ReadLine();
                            }
                            break;


                    }

                    
                }

                
                
            }

        }
    }
}
