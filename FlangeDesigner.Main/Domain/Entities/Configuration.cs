using System.Collections.Generic;
using FlangeDesigner.AbstractEngine;
using Newtonsoft.Json;

namespace FlangeDesigner.Main.Domain.Entities
{
    public class Configuration
    {
        public string Dimensions { get; }
        
        public Configuration()
        {}

        public IModelConfiguration ListDimensions()
        {
            return JsonConvert.DeserializeObject<IModelConfiguration>(Dimensions); 
        }

        private Configuration(IModelConfiguration dimensions)
        {
            Dimensions = JsonConvert.SerializeObject(dimensions);
        }

        public static Configuration FromDimensions(IModelConfiguration dimensions)
        {
            return new Configuration(dimensions);
        }
    }
}