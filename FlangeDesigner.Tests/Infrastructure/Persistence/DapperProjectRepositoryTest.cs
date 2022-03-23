using FlangeDesigner.AbstractEngine;
using FlangeDesigner.Main.Domain.Entities;
using FlangeDesigner.Main.Domain.Repositories;
using Moq;
using Xunit;

namespace FlangeDesigner.Tests.Infrastructure.Persistence
{
    public class DapperProjectRepositoryTest
    {
        private readonly IProjectRepository _projectRepository;
        private Mock<IEngine> _engineMock;

        public DapperProjectRepositoryTest(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
            _engineMock = new Mock<IEngine>();
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
            var project = Project.Create(_engineMock.Object);
            _projectRepository.Save(project);
        }
    }
}