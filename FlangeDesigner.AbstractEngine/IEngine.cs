namespace FlangeDesigner.AbstractEngine
{
    public interface IEngine
    {
        public IEngine Run();
        public IEngine LoadModelFromFilePath(string path);
        public IModel Model { get; }
    }
}