using System;

namespace FlangeDesigner.Main.Domain
{
    public class ProjectException : ApplicationException
    {
        public ProjectException(string message) : base(message)
        {
            
        }
    }
}