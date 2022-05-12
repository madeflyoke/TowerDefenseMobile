namespace TD.Services.Firebase
{
    public struct EventParameter
    {
        public LogEventParameterName name;
        public object value;

        public EventParameter(LogEventParameterName name, object value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
