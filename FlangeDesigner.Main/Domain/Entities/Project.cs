using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Security.Cryptography;
using FlangeDesigner.AbstractEngine;
using FlangeDesigner.SolidWorksEngine;

namespace FlangeDesigner.Main.Domain.Entities
{
    public class Project
    {
        private readonly IEngine? _engine = null;
        public int? Id { get; set; } = null;
        public string Name { get; private set; }
        public string Path { get; private set; }

        public ICollection<Configuration> Configurations { get; set; }

        public Project()
        { }

        private Project(IEngine engine)
        {
            _engine = engine;
        }

        public static Project Create(IEngine engine)
        {
            return new Project(engine);
        }

        public IModel Load(string filePath)
        {
            if (null == _engine)
            {
                throw new ProjectException("Missing engine");
            }
            
            var model = _engine
                .Run()
                .LoadModelFromFilePath(filePath)
                .Model;
            
            Path = filePath;
            Name = model.Name;
            Configurations = model.ProjectConfigurations
                .Select(configuration => Configuration.FromDimensions(configuration))
                .ToList();

            return model;
        }
    }
}