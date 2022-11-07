using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace BugParser
{
    public class BugParserXML : IBugParser
    {
        public List<Bug> GetBugs(string fullPath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BugModel));
            FileStream fs = new FileStream(fullPath, FileMode.Open);
            BugModel completeImportedData;
            try
            {
                completeImportedData = (BugModel)serializer.Deserialize(fs);
            }
            catch (InvalidOperationException e)
            {
                fs.Close();
                throw new XmlException(e.Message);
            }
            List<Bug> importedBugs;
            try
            {
                importedBugs = completeImportedData.ConvertToBugs();
            }
            catch (NullReferenceException e)
            {
                fs.Close();
                throw new XmlException(e.Message);
            }
            fs.Close();
            return importedBugs;

        }

    }
}
