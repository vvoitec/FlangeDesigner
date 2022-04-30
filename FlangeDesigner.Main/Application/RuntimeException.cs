using System;

namespace FlangeDesigner.Main.Application
{
    public class RuntimeException : SystemException
    {
        public RuntimeException(string message) : base (message)
        {
            
        }
    }
}