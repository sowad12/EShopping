using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Core.Infrastructure.Interface
{
    public interface IDapperContext : IAsyncDisposable
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null);

        Task<T> First<T>(string sql, object parameters = null);

        Task<T> FirstOrDefault<T>(string sql, object parameters = null);

        Task<T> Single<T>(string sql, object parameters = null);

        Task<T> SingleOrDefault<T>(string sql, object parameters = null);

        Task<int> RunCommand(string sql, object parameters = null);

        Task<int> CreateAsync<T>(T value) where T : class;

        Task<bool> UpdateAsync<T>(T value) where T : class;

        Task<bool> DeleteAsync<T>(T value) where T : class;

        Task<List<T>> StoredProcedureQueryAsync<T>(string sql, object parameters = null);

        List<T> StoredProcedureQuery<T>(string sql, object parameters = null);

        Task<T> StoredProcedureQueryFirst<T>(string sql, object parameters = null);

        Task<int> StoredProcedureCommand(string sql, object parameters = null);
    }
}
