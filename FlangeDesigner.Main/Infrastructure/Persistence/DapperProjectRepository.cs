using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using FlangeDesigner.AbstractEngine;
using FlangeDesigner.Main.Domain.Entities;
using FlangeDesigner.Main.Domain.Repositories;
using Microsoft.Extensions.Options;

namespace FlangeDesigner.Main.Infrastructure.Persistence
{
    public class DapperProjectRepository : IProjectRepository
    {
        private readonly string _connectionString;
        
        public DapperProjectRepository(IOptions<DapperRepositoryOptions> options)
        {
            _connectionString = options.Value.SqLite;
        }
        public List<Project> FindAll()
        {
            using (IDbConnection cnn = new SQLiteConnection(_connectionString))
            {
                return cnn
                    .Query<Project>("SELECT * FROM projects", new DynamicParameters())
                    .ToList();
            }
        }

        public void Save(Project project)
        {
            using (IDbConnection cnn = new SQLiteConnection(_connectionString))
            {
                var id = cnn.Query<int>( @"
                                                    INSERT INTO projects(Name, Path)
                                                    VALUES (@Name, @Path);
                                                    SELECT last_insert_rowid() FROM projects;
                                                    ",
                    new { Name = project.Name, Path = project.Path }).First();
                
                foreach (Configuration projectConfiguration in project.Configurations)
                {
                    cnn.Query<int>( @"
                                                    INSERT INTO configurations(ProjectId, Dimensions)
                                                    VALUES (@ProjectId, @Dimensions);",
                        new { ProjectId = id, Dimensions = projectConfiguration.Dimensions });   
                }

                project.Id = id;
            }
        }
    }
}