using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using ClassModel.Connection.Connection;
using ClassModel.Connection.Oracle;
using ClassModel.Connection.Reponsitory.Impl;
using ClassModel.Connection.Sql;
using ClassModel.Model.Fiber;
using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace FiberSevices.Server.Fiber.Impl
{
    public class FiberPTMImpl:Reponsitory<FiberPTM>, IFiberPTM
    {

        private Connection connection = new Connection();
        public FiberPTMImpl(DataContext context, IConfiguration configuration) : base(context)
        {
        }

        public dynamic LayDS_PTM_Fiber_Theothang(FiberPTM fiber)
        {
            List<FiberPTM> result = new List<FiberPTM>();
            var dyParam = new OracleDynamicParameters();
            dyParam.Add("i_thang", OracleDbType.Varchar2, ParameterDirection.Input, fiber.thang_tt);
            dyParam.Add("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            var conn = connection.GetConnectionOracle();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {//TK_PTM_Fiber
                var query = "SL_COS.LayDS_PTM_Fiber_Theo_Thang";
                result = SqlMapper.Query<FiberPTM>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).AsList<FiberPTM>();
                //InsertFiberPTMToSQLServer(result);
                conn.Close();
            }
            return result;
        }

       

        private void InsertFiberPTMToSQLServer(List<FiberPTM> listData)
        {
            try
            {
                foreach (var i in listData)
                {
                    insert(i);
                }
            }
            catch (Exception)
            { }

        }
    }
}
