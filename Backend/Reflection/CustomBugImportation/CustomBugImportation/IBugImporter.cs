using System.Collections.Generic;

namespace CustomBugImportation
{
    public interface IBugImporter
    {
        public ImporterInfo GetImporterInfo();

        public List<ImportedBug> ImportBugs(List<Parameter> parameters);
    }
}
