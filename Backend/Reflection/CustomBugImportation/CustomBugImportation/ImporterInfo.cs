using System.Collections.Generic;
using System.Linq;

namespace CustomBugImportation
{
    public struct ImporterInfo
    {
        public string ImporterName { get; set; }
        public List<Parameter> Params { get; set; }


        // Only use for testing
        public override bool Equals(object obj)
        {
            if (!(obj is ImporterInfo))
                return false;
            ImporterInfo p = (ImporterInfo)obj;
            return this.ImporterName.Equals(p.ImporterName) && this.Params.All(p.Params.Contains);
        }
    }
}
