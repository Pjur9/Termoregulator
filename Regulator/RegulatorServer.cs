using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;
using Heater;
using System.Threading;
using System.IO;
using System.CodeDom;

namespace Regulator
{
    public class RegulatorServer : IRegulator
    {
        static bool rez = false;
        static int brt = 0;
        public FileStream fileStream;
        public DateTime time=DateTime.Now;
        static string folder = @"C:\Users\User\OneDrive\Desktop\New folder\projekat termoregulator\Termoregulator\Logger.txt";
        public  string poruka = "";
        public string promjeni_rezim_rada(string psati, string pmin, string ksati, string kmin, string sati, string minute)
        {
            string rezim = "";
            if (Convert.ToInt32(sati) < 0 || Convert.ToInt32(minute) < 0 || Convert.ToInt32(psati) < 0 || Convert.ToInt32(ksati) < 0 || Convert.ToInt32(pmin) < 0 || Convert.ToInt32(kmin) < 0 ||
               Convert.ToInt32(sati) > 24 || Convert.ToInt32(psati) > 24 || Convert.ToInt32(minute) > 60 || Convert.ToInt32(ksati) > 24 || Convert.ToInt32(pmin) > 60 || Convert.ToInt32(kmin) > 60)
            {
                throw new ArgumentException("Greska pri unosu temperature");
            }
            
                if (Convert.ToInt32(sati) == Convert.ToInt32(psati))
                {
                    if (Convert.ToInt32(minute) <= Convert.ToInt32(pmin))
                    {

                        rezim = "nocni";
                    }
                    else
                    {

                        rezim = "dnevni";
                    }
                }
                else if (Convert.ToInt32(sati) == Convert.ToInt32(ksati))
                {
                    if (Convert.ToInt32(minute) <= Convert.ToInt32(kmin))
                    {

                        rezim = "dnevni";
                    }
                    else
                    {

                        rezim = "nocni";
                    }

                }
                else if (Convert.ToInt32(sati) < Convert.ToInt32(psati) || Convert.ToInt32(sati) > Convert.ToInt32(ksati))
                {

                    rezim = "nocni";
                }
                else
                {

                    rezim = "dnevni";
                }
            
            Console.WriteLine($"Sati {sati}:{minute}, a rezim je {rezim}");
            
            return rezim;
        }

        public bool poredi_temp(double avg)
        {
            if(avg>100 || avg < -10)
            {
                throw new ArgumentException("Greska: Temperatura nije unesena ispravno!");
            }
            double ktemp = 0;
            string time = DateTime.Now.ToString("H:mm");
            string sati = time.Split(':')[0];
            string minute = time.Split(':')[1];
            string rezim=promjeni_rezim_rada(Program.pocetni_sati, Program.pocetni_minuti, Program.krajnji_sati, Program.krajnji_minuti, sati, minute);
            if (rezim.Equals("dnevni")) {
                ktemp = Program.dnevna_temperatura;
            }
            else
            {
                ktemp=Program.nocna_temperatura;
            }
            
            if (ktemp < avg)
            {
                
                
                rez = false;
                startuj(false);
            }
            else
            {
                
                rez = true;
                startuj(true);
            }
            if (brt == 0)
            {
                File.WriteAllText(folder, "SACUVANI PODACI:\n\n");
                brt++;
            }
            
            poruka = $"Vrijeme dolaska poruke sa uredjaja {time}, prosjecna temperatura {avg}, zadata temperatura {ktemp}.\n";
            File.AppendAllText(folder, poruka);
            return rez;
        }


        public void startuj(bool rezim)
        {
            ChannelFactory<IPec> factory = new ChannelFactory<IPec>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:4002/IPec"));
            IPec kanal = factory.CreateChannel();
            kanal.UkljuciPec(rezim);
            factory.Close();
        }
    }
}
