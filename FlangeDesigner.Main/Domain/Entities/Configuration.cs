using System.Collections.Generic;
using FlangeDesigner.AbstractEngine;
using FlangeDesigner.Main.Infrastructure;
using Newtonsoft.Json;

namespace FlangeDesigner.Main.Domain.Entities
{
    public class Configuration
    {
        public string Dimensions { get; }
        
        public Configuration()
        {}

        public IEnumerable<Dimension> ListDimensions()
        {
            return JsonConvert.DeserializeObject<IEnumerable<Dimension>>(Dimensions, new LengthJsonConverter()); 
        }

        private Configuration(IEnumerable<Dimension> dimensions)
        {
            Dimensions = JsonConvert.SerializeObject(dimensions);
        }

        public static Configuration FromDimensions(IEnumerable<Dimension> dimensions)
        {
            return new Configuration(dimensions);
        }
    }
}