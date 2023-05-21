using Heater.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Heater
{


    public  class BazaPodataka
    {
       public static void SaveData(string start_time, int resursi,  string elapsed_time)
        {
            using (IDbConnection connection = ConnectionPooling.GetConnection())
            {
                connection.Open();
                
                    string sql = $"INSERT INTO RADPECI (POCETNO_VRIJEME, RESURSI, UKUPNO_VRIJEME_RADA) VALUES ('{start_time}', {resursi},'{elapsed_time}' )";
                  
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sql;

                        command.ExecuteNonQuery();
                    }
                
                connection.Close();
            }
        }



       

        
    }

}

