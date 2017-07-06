using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Bawt.Console
{
    class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            var sites = new List<string>();

            sites.Add("http://www.bash.org");
            sites.Add("http://www.2600.com");

            System.Console.SetIn(new StreamReader(System.Console.OpenStandardInput(), System.Console.InputEncoding, false, 8192));

            var consoleColor = System.Console.ForegroundColor;

            System.Console.ForegroundColor = ConsoleColor.DarkGreen;

            string banner = "\n  _                    _     _       \n | |                  | |   (_)      \n | |__   __ ___      _| |_   _  ___  \n | '_ \\ / _` \\ \\ /\\ / / __| | |/ _ \\ \n | |_) | (_| |\\ V  V /| |_ _| | (_) |\n |_.__/ \\__,_| \\_/\\_/  \\__(_)_|\\___/ \n\n";

            banner += "\n\n.exit to exit\n\n";


            var engine = new Engine();
            var message = string.Empty;
            var rnd = new Random();

            int rndInt;


            System.Console.Write(banner);
            System.Console.ForegroundColor = consoleColor;

            do
            {
                try
                {

                    System.Console.Write("Input: ");
                    message = System.Console.ReadLine();
                    if (message == ".exit")
                        return;

                    if (Helper.IsEncoded(message))
                    {
                        var decoded = engine.Decode(message);

                        System.Console.Write("\n" + decoded + "\n\n");
                    }
                    else
                    {
                        rndInt = rnd.Next(sites.Count());
                        var encoded = engine.Encode(message, (string)sites[rndInt]);
                        System.Console.Write("\n" + encoded + "\n\n");
                    }
                }
                catch (Exception ex)
                {

                }
            } while (true);

        }
    }
}
