using Core;
using Core.Models;
using Core.Services.Database;

namespace Services.Database
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChatService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Chat> CreateChat(Chat chat)
        {
            await _unitOfWork.ChatRepository.AddAsync(chat);
            await _unitOfWork.CommitAsync();

            return chat;
        }

        public async Task DeleteChatById(int id, long deletedById, string deletedByName)
        {
            var chat = await _unitOfWork.ChatRepository.GetByIdAsync(id);
            if (chat is null) return;

            _unitOfWork.ChatRepository.DeleteAsync(chat, deletedById, deletedByName);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteChatByTelegramChatId(long telegramChatId, long deletedById, string deletedByName)
        {
            var chat = await _unitOfWork.ChatRepository.GetActiveChatByTelegramChatIdAsync(telegramChatId);
            if (chat is null) return;

            _unitOfWork.ChatRepository.DeleteAsync(chat, deletedById, deletedByName);

            await _unitOfWork.CommitAsync();
        }

        public async Task<Chat> GetChatById(int id)
        {
            return await _unitOfWork.ChatRepository.GetByIdAsync(id);
        }

        public async Task<Chat> GetChatByTelegramChatId(long telegramChatId)
        {
            return await _unitOfWork.ChatRepository.GetActiveChatByTelegramChatIdAsync(telegramChatId);
        }

        public async Task<Chat> UpdateChatName(int id, string newChatName)
        {
            var chat = await _unitOfWork.ChatRepository.GetByIdAsync(id);
            chat.Name = newChatName;

            await _unitOfWork.CommitAsync();

            return chat;
        }

        public async Task<Chat> UpdateChatName(long telegramChatId, string newChatName)
        {
            var chat = await _unitOfWork.ChatRepository.GetActiveChatByTelegramChatIdAsync(telegramChatId);
            chat.Name = newChatName;

            await _unitOfWork.CommitAsync();

            return chat;
        }
    }
}
