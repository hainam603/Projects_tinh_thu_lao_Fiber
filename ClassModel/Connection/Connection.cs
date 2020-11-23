using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Oracle.ManagedDataAccess.Client;

namespace ClassModel.Connection.Connection
{
    public class Connection
    {
        public string connectionStringSQL { get; set; }
        public string connectionStringOracel { get; set; }
        public Connection()
        {
            connectionStringSQL = "Server= HAINAM_PC\\SQLEXPRESS,1433;Database=Fiber;User Id=dev;Password=123;MultipleActiveResultSets=true";
            connectionStringOracel = "data source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=exax7-scan.vnpthcm.vn)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=SGN)));password=Trami@123;user id=Mivtt";
        }
        public IDbConnection GetConnectionOracle()
        {
            return new OracleConnection(connectionStringOracel);   
        }
    }
}
