using Core.Models;

namespace Core.Services
{
    public interface IChatService
    {
        public Task<Chat> GetChatById(int id);
        public Task<Chat> GetChatByTelegramChatId(long telegramChatId);
        public Task<Chat> CreateChat(Chat chat);
        //public Task<Chat> UpdateChat(Chat oldChat, Chat newChat);
        public Task<Chat> UpdateChatName(int id, string newChatName);
        public Task<Chat> UpdateChatName(long telegramChatId, string newChatName);
        public Task DeleteChatById(int id, long deletedById, string deletedByName);
        public Task DeleteChatByTelegramChatId(long telegramChatId, long deletedById, string deletedByName);
    }
}
