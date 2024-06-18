using Core.Models.ModelBase;
using System.Net.Security;

namespace Core.Models
{
    public class Account : Model
    {
        public string GameName { get; set; } = string.Empty;
        public string TagLine { get; set; } = string.Empty;
        public string Puuid { get; set; } = string.Empty;
        public int ServerId { get; set; }
        public Server Server { get; set; }
        public List<Chat> Chats { get; set; }

    }
}
