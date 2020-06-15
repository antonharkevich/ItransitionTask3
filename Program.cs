using System;
using System.Security.Cryptography;
using System.Text;
namespace rock_paper
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3 || args.Length % 2 == 0)
            {
                Console.WriteLine("Wrong Amount Of Arguments");
                return;
            }
            for(int i = 0; i < args.Length-1; i++)
            {
                for(int j = i + 1; j < args.Length; j++)
                {
                    if (args[i] == args[j])
                    {
                        Console.WriteLine("The Same Argumnets");
                        return;
                    }
                }
            }
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            Random randomize = new Random();
            int x= randomize.Next(0, args.Length);
            byte[] rand = new byte[32];
            rng.GetBytes(rand);
            HMAC Hmac = HMAC.Create("HMACSHA256");
            Hmac.Key = rand;
            Hmac.ComputeHash(Encoding.UTF8.GetBytes(args[x]));
            Console.Write("HMAC: ");
            for (int i = 0; i < 32; i++)
            {
                Console.Write("{0:x}",Hmac.Hash[i]);
            }
            Console.WriteLine("");
            Console.WriteLine("Avaiable Movies:");
            for(int i=0; i< args.Length; i++)
            {
                Console.WriteLine("{0} - {1}",i+1, args[i]);
            }
            Console.WriteLine("0- exit");
            Console.Write("Enter Your Movie: ");
            int mov = Convert.ToInt32(Console.ReadLine());
            while (mov < 0 || mov > args.Length)
            {
                mov = Convert.ToInt32(Console.ReadLine());
            }
            if (mov == 0)
            {
                return;
            }
            else
            {
                mov--;
                Console.WriteLine("Your movie: {0}", args[mov]);
                Console.WriteLine("Computer movie: {0}", args[x]);
                if (x == mov)
                {
                    Console.WriteLine("Draw!");
                    Console.Write("HMAC Key: ");
                    for (int j = 0; j < 32; j++)
                    {
                        Console.Write("{0:X}", Hmac.Key[j]);
                    }
                    return;
                }
                for(int i = 0; i < (args.Length - 1) / 2; i++)
                {
                    int y = mov + i + 1;
                    if (y >= args.Length)
                    {
                        y -= args.Length;
                    }
                    if (y == x)
                    {
                        Console.WriteLine("Computer win!");
                        Console.Write("HMAC Key: ");
                        for (int j = 0; j < 32; j++)
                        {
                            Console.Write("{0:X}", Hmac.Key[j]);
                        }
                        return;
                    }
                }
                Console.WriteLine("You win!");
                Console.Write("HMAC Key: ");
                for (int i = 0; i < 32; i++)
                {
                    Console.Write("{0:X}", Hmac.Key[i]);
                }
                return;
            }
        }
    }
}
