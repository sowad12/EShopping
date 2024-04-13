using Catalog.Api.Manager.Interface;
using Catalog.Library.Context;
using Catalog.Library.Model.Entities;
using Catalog.Library.Seeders;
using EShopping.Core.Constants;
using EShopping.Core.Infrastructure.Interface;
using EShopping.Core.Infrastructure.Options;
using EShopping.Utilities;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Options;
using System.Reflection;



namespace Catalog.Api.Manager.Implementation
{
    public class SystemManager : ISystemManager
    {
        private readonly DatabaseOptions _databaseOptions;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger _logger;
        
        private readonly IDapperContext _dapper;
        public SystemManager(IOptions<DatabaseOptions> databaseOptions, ApplicationDbContext applicationDbContext, IDapperContext dapper)
        {
          
            
            _databaseOptions = databaseOptions.Value;
            _applicationDbContext = applicationDbContext;
            _dapper = dapper;
            
        }
        public async Task<string> RunMainSeeder()
        {
            var rowsAffected = this.RunSeeder();
            return await Task.FromResult($"Seeded {rowsAffected} rows.");
        }
        private int RunSeeder()
        {
            int result = 0;
            var seederStatus = new SeederStatus();

            try
            {
                result = new MainSeeder().Seed(_applicationDbContext, _logger);
                seederStatus.Status = PluginConstants.Success;
            }
            catch (Exception ex)
            {
                seederStatus.Status = PluginConstants.Error;
                seederStatus.Exception = ex?.Message;
                seederStatus.StackTrace = ex?.StackTrace;
                _logger.LogError("SEED ERROR " + ex?.Message);
            }
            finally
            {
                // Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<SeederStatus> addResult = _WCAddonWcAbDatabaseContext.SeederStatuses.AddAsync(seederStatus).Result;
                _applicationDbContext.SaveChanges();
            }

            return result;
        }
        public async Task<string> RunMigration()
        {
            try
            {
                var msg = $"Total Migrations: ({EnumerableUtilities.Count(_applicationDbContext.Database.GetMigrations())}), " +
                          $" Applied Migrations: {EnumerableUtilities.Count(_applicationDbContext.Database.GetPendingMigrations())}";

                await _applicationDbContext.Database.MigrateAsync();
                return msg;
            }
            catch (Exception ex)
            {
                var msg = "(1)." + ex.Message;

                var ex2 = ex.InnerException?.Message;
                if (ex2.HasValue()) msg += Environment.NewLine + "(2)." + ex2;

                var ex3 = ex.InnerException?.InnerException?.Message;
                if (ex3.HasValue()) msg += Environment.NewLine + "(3)." + ex3;

                if (ex.StackTrace.HasValue()) msg += Environment.NewLine + "(STACK_TRACE)." + ex.StackTrace;
                return msg;
            }
        }

        public Task<string> RunUpdater()
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> StoredProcedure()
        {      
            var executionResult = string.Empty;
            var assembly = Assembly.GetExecutingAssembly();
            var sqlFiles = assembly.GetManifestResourceNames().
                        Where(file => file.EndsWith(".sql"));
            foreach (var sqlFile in sqlFiles)
            {
                using (Stream stream = assembly.GetManifestResourceStream(sqlFile))
                using (StreamReader reader = new StreamReader(stream))
                {
                    var sqlScript = reader.ReadToEnd();
                    var queryString = $"{sqlScript}";

                    try
                    {

                        var pluginSP = await _dapper.QueryAsync<string>(queryString);
                        executionResult = "Success";
                    }
                    catch (Exception exception)
                    {
                        executionResult = executionResult + "\n" + exception.Message;
                    }
                }
            }
            return executionResult;
        }
    }
}
