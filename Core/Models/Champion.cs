using Core.Models.ModelBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Champion : Model
    {
        public int ChampionId { get; set; }
        public string Name { get; set; }
    }
}
