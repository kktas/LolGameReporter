using Core.Models.ModelBase;

namespace Core.Models
{
    public class Region : Model
    {
        public string Name { get; set; }
        public List<Server> Servers { get; set; }
    }
}
