using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IRegulator
    {
        [OperationContract]
         string promjeni_rezim_rada(string psati, string pmin, string ksati, string kmin, string sati, string minute);


        [OperationContract]
        bool poredi_temp(double avg);
        [OperationContract]
        void startuj(bool rezim);
        
      
    }
}
