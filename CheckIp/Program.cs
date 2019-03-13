using System;
using System.Net;
using System.Runtime.Loader;
using System.Threading;

namespace CheckIp
{
    class Program
    {
        static void Main(string[] args)
        {


            AssemblyLoadContext.Default.Unloading += SigTermEventHandler;
            Console.CancelKeyPress += CancelHandler;
            string externalOldIp = "";

            try
            {
                externalOldIp = new WebClient().DownloadString("http://icanhazip.com");

            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong with the connection to the ping host. This is the errormessage:\n {e.Message} ");
                Console.WriteLine(Mail.SendMail(args[0], externalOldIp, args[1], args[2], "Something went wrong", $"Something went wrong when trying to reach the ping server. Here is the error message: \n{e.Message}"));
            }


            Console.WriteLine("Sending first email...");
            Console.WriteLine(Mail.SendMail(args[0], externalOldIp, args[1], args[2], "First mail", "This is the first mail from the IP checker"));

            while (true)
            {
                string extarnalNewIp = "";
                Console.WriteLine("Checking external IP");
                try
                {
                    extarnalNewIp = new WebClient().DownloadString("http://icanhazip.com");
                    Console.WriteLine($"Old IP: {externalOldIp}New Ip: {extarnalNewIp}");
                    if (externalOldIp != extarnalNewIp)
                    {
                        Console.WriteLine(Mail.SendMail(args[0], externalOldIp, args[1], args[2], "You have a new IP Adress", "You have a new IP number: "));
                        externalOldIp = extarnalNewIp;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Something went wrong with the connection to the ping host. This is the errormessage:\n {e.Message} ");
                    Console.WriteLine(Mail.SendMail(args[0], externalOldIp, args[1], args[2], "Something went wrong", $"Something went wrong when trying to reach the ping server. Here is the error message: \n{e.Message}"));
                }



                Thread.Sleep(1800000);
            }




        }

        private static void SigTermEventHandler(AssemblyLoadContext obj)
        {
            Console.WriteLine("Unloading...");
        }

        private static void CancelHandler(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Exiting...");
            Environment.Exit(0);

        }


    }
}
