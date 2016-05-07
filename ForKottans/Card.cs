using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ForKottans
{
    class Card
    {
        

        public static List<Tuple<string,Regex, UInt64>> Vendors;

        public static string GetCreditCardVendor(string number)
        {
            InitializeVendors();

            while(number.Contains(' '))
                number = number.Remove(number.IndexOf(' '),1);

            foreach (Tuple<string,Regex, UInt64> t in Vendors)
                if(t.Item2.IsMatch(number))
                    return t.Item1;

            return "Unknown";
        }

        public static bool IsCreditCardNumerValid(string number)
        {
            InitializeVendors();

            while (number.Contains(' '))
                number = number.Remove(number.IndexOf(' '), 1);


            bool buf = false;
            
            foreach(Tuple<string, Regex, UInt64> p in Vendors)
            {
                buf |= p.Item2.IsMatch(number);
            }

            return (GetIdentificationNumber(number) == 0) && buf;
        }

        public static string GenerateNextCreditCardNumber(string number)
        {
            InitializeVendors();

            while (number.Contains(' '))
                number = number.Remove(number.IndexOf(' '), 1);

            string vendor = GetCreditCardVendor(number);

            if ((vendor == "Unknown") || (!IsCreditCardNumerValid(number)))
            {
                throw new Exception("I can`t give you the next number because input number isn`t correct");
            }

            UInt64 maxlength = Vendors.Find(arg => arg.Item1 == vendor).Item3;

            ulong numb;

            string res;

            do
            {

                numb = Convert.ToUInt64(number);

                numb = numb - (numb % 10) + 10;

                res = Convert.ToString((10 - (ulong)GetIdentificationNumber(Convert.ToString(numb))) % 10 + numb);

                if (GetCreditCardVendor(res) == vendor) break;
                else
                {
                    numb = Convert.ToUInt64(res);

                    numb -= numb % (ulong)Math.Pow(10, res.Length - 4);

                    numb += (ulong)(Math.Pow(10, res.Length - 4));

                    number = Convert.ToString(numb);
                }
                if (numb > maxlength) throw new Exception("No more card numbers available for this vendor");

            } while (true);


            return res;
        }

        private static void InitializeVendors()
        {
            if (Vendors == null)
            {
                Vendors = new List<Tuple<string, Regex, UInt64>>();
                Vendors.Add(new Tuple<string, Regex, UInt64>("American Express", new Regex(@"^3[4,7][0-9]{13}$"),379999999999999));
                Vendors.Add(new Tuple<string, Regex, UInt64>( "Visa", new Regex(@"^4([0-9]{12}|[0-9]{15}|[0-9]{18})$"),4999999999999999999));
                Vendors.Add(new Tuple<string, Regex, UInt64>("MasterCard", new Regex(@"^5[1-5]{1}[0-9]{14}$"),5599999999999999));
                Vendors.Add(new Tuple<string, Regex, UInt64>("JCB", new Regex(@"^(35(2[8-9]|[3-8][0-9]))[0-9]{12}$"),3589999999999999));
                Vendors.Add(new Tuple<string, Regex, UInt64>("Maestro", new Regex(@"^(5018|5020|5038|6304|6759|6761|6763)([0-9]){12,15}$"), 6763999999999999999));

                //5018, 5020, 5038, 5612, 5893, 6304, 6759, 6761, 6762, 6763, 0604, 6390
            }
        }

        /*
           American Express - 34****,37**** 15
           Visa 4***** 13,16,19
           MasterCard 51****-55**** 16
           Maestro 5018, 5020, 5038, 6304, 6759, 6761, 6763 12 - 19
           JCB 3528** - 3589** 16
           */

        private static int GetIdentificationNumber(string number)
        {
            while (number.Contains(' '))
                number = number.Remove(number.IndexOf(' '), 1);

            int[] array = number.Reverse().ToArray().Select(arg => int.Parse(new string(new char[] { arg }))).ToArray();

            int res = 0;

            for (int i = 0; i < array.Length; i++)
                if (i % 2 == 1)
                    if (array[i] * 2 < 9)
                        res += array[i] * 2;
                    else
                        res += array[i] * 2 - 9;
                else res += array[i];
            return res%10;
        }

    }
}
