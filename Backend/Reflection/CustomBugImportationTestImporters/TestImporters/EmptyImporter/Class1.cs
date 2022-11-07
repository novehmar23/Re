using System;
using System.Collections.Generic;
using CustomBugImportation;

namespace EmptyImporter
{
    public class Importer : IBugImporter
    {
        public ImporterInfo GetImporterInfo()
        {
            return new ImporterInfo
            {
                ImporterName = "Empty Importer",
                Params = new List<Parameter> { }
            };
        }

        public List<ImportedBug> ImportBugs(List<Parameter> parameters)
        {
            return new List<ImportedBug> { };
        }
    }
}
