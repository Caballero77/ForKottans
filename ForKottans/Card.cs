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
        public static Dictionary<string, Regex> Vendors;

        public static string GetCredicCardVendor(string number)
        {
            InitializeVendors();

            while(number.Contains(' '))
                number = number.Remove(number.IndexOf(' '),1);

            foreach (KeyValuePair<string, Regex> t in Vendors)
                if(t.Value.IsMatch(number))
                    return t.Key;

            return "Unknown";
        }

        public static bool IsCreditCardNumerValid(string number)
        {
            InitializeVendors();

            bool buf = false;
            
            foreach(KeyValuePair<string,Regex> p in Vendors)
            {
                buf |= p.Value.IsMatch(number);
            }

            return (GetIdentificationNumber(number) == 0) && buf;
        }

        public static string GenerateNextCredicCardNumber(string number)
        {
            InitializeVendors();

            while (number.Contains(' '))
                number = number.Remove(number.IndexOf(' '), 1);

            ulong numb = Convert.ToUInt64(number);

            string buf;

            do
            {
                numb = numb - (numb % 10) + 10;
                buf = Convert.ToString((10 - (ulong)GetIdentificationNumber(Convert.ToString(numb))) % 10 + numb);
            } while (!IsCreditCardNumerValid(buf));

            return buf;
        }

        private static void InitializeVendors()
        {
            if (Vendors == null)
            {
                Vendors = new Dictionary<string, Regex>();
                Vendors.Add("American Expres", new Regex(@"^3[4,7][0-9]{13}$"));
                Vendors.Add("Visa", new Regex(@"^4([0-9]{12}|[0-9]{15}|[0-9]{18})$"));
                Vendors.Add("MasterCard", new Regex(@"^5[1-5]{1}[0-9]{14}$"));
                Vendors.Add("JCB", new Regex(@"^(35(2[8-9]|[3-8][0-9]))[0-9]{12}$"));
                Vendors.Add("Maestro", new Regex(@"^(5[0,6-9]|6[0-9])([0-9]){10,17}$"));
            }
        }

        private static int GetIdentificationNumber(string number)
        {
            while (number.Contains(' '))
                number = number.Remove(number.IndexOf(' '), 1);

            int[] aray = number.Reverse().ToArray().Select(arg => int.Parse(new string(new char[] { arg }))).ToArray();

            int res = 0;

            for (int i = 0; i < aray.Length; i++)
                if (i % 2 == 1)
                    if (aray[i] * 2 < 9)
                        res += aray[i] * 2;
                    else
                        res += aray[i] * 2 - 9;
                else res += aray[i];
            return res%10;
        }

    }
}