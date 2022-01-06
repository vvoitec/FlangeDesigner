using FlangeDesigner.AbstractEngine;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace FlangeDesigner.SolidWorksEngine
{
    public class Model : IModel
    {
        private readonly IEngine _engine;
        private ModelDoc2 _swDoc;
        private int _fileerror;
        private int _filewarning;
        public string Name { get; }
        
        public Model(ModelDoc2 swDoc)
        {
            _swDoc = swDoc;
            Name = _swDoc.GetTitle();
        }
    }
}