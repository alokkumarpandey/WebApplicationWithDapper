using Dapper;
using System.Data;


using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationWithDapper.Interfaces
{
    public interface IDapperRepository
    {
        T execute_sp<T>(string query, 
            DynamicParameters sp_params, 
            CommandType commandType = CommandType.StoredProcedure);
        List<T> GetAll<T>(string query, 
            DynamicParameters sp_params, 
            CommandType commandType = CommandType.StoredProcedure);
    }
}
