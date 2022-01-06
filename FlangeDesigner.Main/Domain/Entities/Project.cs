using System.Security.Cryptography;

namespace FlangeDesigner.Main.Domain.Entities
{
    public class Project
    {
        public int? Id { get; set; } = null;
        public string Name { get; set; }
        public string Path { get; set; }
    }
}