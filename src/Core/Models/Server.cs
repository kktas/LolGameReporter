using Core.Models.ModelBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Server : Model
    {
        public string Name { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
