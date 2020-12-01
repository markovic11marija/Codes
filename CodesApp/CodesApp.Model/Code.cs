using System;
using System.Collections.Generic;
using System.Text;

namespace CodesApp.Model
{
    public class Code : Entity
    {
        public string SoftwareName { get; set; }
        public string CodeValue { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsActive { get; set; }
    }
}
