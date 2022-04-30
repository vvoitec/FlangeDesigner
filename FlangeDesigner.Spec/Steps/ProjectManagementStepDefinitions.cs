using System.Data;
using System.IO;
using System.Linq;
using Dapper;
using FlangeDesigner.Main.Application.Project;
using FlangeDesigner.Spec.Services;
using TechTalk.SpecFlow;
using Xunit;

namespace FlangeDesigner.Spec.Steps
{
    [Binding]
    public class ProjectManagementStepDefinitions
    {
        private readonly IProjectService _projectService;
        private readonly ProjectDao _projectDao;
        private string _filePath;
        private string _projectName;

        public ProjectManagementStepDefinitions(
            IProjectService projectService,
            ProjectDao projectDao
            )
        {
            _projectService = projectService;
            _projectDao = projectDao;
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
            _projectService.LoadProject(_filePath);
        }

        [Then(@"New project is created")]
        public void ThenNewProjectIsCreated()
        {
            var project = _projectDao.FindProjectsByFilePath(_filePath).First();
                
            var configurationsParameters = new DynamicParameters();
            configurationsParameters.Add("@ProjectId", project.Id, DbType.String);

            var configurationsResults = _projectDao.FindConfigurationsByProjectId((int) project.Id);
                
            Assert.NotEmpty(configurationsResults);
        }

        [Then(@"Project configurations are created")]
        public void ThenProjectConfigurationsAreCreated()
        {
            var projects = _projectDao.FindProjectsByFilePath(_filePath);
            Assert.NotEmpty(projects);
            Assert.NotNull(projects.First().Id);
            Assert.NotEmpty(_projectDao.FindConfigurationsByProjectId((int) projects.First().Id));
      
        }
    }
}