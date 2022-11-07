using System;
using System.Collections.Generic;
using CustomBugImportation;

namespace FailedImporter
{
    public class Importer : IBugImporter
    {
        public ImporterInfo GetImporterInfo()
        {
            return new ImporterInfo
            {
                ImporterName = "FailedImporter",
                Params = new List<Parameter> { }
            };
        }

        public List<ImportedBug> ImportBugs(List<Parameter> parameters)
        {
            throw new CustomImporterException("Wrong parameters");
        }
    }
}
