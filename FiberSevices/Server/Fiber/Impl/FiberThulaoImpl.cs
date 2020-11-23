using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClassModel.Connection.Connection;
using ClassModel.Connection.Oracle;
using ClassModel.Connection.Reponsitory.Impl;
using ClassModel.Connection.Sql;
using ClassModel.Model.Fiber;
using ClassModel.Model.Respond;
using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace FiberSevices.Server.Fiber.Impl
{
    public class FiberThulaoImpl : Reponsitory<FiberThulao>, IFiberThulao
    {
        Connection Connection = new Connection();
        public FiberThulaoImpl(DataContext dataContext, IConfiguration configuration) : base(dataContext)
        {
            
        }
        public dynamic LayDS_Thulao_Fiber_Theo_Thang_D1(monthFromTo month)
        {
            List<FiberThulao> result = new List<FiberThulao>();
            var dyParam = new OracleDynamicParameters();
            dyParam.Add("tu_thang", OracleDbType.Varchar2, ParameterDirection.Input, month.monthFrom);
            dyParam.Add("den_thang", OracleDbType.Varchar2, ParameterDirection.Input, month.monthTo);
            dyParam.Add("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            var conn = Connection.GetConnectionOracle();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {//TK_PTM_Fiber
                var query = "PK_FIBER.LayDS_Thulao_Fiber_Theo_Thang_D1";
                result = SqlMapper.Query<FiberThulao>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).AsList<FiberThulao>();
                conn.Close();
            }
            return result;
        }
        public dynamic LayDS_Thulao_Fiber_Theo_Thang_D2(monthFromTo month)
        {
            List<FiberThulao> result = new List<FiberThulao>();
            var dyParam = new OracleDynamicParameters();
            dyParam.Add("tu_thang", OracleDbType.Varchar2, ParameterDirection.Input, month.monthFrom);
            dyParam.Add("den_thang", OracleDbType.Varchar2, ParameterDirection.Input, month.monthTo);
            dyParam.Add("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            var conn = Connection.GetConnectionOracle();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {//TK_PTM_Fiber
                var query = "PK_FIBER.LayDS_Thulao_Fiber_Theo_Thang_D2";
                result = SqlMapper.Query<FiberThulao>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).AsList<FiberThulao>();
                conn.Close();
            }
            return result;
        }
    }
}
