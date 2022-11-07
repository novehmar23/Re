using CustomBugImportation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JSONImporter
{
    public class Importer : IBugImporter
    {
        private ImporterInfo info;
        public Importer()
        {
            info = new ImporterInfo()
            {
                ImporterName = "JSON Importer",
                Params = new List<Parameter>
                {
                    new Parameter(){
                        Name = "path",
                        Type = ParameterType.STRING
                    }
                }
            };
        }

        public ImporterInfo GetImporterInfo()
        {
            return info;
        }

        public List<ImportedBug> ImportBugs(List<Parameter> parameters)
        {
            string fileName = parameters.Find(p => p.Name == "path").Value;
            string jsonString = GetJSONString(fileName);
            var importedBugs = DeserializeBugs(jsonString);

            return importedBugs;
        }

        private string GetJSONString(string fileName)
        {
            string jsonString = "";
            try
            {
                jsonString = File.ReadAllText(fileName);
            }
            catch (Exception e) when (e is DirectoryNotFoundException || e is FileNotFoundException)
            {
                throw new CustomImporterException("Error acceding File");
            }
            return jsonString;
        }

        private List<ImportedBug> DeserializeBugs(string jsonString)
        {
            var importedBugs = new List<ImportedBug>();
            try
            {
                importedBugs = JsonSerializer.Deserialize<List<ImportedBug>>(jsonString);
            }
            catch (JsonException)
            {
                throw new CustomImporterException("Error reading JSON");
            }
            return importedBugs;
        }
    }
}
