using System;
using System.Collections.Generic;
using CustomBugImportation;

namespace EmptyImporter2
{
    public class Importer : IBugImporter
    {
        public ImporterInfo GetImporterInfo()
        {
            return new ImporterInfo
            {
                ImporterName = "Empty Importer 2",
                Params = new List<Parameter> { }
            };
        }

        public List<ImportedBug> ImportBugs(List<Parameter> parameters)
        {
            return new List<ImportedBug> { };
        }
    }
}

