using System.Collections;
using System.Collections.Generic;

namespace FlangeDesigner.AbstractEngine
{
    public interface IModelConfiguration : IEnumerable<Dimension>
    {
        public string Name { get; set; }
    }
}
