using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Device;
using Common;
using System.ServiceModel;

namespace Heater
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceHost svc = new ServiceHost(typeof(RadPeci));
            svc.AddServiceEndpoint(typeof(Common.IPec), new NetTcpBinding(), new Uri("net.tcp://localhost:4002/IPec"));
            svc.Open();
            Console.WriteLine("Pokrenut server peci");
            Console.ReadKey();
        }
    }
}
