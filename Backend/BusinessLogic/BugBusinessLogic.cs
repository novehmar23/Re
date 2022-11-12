using BugParser;
using BusinessLogicInterfaces;
using CustomBugImportation;
using CustomBugImporter;
using Domain;
using Domain.Utils;
using DTO;
using RepositoryInterfaces;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class BugBusinessLogic : IBugBusinessLogic
    {
        public IBugDataAccess BugDataAccess { get; set; }
        public IBugTypeDataAccess BugTypeDataAccess { get; set; }

        public BugBusinessLogic(IBugDataAccess newBugDataAccess, IBugTypeDataAccess newBugTypeDataAccess)
        {
            BugDataAccess = newBugDataAccess;
            BugTypeDataAccess = newBugTypeDataAccess;
        }

        public BugBusinessLogic(IBugDataAccess newBugDataAccess)
        {
            BugDataAccess = newBugDataAccess;
        }

        public BugDTO GetById(int idBug)
        {
            Bug bug = BugDataAccess.GetById(idBug);
            return new BugDTO(bug);
        }

        public IEnumerable<BugDTO> GetAll(string token)
        {
            List<Bug> bugs = (List<Bug>)BugDataAccess.GetAll(token);
            return bugs.ConvertAll(b => new BugDTO(b));
        }

        public BugDTO Add(BugDTO bugDTO)
        {

            bugDTO.Type = null;
            Bug bug = bugDTO.ConvertToDomain();
            bug.Validate();
            BugDataAccess.Create(bug);
            BugDataAccess.Save();
            return bugDTO;
        }

        public BugDTO Update(int Id, BugDTO bugDTO)
        {
            Bug bugMod = bugDTO.ConvertToDomain();
            bugMod.Validate();
            return new BugDTO(BugDataAccess.Update(Id, bugMod));
        }


        public ResponseMessage Delete(int Id)
        {
            return BugDataAccess.Delete(Id);

        }

        public void ImportBugs(string path, ImportCompany format, IParserFactory factory = null)
        {
            // This is to allow the tests to include their own mock factory
            if (factory == null)
                factory = new ParserFactory();
            IBugParser parser = factory.GetBugParser(format);
            List<Bug> bugsToImport = parser.GetBugs(path);
            foreach (var bug in bugsToImport)
            {
                bug.Validate();
                BugDataAccess.Create(bug);
            }
        }

        public List<ImporterInfo> GetCustomImportersInfo(ICustomBugImporter importerManager = null)
        {
            // This is to allow the tests to include their own mock custom importer
            if (importerManager == null)
                importerManager = new CustomBugImporterManager();
            List<ImporterInfo> importersInfo = importerManager.GetAvailableImportersInfo();
            return importersInfo;

        }

        public void ImportBugsCustom(string importerName, List<Parameter> parameters, ICustomBugImporter importerManager = null)
        {
            // This is to allow the tests to include their own mock custom importer
            if (importerManager == null)
                importerManager = new CustomBugImporterManager();

            List<ImportedBug> bugsToImport = importerManager.ImportBugs(importerName, parameters);
            foreach (var importedBug in bugsToImport)
            {
                Bug convertedBug = convertImportedBugToBug(importedBug);
                convertedBug.Validate();
                BugDataAccess.Create(convertedBug);
            }

        }

        private Bug convertImportedBugToBug(ImportedBug importedBug)
        {
            return new Bug()
            {
                Name = importedBug.Name,
                Description = importedBug.Description,
                Version = importedBug.Version,
                CompletedById = importedBug.CompletedById,
                IsActive = importedBug.IsActive,
                ProjectId = importedBug.ProjectId,
                ProjectName = importedBug.ProjectName,
                Time = importedBug.Time
            };
        }

        public BugDTO ResolveBug(int id, string token)
        {
            return new BugDTO(BugDataAccess.ResolveBug(id, token));
        }

        public BugDTO UnresolveBug(int id, string token)
        {
            return new BugDTO(BugDataAccess.UnresolveBug(id, token));
        }

        public void ClassifyPriority(int id, BugTypeDTO prioridad)
        {
            Bug bug = BugDataAccess.GetById(id);
            BugType priorityDomain = prioridad.ConvertToDomain();
            priorityDomain.Validate();
            bool valueCorrect = CheckValuePriority(priorityDomain.Value);
            if (valueCorrect)
                bug.Priority = priorityDomain;
            else
                throw new ValidationException();
            BugDataAccess.Update(id, bug);
        }

        public void ClassifySeverity(int id, BugTypeDTO severidad)
        {
            Bug bug = BugDataAccess.GetById(id);
            BugType severityDomain = severidad.ConvertToDomain();
            severityDomain.Validate();
            bool valueCorrect = CheckValueSeverity(severityDomain.Value);
            if (valueCorrect)
                bug.Severity = severityDomain;
            else
                throw new ValidationException();
            BugDataAccess.Update(id, bug);
        }


        bool CheckValuePriority(string value)
        {
            string valueToCompare = value.ToUpper();
            if (valueToCompare == "INMEDIATA" || valueToCompare == "ALTA" || valueToCompare == "MEDIA" || valueToCompare == "BAJA")
                return true;
            return false;
        }

        bool CheckValueSeverity(string value)
        {
            string valueToCompare = value.ToUpper();
            if (valueToCompare == "CRITICO" || valueToCompare == "MAYOR" || valueToCompare == "MENOR" || valueToCompare == "LEVE")
                return true;
            return false;
        }
    }


}