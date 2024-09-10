using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actividad_1_5.Data.Utils
{
    public class DataHelper
    {
        private static DataHelper _instancia;
        private SqlConnection _connection;

        private DataHelper() {
            _connection = new SqlConnection(Properties.Resources.cnnString);
        }

        public static DataHelper GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new DataHelper();
            }
            return _instancia;
        }


        public SqlConnection OpenConnection()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();  
            }
            return _connection;
        }

        public void CloseConnection() {
            _connection?.Close();
        }

        public void CommitTransaction(SqlTransaction transaction)
        {
            transaction?.Commit();
        }

        public void RollbackTransaction(SqlTransaction transaction)
        {
            transaction?.Rollback();
        }
        public SqlTransaction beginTransaction()
        {

            return OpenConnection().BeginTransaction();
        }

        public DataTable ExecuteSPGet(string sp, Params paramsql)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(paramsql.Nombre, paramsql.Valor);
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception exc)
            {
                dt = null;
                throw exc;
            }

            return dt;
        }

        public DataTable ExecuteSPGet(string sp)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception exc)
            {
                dt = null;
                throw exc;
            }

            return dt;
        }

        public int ExecuteSPwParams(string sp, List<Params> paramsql, SqlTransaction t)
        {
            int idFactura = 0;
            int rows = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(sp, _connection, t);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (Params param in paramsql)
                {
                    cmd.Parameters.AddWithValue(param.Nombre, param.Valor);
                }

                if(sp == "SP_INSERTAR_MAESTRO")
                {
                    SqlParameter paramOutput = new SqlParameter("@id_factura", SqlDbType.Int); //se crea el param que va a guardar el parametro que retorna el SP
                    paramOutput.Direction = ParameterDirection.Output; //definir explicitamente que sea de salida (investigar mas depsues)
                    cmd.Parameters.Add(paramOutput); //se agrega al cmd

                    cmd.ExecuteNonQuery(); // se ejecuta cmd

                    idFactura = (int)paramOutput.Value; // se obtiene el valor del parametro que retorna el SP
                    cmd.Parameters.Clear();
                }
                else
                {
                    rows = cmd.ExecuteNonQuery();
                }

               
            }
            catch (Exception exc)
            {
                throw exc;
            }

            if(sp == "SP_INSERTAR_MAESTRO")
            {
                return idFactura;
            }
            else
            {
                return rows;
            }
            
        }

        public int ExecuteSPwParams(string sp, Params param, SqlTransaction t)
        {
            
            int rows = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(sp, _connection, t);
                cmd.CommandType = CommandType.StoredProcedure;
             
                cmd.Parameters.AddWithValue(param.Nombre, param.Valor);
                
                rows = cmd.ExecuteNonQuery();
                


            }
            catch (Exception exc)
            {
                throw exc;
            }

            return rows;

        }
    }
}
