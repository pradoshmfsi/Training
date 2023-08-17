using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyDoctor.Utils.Models
{
    public class StandardPostResponseModel
    {
        public bool IsSuccess { get; set; }
        public dynamic Data { get; set; }
    }
}
