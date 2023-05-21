using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
  
    public enum Rezim { dnevni, nocni};
    [DataContract]
    public class Regulator_model
    {
        [DataMember]
        private Rezim rezim_rada;
        [DataMember]
        private DateTime opseg_vremena;
       

        public Rezim Rezim_rada
        {
            get { return rezim_rada; }
            set { rezim_rada = value; }
        }

        public DateTime Opseg_vremenaP
        {
            get { return opseg_vremena; }
            set { opseg_vremena = value; }
        }

    }
}
