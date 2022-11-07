namespace CustomBugImportation
{
    public struct Parameter
    {
        public string Name { get; set; }
        public ParameterType Type { get; set; }
        public string Value { get; set; }



        // Only use for testing
        public override bool Equals(object obj)
        {
            if (!(obj is Parameter))
                return false;
            Parameter p = (Parameter)obj;
            return this.Name.Equals(p.Name) && this.Type.Equals(p.Type);
        }

    }

}
