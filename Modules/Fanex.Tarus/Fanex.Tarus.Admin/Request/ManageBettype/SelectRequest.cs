using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Tarus.Admin.Request.ManageBettype
{
    public class SelectRequest
    {
        public int TypeId { get; set; }

        public string TypeName { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
