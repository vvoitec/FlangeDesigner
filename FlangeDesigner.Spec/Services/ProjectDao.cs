using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using FlangeDesigner.Main.Domain.Entities;
using FlangeDesigner.Main.Infrastructure.Persistence;
using FlangeDesigner.SolidWorksEngine;
using Microsoft.Extensions.Options;

namespace FlangeDesigner.Spec.Services
{
    public class ProjectDao
    {
        private string _connectionString;

        public ProjectDao(IOptions<DapperRepositoryOptions> repositoryOptions)
        {
            _connectionString = repositoryOptions.Value.SqLite;
        }
        
        public List<Project> FindProjectsByName(string projectName)
        {
            using (IDbConnection cnn = new SQLiteConnection(_connectionString))
            {
                var projectParameters = new DynamicParameters();
                projectParameters.Add("@projectName", projectName, DbType.String);
                
                var projectResults = cnn
                    .Query<Project>("SELECT * FROM projects WHERE Name = @projectName", projectParameters)
                    .ToList();

                return projectResults;
            }
        }

        public List<Configuration> FindConfigurationsByProjectId(int projectId)
        {
            using (IDbConnection cnn = new SQLiteConnection(_connectionString))
            {
                var configurationsParameters = new DynamicParameters();
                configurationsParameters.Add("@ProjectId", projectId, DbType.String);
                
                var configurationsResults = cnn
                    .Query<Configuration>("SELECT * FROM configurations WHERE ProjectId = @ProjectId", configurationsParameters)
                    .ToList();
                
                return configurationsResults;
            }
        }
        
        public List<Project> FindProjectsByFilePath(string filePath)
        {
            using (IDbConnection cnn = new SQLiteConnection(_connectionString))
            {
                var projectParameters = new DynamicParameters();
                projectParameters.Add("@Path", filePath, DbType.String);
                
                var projectResults = cnn
                    .Query<Project>("SELECT * FROM projects WHERE Path = @Path", projectParameters)
                    .ToList();

                return projectResults;
            }
        }
    }
}