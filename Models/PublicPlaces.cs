using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Waka.Models
{
    public class PublicPlaces
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FullAddress { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Description { get; set; }
        public string PostedBy { get; set; }
    }
}
