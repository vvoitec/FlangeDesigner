using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using FlangeDesigner.Main.Domain.Entities;
using FlangeDesigner.Main.Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using TechTalk.SpecFlow;
using Xunit;

namespace FlangeDesigner.Spec.Hooks
{
    [Binding]
    public class DBHooks
    {
        private readonly string _connectionString;

        public DBHooks(IOptions<DapperRepositoryOptions> options)
        {
            _connectionString = options.Value.SqLite;
        }    
        
        [AfterScenario]
        [Scope(Tag = "clearDatabase")]
        public void ClearDatabase()
        {
            using (IDbConnection cnn = new SQLiteConnection(_connectionString))
            {
                cnn.Execute("delete from projects;");
            }
        }
    }
}