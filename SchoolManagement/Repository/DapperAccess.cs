using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace SchoolManagement.Repository
{
    public class DapperAccess
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;

        public DapperAccess(IConfiguration configuration) 
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SchoolManagementConnection");
        }

        public int Execute(string storeProcedure, object parameters = null) 
        {
            using (IDbConnection db = new SqlConnection(_connectionString)) 
            {
                db.Open();

                using (var transaction = db.BeginTransaction()) 
                {
                    try
                    {
                        int result =  db.Execute(storeProcedure, parameters, transaction, commandType: CommandType.StoredProcedure);

                        transaction.Commit();

                        return result;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception(ex.Message);
                    }
                }   
            }
        }

        public IEnumerable<T> Query<T>(string storedProcedure, object parameters) 
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                try
                {
                    return db.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }                
            }
        }

        public T QueryFirst<T> (string storedProcedure, object parameters)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                try
                {
                    return db.QueryFirst<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
