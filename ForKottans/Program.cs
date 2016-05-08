using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForKottans
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WindowHeight += 30;

            string number = "4534 6787 3452 7867";

            Console.WriteLine("First 20 numbers after 4534 6787 3452 7867\n\n");

            for (int i = 0; i < 20; i++)
            {
                number = Card.GenerateNextCreditCardNumber(number);
                Console.Write(number);
                if (Card.IsCreditCardNumerValid(number)) Console.WriteLine(" Valid");
            }
            

            //Maestro
            Console.WriteLine("\n\n\n\nMaestro");
            Console.WriteLine(Card.GetCreditCardVendor("5005678438955434565") == "Maestro");
            Console.WriteLine(Card.GetCreditCardVendor("61674975867456") == "Maestro");
            Console.WriteLine(Card.GetCreditCardVendor("565678438904") == "Maestro");
            Console.WriteLine(Card.GetCreditCardVendor("6956784389065457") == "Maestro");
            Console.WriteLine(Card.GetCreditCardVendor("6590 5737 5465") == "Maestro");
            Console.WriteLine(Card.GetCreditCardVendor("6309 8450 2454 2355") == "Maestro");
            Console.WriteLine(Card.GetCreditCardVendor("5009 5466 3457 543") == "Maestro");

            //JCB 
            Console.WriteLine("\n\nJBC");
            Console.WriteLine(Card.GetCreditCardVendor("3580 4534 6545 5434") == "JCB");
            Console.WriteLine(Card.GetCreditCardVendor("3570 4534 6545 5434") == "JCB");
            Console.WriteLine(Card.GetCreditCardVendor("3560 4534 6545 5434") == "JCB");
            Console.WriteLine(Card.GetCreditCardVendor("3589 4534 6545 5434") == "JCB");
            Console.WriteLine(Card.GetCreditCardVendor("4534 3528 6545 5434") != "JCB");


            //MasterCard
            Console.WriteLine("\n\nMasterCard");
            Console.WriteLine(Card.GetCreditCardVendor("558 956 2343 546783") == "MasterCard");
            Console.WriteLine(Card.GetCreditCardVendor("518956 234 3546783") == "MasterCard");
            Console.WriteLine(Card.GetCreditCardVendor("5289 562343546783") == "MasterCard");
            Console.WriteLine(Card.GetCreditCardVendor("53895623435467 83") == "MasterCard");
            Console.WriteLine(Card.GetCreditCardVendor("54895623435467 8 3") == "MasterCard");
            Console.WriteLine(Card.GetCreditCardVendor("5289 5623 4354 6783") == "MasterCard");
        
            // Visa
            Console.WriteLine("\n\nVisa");
            Console.WriteLine(Card.GetCreditCardVendor("45 65 67 78 65 23 7") == "Visa");
            Console.WriteLine(Card.GetCreditCardVendor("4 465674673456789") == "Visa");
            Console.WriteLine(Card.GetCreditCardVendor("4534 6787 3452 7867") == "Visa");
            Console.WriteLine(Card.GetCreditCardVendor("453 564 567 234 546 785 5") == "Visa");
            Console.WriteLine(Card.GetCreditCardVendor("5658964563243") != "Visa");
            Console.WriteLine(Card.GetCreditCardVendor("4444444444444") == "Visa");
        
            //American Expres
            Console.WriteLine("\n\nAmerican Express");
            Console.WriteLine(Card.GetCreditCardVendor("373454323432344") == "American Express");
            Console.WriteLine(Card.GetCreditCardVendor("349090444444444") == "American Express");
            Console.WriteLine(Card.GetCreditCardVendor("3789 0967 8767 987") == "American Express");
            Console.WriteLine(Card.GetCreditCardVendor("343434343434343") == "American Express");
            Console.WriteLine(Card.GetCreditCardVendor("37 23 54 12 34 54 32 4") == "American Express");
            Console.WriteLine(Card.GetCreditCardVendor("34564 23432 57654") == "American Express");

            

            Console.ReadLine();
        }
    }
}
