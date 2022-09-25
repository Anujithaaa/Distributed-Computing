using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistryProject.Models
{
    public class serviceInputModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ApiEndpoint { get; set; }
        public string NumberOfOperands { get; set; }
        public string OperandType { get; set; }
    }
}