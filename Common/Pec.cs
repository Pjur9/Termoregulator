using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class Pec
    {
        [DataMember]
        private bool rezim=false;
        [DataMember]
        private DateTime vrijeme;
        [DataMember]
        private int resursi;

        public int Resursi
        {
            get { return resursi; }
            set { resursi = value; }
        }

        public DateTime Vrijeme
        {
            get { return vrijeme; }
            set { vrijeme = value; }
        }

        public bool Rezim
        {
            get { return rezim; }
            set { rezim = value; }
        }
    }
}
