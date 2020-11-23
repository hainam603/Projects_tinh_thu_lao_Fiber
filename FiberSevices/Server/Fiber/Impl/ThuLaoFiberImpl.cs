using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
    public class ThuLaoFiberImpl : Reponsitory<ThuLaoFiber>, IThuLaoFiber
    {
        private IConfiguration m_configuration;
        public ThuLaoFiberImpl(DataContext context, IConfiguration configuration) : base(context)
        {
            m_configuration = configuration;
        }
        public dynamic execureThuLaoFiber(ThuLaoFiber thulaofiber)
        {
            List<ThuLaoFiber> result = new List<ThuLaoFiber>();
            var dyParam = new OracleDynamicParameters();
            dyParam.Add("i_thang", OracleDbType.Varchar2, ParameterDirection.Input, thulaofiber.THANG_HOAMANG);
            dyParam.Add("i_start", OracleDbType.Int32, ParameterDirection.Input, thulaofiber.START);
            dyParam.Add("i_limit", OracleDbType.Int32, ParameterDirection.Input, thulaofiber.LIMIT);
            dyParam.Add("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            var conn = GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (conn.State == ConnectionState.Open)
            {
                var query = "SL_COS.TK_ThuLao_Fiber";
                result = SqlMapper.Query<ThuLaoFiber>(conn, query, param: dyParam, commandType: CommandType.StoredProcedure).AsList<ThuLaoFiber>();

                //insertPTMFiber(result);
            }
            return result;
        }
        public dynamic execuretotalrecordedThuLaoFiber(ThuLaoFiber thulaofiber)
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
                var query = "select count(*) total  from ppm_tuyetnt.thulao_Fiber_2019_Test where thang_hoamang = '" + thulaofiber.THANG_HOAMANG + "'";
                result = SqlMapper.Query<totalrecorded>(conn, query, param: dyParam, commandType: CommandType.Text).AsList<totalrecorded>();
            }
            return result;
        }
        private void insertThuLaoFiber(List<ThuLaoFiber> listData)
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

        public IDbConnection GetConnection()
        {
            var connectionString = m_configuration.GetSection("connectionstrings").GetSection("defaultconnection2").Value;
            var conn = new OracleConnection(connectionString);
            return conn;
        }
    }
}
