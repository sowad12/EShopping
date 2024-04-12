﻿namespace Catalog.Api.Manager.Interface
{
    public interface ISystemManager
    {
        Task<string> RunMigration();
        Task<string> RunMainSeeder();
        Task<dynamic> StoredProcedure();
        Task<string> RunUpdater();
    }
}
