using System.Collections;
using System.Collections.Generic;

namespace FlangeDesigner.AbstractEngine
{
    public interface IModel
    {
        public string Name { get; }
        
        public ICollection<IEnumerable<Dimension>> ProjectConfigurations { get; }

        public void AddConfiguration(IEnumerable<Dimension> modelConfiguration);
    }
}