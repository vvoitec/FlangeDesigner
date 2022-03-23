using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FlangeDesigner.AbstractEngine
{
    public interface IModel
    {
        public string Name { get; }
        
        public ICollection<IModelConfiguration> ProjectConfigurations { get; }
    }
}