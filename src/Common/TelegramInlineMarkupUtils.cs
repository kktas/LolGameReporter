using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common
{
    public record InlineKeyboardButtonDTO(string Text, string Data);
    public static class TelegramInlineMarkupUtils
    {
        public static InlineKeyboardMarkup CreateInlineKeyboardMarkup(List<InlineKeyboardButtonDTO> buttons, int buttonsPerRow)
        {
            var inlineKeyboardLayout = new List<List<InlineKeyboardButton>>();

            for (int i = 0; i < Math.Ceiling((double)buttons.Count / (double)buttonsPerRow); i++)
            {
                var row = new List<InlineKeyboardButton>();
                for (int j = 0; j < buttonsPerRow && i * buttonsPerRow + j < buttons.Count; j++)
                {
                    var button = buttons[i * buttonsPerRow + j];
                    row.Add(InlineKeyboardButton.WithCallbackData(text: button.Text, callbackData: button.Data));
                }
                inlineKeyboardLayout.Add(row);
            }

            return new InlineKeyboardMarkup(inlineKeyboardLayout);
        }
    }
}
