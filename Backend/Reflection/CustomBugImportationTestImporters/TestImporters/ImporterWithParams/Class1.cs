using System;
using System.Collections.Generic;
using CustomBugImportation;

namespace ImporterWithParams
{
    public class Importer : IBugImporter
    {
        public ImporterInfo GetImporterInfo()
        {
            return new ImporterInfo
            {
                ImporterName = "ImporterWithParams",
                Params = new List<Parameter> { }
            };
        }

        public List<ImportedBug> ImportBugs(List<Parameter> parameters)
        {
            string path = parameters.Find(p => p.Name == "path" && p.Type == ParameterType.STRING).Value;
            int port = Int32.Parse(parameters.Find(p => p.Name == "port" && p.Type == ParameterType.INTEGER).Value);
            if (path == null || port == 0)
                throw new Exception("Path or port parameter is missing");
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
                },
                new ImportedBug(){
                    Name = "Bug3",
                    Description = "The third bug",
                    IsActive = false,
                    CompletedById =  2,
                    Version = "1.0.0"
                }
    };
        }
    }
}
