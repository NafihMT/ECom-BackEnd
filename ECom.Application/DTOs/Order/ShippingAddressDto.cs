using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Application.DTOs.Order
{
    public class ShippingAddressDto
    {
        public string f_name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string zipCode { get; set; }
    }
}
