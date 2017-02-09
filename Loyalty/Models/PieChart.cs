using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Loyalty.Models
{
    [DataContract]
    public class PieChartModel
    {
        [DataMember(Name = "label")]
        public string Label { get; set; }
        [DataMember(Name = "value")]
        public Int64 Value { get; set; }
        [DataMember(Name = "color")]
        public string Color { get; set; }
        [DataMember(Name = "highlight")]
        public string Highlight { get; set; }
    }
}