using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Device;
namespace Heater
{
    public class RadPeci : IPec
    {
        static int counter, counter1 = 0;
        static DateTime time, end_time;
        static int resursi = 0;
        public void UkljuciPec(bool rezim)
        {
            var timer = new Timer((e => Rad(rezim)), null, 0, 2000);

        }

        public void Rad(bool rezim)
        {
            if (rezim)
            {
                if (counter == 0)
                {
                    time = DateTime.Now;
                    counter++;
                    counter1 = 0;
                }

                resursi++;
            }
            else
            {
                if (counter1 == 0)
                {
                    end_time = DateTime.Now;
                    TimeSpan time_dif = end_time - time;
                    string vrijeme = time_dif.ToString();
                    string pocetno = time.ToString();
                    BazaPodataka.SaveData(pocetno, resursi, vrijeme);
                    counter = 0;
                    resursi = 0;
                    counter1++;
                }
            }
        }

    }
}