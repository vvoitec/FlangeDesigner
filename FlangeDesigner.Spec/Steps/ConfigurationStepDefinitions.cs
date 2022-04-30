using System.Linq;
using FlangeDesigner.AbstractEngine;
using FlangeDesigner.Main.Application.Project;
using FlangeDesigner.Main.Domain.Entities;
using FlangeDesigner.Main.Infrastructure.Persistence;
using FlangeDesigner.Spec.Services;
using Microsoft.Extensions.Options;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace FlangeDesigner.Spec.Steps
{
    [Binding]
    public class ConfigurationStepDefinitions
    {
        private IProjectService _projectService;
        private readonly ProjectDao _projectDao;
        private string _projectName;

        public ConfigurationStepDefinitions(
            IOptions<DapperRepositoryOptions> repositoryOptions, 
            IProjectService projectService,
            ProjectDao projectDao
            )
        {
            _projectService = projectService;
            _projectDao = projectDao;
        }

        [Given(@"Project named (.*) is loaded from global path (.*)")]
        public void GivenProjectNamedIsLoadedFromGlobalPath(string projectName, string filePath)
        {
            _projectService.LoadProject(filePath);
            _projectName = projectName;
        }
        
        [Then(@"Project named (.*) contains configuration named (.*) with following dimensions")]
        public void ProjectNamedContainsFollowingConfiguration(string projectName, string configurationName, Table table)
        {
            var expectedConfiguration = Configuration.FromDimensions(table.CreateSet<Dimension>(), configurationName);
            
            // var project = _projectDao.FindProjectsByName(projectName).First();
            // Assert.NotNull(project);
            //
            // var configurations = project._engine.Model.ProjectConfigurations.Select(Configuration.FromDimensions);

            var configurations = _projectService.Project.Configurations;

            Assert.Contains(configurations, configuration => configuration.Dimensions.Equals(expectedConfiguration.Dimensions));
        }

        [When(@"User adds configuration named (.*) to project named (.*)")]
        public void WhenUserAddsAFollowingConfiguration(string configurationName, string projectName, Table table)
        {
            var configurationToAdd = Configuration.FromDimensions(table.CreateSet<Dimension>(), configurationName);

            _projectService.UpdateConfiguration(configurationToAdd);
        }
    }
}