using FlangeDesigner.AbstractEngine;
using FlangeDesigner.Main.Domain.Repositories;

namespace FlangeDesigner.Main.Application.Project
{
    public class ProjectLoader
    {
        private readonly IEngine _engine;
        private readonly IProjectRepository _repository;

        public ProjectLoader(IEngine engine, IProjectRepository repository)
        {
            _engine = engine;
            _repository = repository;
        }
        
        public void Load(string filePath)
        {
            var model = _engine
                .Run()
                .LoadModelFromFilePath(filePath)
                .Model;
            
            _repository.Save(new Domain.Entities.Project()
            {
                Name = model.Name,
                Path = filePath
            });
        }
    }
}