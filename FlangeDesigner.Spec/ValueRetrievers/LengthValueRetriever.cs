using System;
using System.Collections.Generic;
using FlangeDesigner.AbstractEngine;
using TechTalk.SpecFlow.Assist;

namespace FlangeDesigner.Spec.ValueRetrievers
{
    public class LengthValueRetriever : IValueRetriever
    {
        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return Int32.TryParse(keyValuePair.Value, out int number);
        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return Length.of(Int32.Parse(keyValuePair.Value));
        }
    }
}
