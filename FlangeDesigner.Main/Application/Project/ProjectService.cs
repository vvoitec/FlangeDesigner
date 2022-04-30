using FlangeDesigner.AbstractEngine;
using FlangeDesigner.Main.Domain.Entities;
using FlangeDesigner.Main.Domain.Repositories;

namespace FlangeDesigner.Main.Application.Project
{
    public class ProjectService : IProjectService
    {
        private readonly IEngine _engine;
        private readonly IProjectRepository _repository;
        public Domain.Entities.Project? Project { get; private set; }

        public ProjectService(IEngine engine, IProjectRepository repository)
        {
            _engine = engine;
            _repository = repository;
        }

        public void LoadProject(string filePath)
        {
            Project = Domain.Entities.Project.Create(_engine);
            
            if (null == Project)
            {
                throw new RuntimeException("Project failed to load");
            }
            
            Project.Load(filePath);
            _repository.Save(Project);
        }

        public void UpdateConfiguration(Configuration configuration)
        {
            if (null == Project)
            {
                throw new RuntimeException("Cannot update configuration because project is not loaded");
            }

            Project.AddConfiguration(configuration);
            _repository.Save(Project);
        }
    }
}