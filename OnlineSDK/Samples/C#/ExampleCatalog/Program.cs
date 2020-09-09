using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace ExampleCatalog
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine($"Version {Assembly.GetExecutingAssembly().GetName().Version}\r\n");
                var server = new HttpServer();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not start service: " + ex.Message);
                return;
            }

            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
