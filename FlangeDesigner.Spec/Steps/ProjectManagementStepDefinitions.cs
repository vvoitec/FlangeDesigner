using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Dapper;
using FlangeDesigner.Main.Application.Project;
using FlangeDesigner.Main.Domain.Entities;
using FlangeDesigner.Main.Domain.Repositories;
using FlangeDesigner.Main.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TechTalk.SpecFlow;
using Xunit;

namespace FlangeDesigner.Spec.Steps
{
    [Binding]
    public class ProjectManagementStepDefinitions
    {
        private readonly IProjectFacade _projectFacade;
        private string _filePath;
        private string _projectName;
        private readonly string _connectionString;

        public ProjectManagementStepDefinitions(IOptions<DapperRepositoryOptions> repositoryOptions, IProjectFacade projectFacade)
        {
            _connectionString = repositoryOptions.Value.SqLite;
            _projectFacade = projectFacade;
        }
        
        [Given(@"Global path to existing SolidWorks project is (.*)")]
        public void GivenGlobalPathToExistingSolidWorksProjectIs(string filePath)
        {
            _filePath = filePath;
            Assert.True(File.Exists(_filePath));
        }

        [Given(@"SolidWorks project name is (.*)")]
        public void GivenSolidWorksProjectNameIs(string projectName)
        {
            _projectName = projectName;
        }

        [When(@"User loads the project")]
        public void WhenUserLoadsTheProject()
        {
            _projectFacade.LoadProject(_filePath);
        }

        [Then(@"New project is created")]
        public void ThenNewProjectIsCreated()
        {
            using (IDbConnection cnn = new SQLiteConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Path", _filePath, DbType.String);
                
                var result = cnn
                    .Query<Project>("SELECT * FROM projects WHERE Path = @Path", parameters)
                    .ToList();
                
                Assert.NotEmpty(result);
            }
        }
    }
}