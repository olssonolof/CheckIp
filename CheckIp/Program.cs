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

            string externalOldIp = new WebClient().DownloadString("http://icanhazip.com");
            Console.WriteLine("Sending first email...");
            Console.WriteLine(Mail.SendMail(args[0], externalOldIp, args[1], args[2]));

            while (true)
            {
                Console.WriteLine("Checking external IP");
                string extarnalNewIp = new WebClient().DownloadString("http://icanhazip.com");
                Console.WriteLine($"Old IP: {externalOldIp}New Ip: {extarnalNewIp}");



                if (externalOldIp != extarnalNewIp)
                {
                    Console.WriteLine(Mail.SendMail(args[0], externalOldIp, args[1], args[2]));
                    externalOldIp = extarnalNewIp;
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
