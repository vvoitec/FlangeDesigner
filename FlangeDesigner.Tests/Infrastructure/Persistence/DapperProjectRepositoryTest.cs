using FlangeDesigner.Main.Domain.Entities;
using FlangeDesigner.Main.Domain.Repositories;
using Xunit;

namespace FlangeDesigner.Tests.Infrastructure.Persistence
{
    public class DapperProjectRepositoryTest
    {
        private readonly IProjectRepository _projectRepository;

        public DapperProjectRepositoryTest(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        
        [Fact]
        public void TestFindAll()
        {
            var projects = _projectRepository.FindAll();
            Assert.NotEmpty(projects);
        }

        [Fact]
        public void TestSave()
        {
            var project = new Project() {Name = "test", Path = "test"};
            _projectRepository.Save(project);
        }
    }
}