using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Tarus.CopySuperWinlossSetting.Model
{
    public class CopySuperWinlossSettingRequest
    {
        public int SourceCustId { get; set; }

        public string SourceUsername { get; set; }

        public string TargetCustIds { get; set; }

        public List<Product> Products { get; set; }

        public string IP { get; set; }

        public int UserId { get; set; }

        public bool Contains(int sporttype)
        {
            foreach (var product in Products)
            {
                if(product.Sporttypes.Contains(sporttype))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class Product
    {
        public int ProductId { get; set; }

        public List<int> Sporttypes { get; set; }
    }
}
