using Core.Models.ModelBase;

namespace Core.Models
{
    public class Chat : Model
    {
        public string Name { get; set; } = string.Empty;
        public long TelegramChatId { get; set; }
        public bool IsVerified { get; set; } = false;
        public List<Account> Accounts { get; set; }
    }
}
