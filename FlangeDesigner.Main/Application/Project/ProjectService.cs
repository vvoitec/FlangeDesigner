using FlangeDesigner.AbstractEngine;
using FlangeDesigner.Main.Domain.Repositories;

namespace FlangeDesigner.Main.Application.Project
{
    public class ProjectService : IProjectService
    {
        private readonly IEngine _engine;
        private readonly IProjectRepository _repository;

        public ProjectService(IEngine engine, IProjectRepository repository)
        {
            _engine = engine;
            _repository = repository;
        }

        public void LoadProject(string filePath)
        {
            var project = Domain.Entities.Project.Create(_engine);
            project.Load(filePath);

            _repository.Save(project);
        }
    }
}