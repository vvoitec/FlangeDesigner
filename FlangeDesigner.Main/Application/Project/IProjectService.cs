using FlangeDesigner.Main.Domain.Entities;

namespace FlangeDesigner.Main.Application.Project
{
    public interface IProjectService
    {
        public Domain.Entities.Project? Project { get; }
        public void LoadProject(string filePath);
        public void UpdateConfiguration(Configuration configuration);
    }
}