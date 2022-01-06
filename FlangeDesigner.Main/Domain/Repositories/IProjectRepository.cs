using System.Collections.Generic;
using FlangeDesigner.Main.Domain.Entities;

namespace FlangeDesigner.Main.Domain.Repositories
{
    public interface IProjectRepository
    {
        public List<Project> FindAll();

        public void Save(Project project);
    }
}