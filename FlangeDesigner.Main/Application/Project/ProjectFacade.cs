namespace FlangeDesigner.Main.Application.Project
{
    public class ProjectFacade : IProjectFacade
    {
        private readonly ProjectLoader _loader;

        public ProjectFacade(ProjectLoader loader)
        {
            _loader = loader;
        }
        
        public void LoadProject(string filePath)
        {
            _loader.Load(filePath);
        }
    }
}