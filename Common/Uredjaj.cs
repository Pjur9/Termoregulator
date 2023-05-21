using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class Uredjaj
    {
       
        [DataMember]
        private int id;
        [DataMember]
        private double temperatura_prostorije;

        public double TemperaturaProstorije
        {
            get { return temperatura_prostorije; }
            set { temperatura_prostorije = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Uredjaj(int id, double temp)
        {
            if (id > 0 && temp < 100 && temp > -20)
            {
                Id = id;
                TemperaturaProstorije = temp;
            }
        }

        
    }
}
