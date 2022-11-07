using CustomBugImportation;
using System;
using System.Collections.Generic;

namespace TextImporter
{
    public class Importer : IBugImporter
    {
        private ImporterInfo info;
        private const string fileExtension = ".txt";
        const string pathParameterName = "Folder path";
        const string fileNameParameterName = "File Name";
        public Importer()
        {
            info = new ImporterInfo()
            {
                ImporterName = "Positional Text Importer",
                Params = new List<Parameter>
                {
                    new Parameter(){
                    Name = pathParameterName,
                    Type = ParameterType.STRING,
                },
                new Parameter(){
                    Name = fileNameParameterName,
                    Type = ParameterType.INTEGER,
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

            string fullPath = GetFilePath(parameters);
            List<ImportedBug> bugs = ImportBugsFromPath(fullPath);
            return bugs;

        }

        private string GetFilePath(List<Parameter> parameters)
        {
            string path = parameters.Find(p => p.Name == pathParameterName).Value;
            string fileNameText = parameters.Find(p => p.Name == fileNameParameterName).Value;

            int fileName;
            bool parsed = Int32.TryParse(fileNameText, out fileName);
            if (!parsed)
                throw new CustomImporterException("File Name must be an integer");

            string fullPath = path + fileName + fileExtension;
            return fullPath;
        }

        private List<ImportedBug> ImportBugsFromPath(string fullPath)
        {
            string[] lines;
            try
            {
                lines = System.IO.File.ReadAllLines(fullPath);
            }
            catch (Exception)
            {
                throw new CustomImporterException("Error reading file");
            }

            List<ImportedBug> bugs;
            try
            {
                bugs = ImportBugsFromLines(lines);
            }
            catch (Exception)
            {
                throw new CustomImporterException("Error importing bugs");
            }

            return bugs;
        }

        private List<ImportedBug> ImportBugsFromLines(string[] lines)
        {
            List<ImportedBug> bugs = new List<ImportedBug>();

            const int projectNameStart = 0;
            const int projectNameLength = 30;
            const int NameStart = 34;
            const int NameLength = 60;
            const int DescriptionStart = 94;
            const int DescriptionLength = 150;
            const int VersionStart = 244;
            const int VersionLength = 10;
            const int isActivePos = 254;
            const int TimeStart = 255;
            const int TimeLength = 4;
            foreach (string line in lines)
            {
                bugs.Add(new ImportedBug()
                {
                    ProjectName = line.Substring(projectNameStart, projectNameLength).Trim(),
                    Name = line.Substring(NameStart, NameLength).Trim(),
                    Description = line.Substring(DescriptionStart, DescriptionLength).Trim(),
                    Version = line.Substring(VersionStart, VersionLength).Trim(),
                    IsActive = (line[isActivePos].ToString()) == "1",
                    Time = Int32.Parse(line.Substring(TimeStart, TimeLength).Trim()),
                });
            }
            return bugs;
        }
    }
}
