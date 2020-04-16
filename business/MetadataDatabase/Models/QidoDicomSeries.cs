﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MetadataDatabase.Controllers
{
    public class DicomStringObject
    {
        public IList<string> Value { get; set; }
        public string vr { get; set; }
    }
    public class DicomIntObject
    {
        public IList<int> Value { get; set; }
        public string vr { get; set; }
    }

    public class DicomNameObject
    {
        public IList<Name> Value { get; set; }
        public string vr { get; set; }
    }

    public class Name
    {
        public string type { get; set; }
    }

    public class QidoDicomSeries
    {
        // Character Set that expands or replaces the Basic Graphic Set. (00080005)
        [JsonPropertyName("00080005")]
        public DicomStringObject SpecificCharacterSet { get; set; }

        // Date the Study started. (00080020)
        [JsonPropertyName("00080020")]
        public DicomStringObject StudyDate { get; set; }

        // Time the Study started. (00080030)
        [JsonPropertyName("00080030")]
        public DicomStringObject StudyTime { get; set; }

        // A RIS generated number that identifies the order for the Study. (00080050)
        [JsonPropertyName("00080050")]
        public DicomStringObject AccessionNumber { get; set; }

        // Type of equipment that originally acquired the data. (00080060)
        [JsonPropertyName("00080060")]
        public DicomStringObject Modality { get; set; }

        // Name of the patient's referring physician (00080090)
        [JsonPropertyName("00080090")]
        public DicomStringObject ReferringPhysiciansName { get; set; }

        // User provided description of the Series (0008103E)
        [JsonPropertyName("0008103E")]
        public DicomStringObject SeriesDescription { get; set; }

        // URL specifying the location of the referenced instance(s). (00081190)
        [JsonPropertyName("00081190")]
        public DicomStringObject RetrieveURLAttribute { get; set; }

        // Patient's full name. (00100010)
        [JsonPropertyName("00100010")]
        public DicomNameObject PatientsName { get; set; }

        // Primary hospital identification number or code for the patient. (00100020)
        [JsonPropertyName("00100020")]
        public DicomStringObject PatientID { get; set; }

        // Birth date of the patient. (00100030)
        [JsonPropertyName("00100030")]
        public DicomStringObject PatientsBirthDate { get; set; }

        // Sex of the named patient. (00100040)
        [JsonPropertyName("00100040")]
        public DicomStringObject PatientsSex { get; set; }

        // Unique identifier for the Study (0020000D)
        [JsonPropertyName("0020000D")]
        public DicomStringObject StudyInstanceUID { get; set; }

        // Unique identifier of the Series. (0020000E)
        [JsonPropertyName("0020000E")]
        public DicomStringObject SeriesInstanceUID { get; set; }

        // User or equipment generated Study identifier (00200010)
        [JsonPropertyName("00200010")]
        public DicomStringObject StudyID { get; set; }

        // A number that identifies this Series. (00200011)
        [JsonPropertyName("00200011")]
        public DicomIntObject SeriesNumber { get; set; }

        // The number of Composite Object Instances in a Series that match the Series level Query/Retrieve search criteria (00201209)
        [JsonPropertyName("00201209")]
        public DicomIntObject NumberOfSeriesRelatedInstances { get; set; }

        // Todo
        public string GetValueOfDicomTag(DicomTag propertyName)
        {
            string value = "";
            var theType = this.GetType().GetProperty(propertyName.Value).PropertyType;
            var otherType = nameof(DicomStringObject);
            if(theType.Name == otherType)
            {
                DicomStringObject dicomStringValue = (DicomStringObject)this.GetType().GetProperty(propertyName.Value).GetValue(this, null);
                if (dicomStringValue.Value != null)
                {
                    value = dicomStringValue.Value[0];
                }
            } else if (theType.Name == nameof(DicomNameObject))
            {
                DicomNameObject dicomNameObject = (DicomNameObject)this.GetType().GetProperty(propertyName.Value).GetValue(this, null);
                if (dicomNameObject.Value != null)
                {
                    value = dicomNameObject.Value[0].type;
                }
            }
            
            

            return value;
        }

        public class DicomTag
        {
            private DicomTag(string value) { Value = value; }
            public string Value { get; set; }
            public static DicomTag SpecificCharacterSet { get { return new DicomTag("SpecificCharacterSet"); } }
            public static DicomTag StudyDate { get { return new DicomTag("StudyDate"); } }
            public static DicomTag StudyTime { get { return new DicomTag("StudyTime"); } }
            public static DicomTag AccessionNumber { get { return new DicomTag("AccessionNumber"); } }
            public static DicomTag Modality { get { return new DicomTag("Modality"); } }
            public static DicomTag ReferringPhysiciansName { get { return new DicomTag("ReferringPhysiciansName"); } }
            public static DicomTag SeriesDescription { get { return new DicomTag("SeriesDescription"); } }
            public static DicomTag RetrieveURLAttribute { get { return new DicomTag("RetrieveURLAttribute"); } }
            public static DicomTag PatientsName { get { return new DicomTag("PatientsName"); } }
            public static DicomTag PatientID { get { return new DicomTag("PatientID"); } }
            public static DicomTag PatientsBirthDate { get { return new DicomTag("PatientsBirthDate"); } }
            public static DicomTag PatientsSex { get { return new DicomTag("PatientsSex"); } }
            public static DicomTag StudyInstanceUID { get { return new DicomTag("StudyInstanceUID"); } }
            public static DicomTag SeriesInstanceUID { get { return new DicomTag("SeriesInstanceUID"); } }
            public static DicomTag StudyID { get { return new DicomTag("StudyID"); } }
            public static DicomTag SeriesNumber { get { return new DicomTag("SeriesNumber"); } }
            public static DicomTag NumberOfSeriesRelatedInstances { get { return new DicomTag("NumberOfSeriesRelatedInstances"); } }
        }
    }
}