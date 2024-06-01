using Core.Models.ModelBase;

namespace Core.Models
{
    public class Account : Model
    {
        public string GameName { get; set; } = string.Empty;
        public string TagLine { get; set; } = string.Empty;
        public string Puuid { get; set; } = string.Empty;
        public int ChatId { get; set; }
        public required Chat Chat { get; set; }

    }
}
