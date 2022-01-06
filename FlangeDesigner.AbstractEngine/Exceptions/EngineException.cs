using System;

namespace FlangeDesigner.AbstractEngine.Exceptions
{
    public class EngineException : Exception
    {
        public EngineException(string message): base(message)
        {
            
        }
    }
}