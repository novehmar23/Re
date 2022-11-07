using CustomBugImportation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
namespace CustomBugImporter
{
    public class CustomBugImporterManager : ICustomBugImporter
    {
        private string folderPathForImporters;
        public const string customImporterFolderSettingsName = "CustomImporterFolder";
        public CustomBugImporterManager()
        {
            folderPathForImporters = ReflectionPath.Path;
        }

        // Path parameter is only for testing, folderPathForImporters is use in production
        public List<ImporterInfo> GetAvailableImportersInfo(string path = null)
        {
            if (path == null)
                path = folderPathForImporters;
            List<ImporterInfo> importerInfos = new List<ImporterInfo>();

            var importers = GetImporters(path);
            foreach (IBugImporter importer in importers)
            {
                try
                {
                    importerInfos.Add(importer.GetImporterInfo());
                }
                catch (Exception e)
                {
                    if (e is CustomImporterException)
                        throw; // CustomImporter exception is re-throw
                }
            }
            return importerInfos;
        }

        // Path parameter is only for testing, folderPathForImporters is use in production
        public List<ImportedBug> ImportBugs(string importerName, List<Parameter> parameters, string path = null)
        {
            if (path == null)
                path = folderPathForImporters;

            List<ImportedBug> bugs = new List<ImportedBug>();

            var importers = GetImporters(path);
            foreach (IBugImporter importer in importers)
            {
                if (importer.GetImporterInfo().ImporterName == importerName)
                {
                    try
                    {
                        bugs = importer.ImportBugs(parameters);
                        return bugs;
                    }
                    catch (Exception e)
                    {
                        if (e is CustomImporterException)
                            throw; // CustomImporter exception is re-throw
                    }
                }

            }
            return bugs;
        }
        private List<IBugImporter> GetImporters(string path)
        {
            List<IBugImporter> importerInfos = new List<IBugImporter>();

            string[] filePaths = Directory.GetFiles(path);
            foreach (string filePath in filePaths)
            {
                FileInfo dllFile = new FileInfo(filePath);
                Assembly assembly = Assembly.LoadFile(dllFile.FullName);
                foreach (Type type in assembly.GetTypes())
                {
                    try
                    {
                        if (typeof(IBugImporter).IsAssignableFrom(type))
                        {
                            IBugImporter provider = (IBugImporter)Activator.CreateInstance(type);
                            importerInfos.Add(provider);
                        }

                    }
                    catch (Exception e)
                    {
                        if (e is CustomImporterException)
                            throw; // CustomImporter exception is re-throw
                    }
                }
            }
            return importerInfos;
        }

        public void setPath(string path)
        {
            this.folderPathForImporters = path;
        }
    }
}
