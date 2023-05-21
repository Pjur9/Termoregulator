using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Heater;
namespace Regulator
{
    internal class Program
    {
        public static double dnevna_temperatura, nocna_temperatura;
        //static string rezim;
        public static string pocetni_sati, pocetni_minuti, krajnji_sati, krajnji_minuti;
        static void Main(string[] args)
        {


            Console.WriteLine("Unesi opseg vremena za dnevni rezim u formatu(h:mm-h:mm)");
            
            
            string niz = Console.ReadLine();
            if (niz.Length > 11 || niz.Length<9)
            {
                throw new ArgumentException("Pogresan format!");
            }
            string[] opseg = niz.Split('-');
            Console.WriteLine(opseg[0]);
            Console.WriteLine(opseg[1]);
             pocetni_sati = opseg[0].Split(':')[0];
            pocetni_minuti = opseg[0].Split(':')[1];
             krajnji_sati = opseg[1].Split(':')[0];
             krajnji_minuti = opseg[1].Split(':')[1];
            Console.WriteLine("Unesite  zeljenu temperaturu za dnevni rezim");
            dnevna_temperatura = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Unesite i zeljenu temperaturu za nocni rezim");
            nocna_temperatura = Convert.ToDouble(Console.ReadLine());

            ServiceHost svc = new ServiceHost(typeof(RegulatorServer));
            svc.AddServiceEndpoint(typeof(Common.IRegulator), new NetTcpBinding(), new Uri("net.tcp://localhost:4005/IRegulator"));
            svc.Open();
            
            Console.WriteLine("Pritisnite [Enter] za zaustavljanje servisa.");
            Console.ReadLine();
        }
    }
}
