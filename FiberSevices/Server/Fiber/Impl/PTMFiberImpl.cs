using System;
using System.Collections.Generic;
using System.Data;
using ClassModel.Connection.Oracle;
using ClassModel.Connection.Reponsitory;
using ClassModel.Connection.Reponsitory.Impl;
using ClassModel.Connection.Sql;
using ClassModel.Model.Fiber;
using ClassModel.Model.Respond;
using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace FiberSevices.Server.Fiber.Impl
{
    public class PTMFiberImpl : Reponsitory<PTMFiber>, IPTMFiber
    {
        private IConfiguration m_configuration;
        public PTMFiberImpl(DataContext context, IConfiguration configuration) : base(context)
        {
            m_configuration = configuration;
        }

        public dynamic LayDS_PTM_Fiber_Theo_Thang(string thang)
        {
            List<PTMFiber> result = new List<PTMFiber>();
            var dyParam = new OracleDynamicParameters();
            dyParam.Add("i_thang", OracleDbType.Varchar2, ParameterDirection.Input, thang);
            dyParam.Add("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            var conn = GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {//TK_PTM_Fiber
                var query = "SL_COS.LayDS_PTM_Fiber_Theo_Thang";
                result = SqlMapper.Query<PTMFiber>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).AsList<PTMFiber>();
                //insertPTMFiber(result);
            }
            return result;
        }

        public dynamic execurePTMFiber(PTMFiber ptmfiber)
        {
            List<PTMFiber> result = new List<PTMFiber>();
            var dyParam = new OracleDynamicParameters();
            dyParam.Add("i_thang", OracleDbType.Varchar2, ParameterDirection.Input, ptmfiber.THANG_HOAMANG);
            dyParam.Add("i_start", OracleDbType.Int32, ParameterDirection.Input, ptmfiber.START);
            dyParam.Add("i_limit", OracleDbType.Int32, ParameterDirection.Input, ptmfiber.LIMIT);
            dyParam.Add("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            var conn = GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {//TK_PTM_Fiber
                var query = "SL_COS.TK_PTM_Fiber";
                result = SqlMapper.Query<PTMFiber>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).AsList<PTMFiber>();
                //var query = "select * from ppm_tuyetnt.ds_Fiber_PTM_201902_TEST where ngay_ins between to_date('01-07-2020','dd-mm-yyyy') and to_date('02-07-2020','dd-mm-yyyy')";
                //result = SqlMapper.Query<PTMFiber>(conn, query, param: dyParam, commandType: CommandType.Text).AsList<PTMFiber>();
                //insertPTMFiber(result);
            }
            return result;
        }
        public dynamic execuretotalrecordedPTMFiber(PTMFiber ptmfiber)
        {
            List<totalrecorded> result = new List<totalrecorded>();
            var dyParam = new OracleDynamicParameters();
            //dyParam.Add("i_thang", OracleDbType.Varchar2, ParameterDirection.Input, ptmfiber.THANG_HOAMANG);
            dyParam.Add("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            var conn = GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "select count(*) total  from ppm_tuyetnt.ds_Fiber_PTM_201902_TEST where thang_hoamang = '" + ptmfiber.THANG_HOAMANG+"'";
                result = SqlMapper.Query<totalrecorded>(conn, query, param: dyParam, commandType: CommandType.Text).AsList<totalrecorded>();
            }
            return result;
        }
        

        public IDbConnection GetConnection()
        {
            var connectionString = m_configuration.GetSection("connectionstrings").GetSection("defaultconnection2").Value;
            var conn = new OracleConnection(connectionString);
            return conn;
        }

        

    }
}
