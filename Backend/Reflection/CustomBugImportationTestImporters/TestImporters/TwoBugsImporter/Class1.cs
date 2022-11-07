using System;
using System.Collections.Generic;
using CustomBugImportation;

namespace TwoBugsImporter
{
    public class Importer : IBugImporter
    {
        public ImporterInfo GetImporterInfo()
        {
            return new ImporterInfo
            {
                ImporterName = "TwoBugsImporter",
                Params = new List<Parameter> { }
            };
        }

        public List<ImportedBug> ImportBugs(List<Parameter> parameters)
        {
            return new List<ImportedBug>
            {
                new ImportedBug(){
                    Name = "Bug1",
                    Description = "The first bug",
                    IsActive = true,
                    ProjectName =  "The mega project"
                },
                new ImportedBug(){
                    Name = "Bug2",
                    Description = "The second bug",
                    Time = 67,
                    ProjectId =  2
                }
    };
        }
    }
}
