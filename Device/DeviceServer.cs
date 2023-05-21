using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
namespace Device
{
   public class State{
        public  bool rezim;
    }
    public class DeviceServer
    {
        
        static int counter = 0;
        private Thread thread;
        static int brt1 = 0;
        public static Semaphore semaphore = new Semaphore(1, 1);
        public static Semaphore semaphore1 = new Semaphore(1, 1);
        public static Timer timer;
        public static Timer timer1;
        public static State state=new State();
        public  static Thread t1 = new Thread(() => tajmer2(state));
        public static List<Uredjaj> uredjaji=new List<Uredjaj>();
        public static bool provjera = true;
        public static int provjeriti = 0;
 
        static void Main(string[] args)
        {
            Console.WriteLine("Unesite broj uredjaja");
            int br=Convert.ToInt32(Console.ReadLine());
            for(int i=0;i<br; i++)
            {
                provjera = true;
                Uredjaj u = LogIn();
                if (uredjaji.Any())
                {
                    while (provjera)
                    {
                        for (int j = 0; j < uredjaji.Count; j++) 
                        {
                            if (uredjaji[j].Id == u.Id)
                            {
                                provjeriti++;
                            } 
                        }

                        if (provjeriti != 0)
                        {
                            provjera = true;
                            Console.WriteLine("VEc postoji uredjaj sa datim id-jem, unesite ponovo");
                            u = LogIn();
                            provjeriti = 0;
                        }
                        else
                        {
                            provjera = false;
                        }
                    }
                    uredjaji.Add(u);
                    
                 
                }
                else
                {
                    uredjaji.Add(u);
                    Console.WriteLine("Uredjaj registrovan");
                }
            }


            for(int i=0;i < uredjaji.Count; i++)
            {
                Console.WriteLine(uredjaji[i].Id +  ",  "+ uredjaji[i].TemperaturaProstorije);
            }
            DeviceServer program = new DeviceServer();
             Console.WriteLine("Press enter to quit the application...");
            Console.ReadLine();

        }

        public DeviceServer()
        {
            thread = new Thread(Provjera);
            thread.Start();
        }

        public void Provjera()
        {
            
                timer = new Timer((e => provjerava()), null, 21000, 15000);
                GC.KeepAlive(timer);
           
            
        }

        public static void provjerava()
        {

            Console.WriteLine("Zapocinje provjera temperature...");
           
            semaphore.WaitOne();
            ChannelFactory<IRegulator> factory = new ChannelFactory<IRegulator>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:4005/IRegulator"));
            IRegulator kanal = factory.CreateChannel();
            if (counter>=30)
            {
                Console.WriteLine("Pecnica nije uplajena duze vrijeme, temperatura se smanjuje u prostorijama");
                for (int i = 0; i < uredjaji.Count; i++)
                {
                    uredjaji[i].TemperaturaProstorije -= 0.05;
                }


            }
            counter += 3;
            double rez = 0;
            for (int i = 0; i < uredjaji.Count; i++)
            {
                rez += uredjaji[i].TemperaturaProstorije;
            }
            double num=uredjaji.Count;
            rez = rez / num;
         
            
            bool poruka=kanal.poredi_temp(rez);
            if (poruka)
            {
                if (brt1 == 0)
                {
                    t1.Start();
                    brt1++;
                }
                state.rezim = poruka;
                counter = 0;
            }
            else
            {
                state.rezim = poruka;
            }

            Console.WriteLine("Ispis temperatura svih prostorija: ");
            for (int i = 0; i < uredjaji.Count; i++)
            {
                Console.WriteLine($"Uredjaj {i} ima temperaturu {uredjaji[i].TemperaturaProstorije}");
            }

            Console.WriteLine("Vrijeme rada uredjaja {0}", counter);
            semaphore.Release();
        }

        public static void tajmer2(State state)
        {
            if (state == null)
            {
                throw new ArgumentException("Greska: ne moze biti null!");
            }
            timer1 = new System.Threading.Timer((e => rad(state)), null, 0, 6000);
            GC.KeepAlive(timer1);
        }

        static void rad(State state)
        {
            if (state == null)
            {
                throw new ArgumentException("Greska: ne moze biti null!");
            }
            bool rezim = state.rezim;
            semaphore1.WaitOne();
            if (rezim)
            {
                Console.WriteLine("Pec dize temperaturu za 0.01");
                for (int i = 0; i < uredjaji.Count; i++)
                {
                    uredjaji[i].TemperaturaProstorije += 0.01;
                }

                for (int i = 0; i < uredjaji.Count; i++)
                {
                    Console.WriteLine($"Temperatura uredjaja {i} je {uredjaji[i].TemperaturaProstorije}");
                }
               
                
            }
            else
            {
                
                Console.WriteLine("Pecnica je ugasena");
            }
            semaphore1.Release();
        }

        public static Uredjaj LogIn()
        {
           
            Console.WriteLine($"Unesite id uredjaja");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Unesite temperaturu");
            double temp = Convert.ToDouble(Console.ReadLine());

            Uredjaj u=new Uredjaj(id, temp);

            return u;
        }

    }
}
