using System;
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
        public readonly IEngine? _engine = null;
        public int? Id { get; set; } = null;
        public string Name { get; private set; }
        public string Path { get; private set; }

        public ICollection<Configuration> Configurations { get; private set; }

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
            ValidateEngine();
            
            var model = _engine
                .Run()
                .LoadModelFromFilePath(filePath)
                .Model;
            
            Path = filePath;
            Name = model.Name;
            Configurations = model.ProjectConfigurations
                .Select(Configuration.FromDimensions)
                .ToList();

            return model;
        }

        public void AddConfiguration(Configuration configuration)
        {
            ValidateEngine();
            
            Configurations.Add(configuration);
            var dimensions = configuration.ListDimensions();
            _engine.Model.AddConfiguration(dimensions);
        }

        private void ValidateEngine()
        {
            if (null == _engine)
            {
                throw new ProjectException("Missing engine");
            }
        }
    }
}