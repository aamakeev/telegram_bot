using System;
using RestSharp;
using Newtonsoft.Json;
namespace MakeevTelegramBot
{
    public class TelegramAPI
    {
        public class Chat
        {
            public int id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string username { get; set; }
        }
        public class Message
        {
            public Chat chat { get; set; }
            public string text { get; set; }
        }

        public class Update
        {
            public int update_id { get; set; }
            public Message message { get; set; }
            public Message edited_message { get; set; }
        }

        public class ApiResult
        {
            public Update[] result { get; set; }
        }



        const String API_Key = "986887800:AAE6kImMWHl-zUccdzyUpF2bg-YZchnrtOM";
        const String API_URL = "https://api.telegram.org/bot" + API_Key + "/";

        RestClient RC = new RestClient();

        private int last_update_id = 0;

        public TelegramAPI()
        {

            // код инициализации нашего объекта
        }

        public void sendMessage(string text, int chat_id)
        {
            sendApiRequest("sendMessage", $"chat_id={chat_id}&text={text}");
        }

        public Update[] GetUpdates()
        {
            var json = sendApiRequest("getUpdates", "offset=" + last_update_id);
            var apiResult = JsonConvert.DeserializeObject<ApiResult>(json);
            foreach (var update in apiResult.result)
            {
                Console.WriteLine($"Получен апдейт {update.update_id}, "
                                  + $"сообщение от {(update.message != null ? update.message.chat.first_name : update.edited_message.chat.first_name)}" + $" {(update.message != null ? update.message.chat.last_name : update.edited_message.chat.last_name)} "
                                  + $"текст: {(update.message != null ? update.message.text : update.edited_message.text)}, " + $"номер чата: {(update.message != null ? update.message.chat.id : update.edited_message.chat.id)}");
                last_update_id = update.update_id + 1;
            }
            return apiResult.result;
        }


        public string sendApiRequest(string ApiMethod, string Params)
        {

            //склеиваем URL
            var URL = API_URL + ApiMethod + "?" + Params;

            //создаём объект запроса из ссылки
            var Request = new RestRequest(URL);

            //выполняем запрос
            var Response = RC.Get(Request);

            //возвращаем результат
            return Response.Content;
        }
    }
}