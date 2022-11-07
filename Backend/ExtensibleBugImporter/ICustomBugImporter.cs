using CustomBugImportation;
using System.Collections.Generic;

namespace CustomBugImporter
{
    public interface ICustomBugImporter
    {
        public List<ImportedBug> ImportBugs(string importerName, List<Parameter> parameters, string path = null);

        public List<ImporterInfo> GetAvailableImportersInfo(string path = null);
    }
}
