namespace FlangeDesigner.AbstractEngine
{
    public class Dimension
    {
        private Length _value;
        public string Key { get; }

        public int Value { get; }

        public Dimension(string key, Length value)
        {
            Key = key;
            _value = value;
            Value = _value.Value;
        }
    }

    public class Length
    {
        public int Value { get; }
        
        private Length(int value)
        {
            Value = value;
        }

        public static Length of(int value)
        {
            return new Length(value);
        }
    }
}