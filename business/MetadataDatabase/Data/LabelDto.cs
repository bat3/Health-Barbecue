﻿using MetadataDatabase.Models;

namespace MetadataDatabase.Data
{
    public class LabelDto
    {
        public string Id { get; set; }
        public string User { get; set; }
        public string LabelKey { get; set; }
        public string LabelType { get; set; }
        public string[] LabelValue { get; set; }
        public bool IsPublic { get; set; }
        public bool IsApproved { get; set; }
        public string AssignedValue { get; set; }
    }
}
