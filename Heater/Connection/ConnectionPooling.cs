﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heater.Connection
{
    internal class ConnectionPooling
    {
        private static OracleConnection instance = null;

        public static OracleConnection GetConnection()
        {
            if (instance == null || instance.State == System.Data.ConnectionState.Closed)
            {
                OracleConnectionStringBuilder ocsb = new OracleConnectionStringBuilder();
                ocsb.DataSource = Connection.ConnectionParams.LOCAL_DATA_SOURCE;
                ocsb.UserID = Connection.ConnectionParams.USER_ID;
                ocsb.Password = Connection.ConnectionParams.PASSWORD;
                ocsb.Pooling = true;
                ocsb.MinPoolSize = 1;
                ocsb.MaxPoolSize = 10;
                ocsb.IncrPoolSize = 3;
                ocsb.ConnectionLifeTime = 5;
                ocsb.ConnectionTimeout = 30;
                instance = new OracleConnection(ocsb.ConnectionString);
            }
            return instance;
        }

        public void Dispose()
        {
            Console.WriteLine("Closing connection");
            if (instance != null)
            {
                instance.Close();
                instance.Dispose();
            }

        }
    }
}
