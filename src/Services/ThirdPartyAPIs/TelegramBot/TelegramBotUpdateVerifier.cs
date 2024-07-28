using Core.Services.Database;
using Core.Services.ThirdPartyAPIs.TelegramBot;

namespace Services.ThirdPartyAPIs.TelegramBot
{
    internal class TelegramBotUpdateVerifier(IChatService chatService) : ITelegramBotUpdateVerifier
    {
        public async Task Verify(long telegramChatId)
        {
            var chat = await chatService.GetChatByTelegramChatId(telegramChatId) ?? throw new Exception("chat doesn't exist");
        }
    }
}
