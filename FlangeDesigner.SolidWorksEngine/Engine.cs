using FlangeDesigner.AbstractEngine;
using FlangeDesigner.AbstractEngine.Exceptions;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace FlangeDesigner.SolidWorksEngine
{
    public class Engine : IEngine
    {
        private SldWorks? _swApp = null;
        private Model? _model = null;
        private const string ProgId = "SldWorks.Application";
        private int _fileerror;
        private int _filewarning;
        
        public IEngine Run()
        {
            var progType = System.Type.GetTypeFromProgID(ProgId);

            _swApp = System.Activator.CreateInstance(progType) as SldWorks;
            if (null == _swApp)
            {
                throw new EngineException("Failed to create SldWorks instance");
            }
            _swApp.Visible = true;

            return this;
        }

        public IEngine LoadModelFromFilePath(string path)
        {
            _model = new Model(
                    _swApp.OpenDoc6(path, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref _fileerror, ref _filewarning)
                );
            if (null == _model)
            {
                throw new EngineException("Failed to load model from filepath");
            }

            return this;
        }

        public IModel Model
        {
            get {
                if (null == _model)
                {
                    throw new EngineException("Model is not loaded");
                }
                return _model;
            }
        }
    }
}