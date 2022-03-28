using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Dapper;
using FlangeDesigner.AbstractEngine;
using FlangeDesigner.Main.Application.Project;
using FlangeDesigner.Main.Domain.Entities;
using FlangeDesigner.Main.Domain.Repositories;
using FlangeDesigner.Main.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using NuGet.Frameworks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace FlangeDesigner.Spec.Steps
{
    [Binding]
    public class ProjectManagementStepDefinitions
    {
        private readonly IProjectService _projectFacade;
        private string _filePath;
        private string _projectName;
        private readonly string _connectionString;

        public ProjectManagementStepDefinitions(IOptions<DapperRepositoryOptions> repositoryOptions, IProjectService projectFacade)
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
                var projectParameters = new DynamicParameters();
                projectParameters.Add("@Path", _filePath, DbType.String);
                
                var projectResults = cnn
                    .Query<Project>("SELECT * FROM projects WHERE Path = @Path", projectParameters)
                    .ToList();
                
                var project = projectResults[0];
                
                var configurationsParameters = new DynamicParameters();
                configurationsParameters.Add("@ProjectId", project.Id, DbType.String);
                
                var configurationsResults = cnn
                    .Query<Configuration>("SELECT * FROM configurations WHERE ProjectId = @ProjectId", configurationsParameters)
                    .ToList();
                
                Assert.NotEmpty(configurationsResults);
                
            }
        }

        [Then(@"Project configurations are created")]
        public void ThenProjectConfigurationsAreCreated()
        {
            var projects = FindProjectsByFilePath(_filePath);
            Assert.NotEmpty(projects);
            Assert.NotNull(projects.First().Id);
            Assert.NotEmpty(FindConfigurationsByProjectId((int) projects.First().Id));
      
        }

        [Given(@"Project named (.*) contains following configuration")]
        [Then(@"Project named (.*) contains following configuration")]
        public void ProjectNamedContainsFollowingConfiguration(string projectName, Table table)
        {
            var expectedConfiguration = Configuration.FromDimensions(table.CreateSet<Dimension>());
            
            var projects = FindProjectsByName(projectName);
            Assert.NotEmpty(projects);
            Assert.NotNull(projects.First().Id);
            
            var configurations = FindConfigurationsByProjectId((int) projects.First().Id);

            Assert.Contains(configurations, configuration => configuration.Dimensions.Equals(expectedConfiguration.Dimensions));
        }

        [When(@"User adds a following configuration to project named (.*)")]
        public void WhenUserAddsAFollowingConfiguration(string projectName)
        {
            ScenarioContext.StepIsPending();
        }

        private List<Project> FindProjectsByFilePath(string filePath)
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
        
        private List<Project> FindProjectsByName(string projectName)
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

        private List<Configuration> FindConfigurationsByProjectId(int projectId)
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
    }
}