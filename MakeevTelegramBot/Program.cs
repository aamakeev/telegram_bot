using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MakeevTelegramBot
{
    class Program
    {
        static Dictionary<string, string> Questions;

        static void Main(string[] args)
        {
            var Data = System.IO.File.ReadAllText(@"/home/lehamakeev/bot/MakeevTelegramBot/answers.json");
            Questions = JsonConvert.DeserializeObject<Dictionary<string, string>>(Data);

            var API = new TelegramAPI();

            while (true)
            {
                var Updates = API.GetUpdates();
                foreach (var update in Updates)
                    try
                    {
                        var Question = (update.message != null ? update.message.text : update.edited_message.text);
                        var Answer = AnswerQuestion(Question);
                        API.sendMessage(Answer, (update.message != null ? update.message.chat.id : update.edited_message.chat.id));
                    }
                    catch (Exception e)
                    {
                        // Log error.
                    }
            }
            Console.ReadKey();
        }
        static string AnswerQuestion(string UserQuestion)
        {
            UserQuestion = UserQuestion.ToLower();
            List<string> Answers = new List<string>();

            /* Console.WriteLine($"Ваш вопрос: {UserQuestion}"); *///Так объявляется переменная UserQuestion в строке

            foreach (var Question in Questions)
            {
                if (UserQuestion.Contains(Question.Key))
                {
                    Answers.Add(Question.Value);
                }
            }


            if (UserQuestion.Contains("сколько времени"))
            {
                var Time = DateTime.Now.ToString("HH: mm:ss");
                Answers.Add($"Точное время: {Time}");
            }
            //"dddd, dd MMMM yyyy"

            if (UserQuestion.Contains("какой сегодня день"))
            {
                var Date = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                Answers.Add($"Сегодня {Date}");
            }

            if (UserQuestion.StartsWith("какая погода в городе"))
            {
                var Words = UserQuestion.Split(' ');
                var City = Words[Words.Length - 1];

                var WeatherApi = new Weather();
                var Forcast = WeatherApi.getWeatherInCity(City);
                Answers.Add(Forcast);
            }

            if (Answers.Count == 0)
            {
                Answers.Add("Я тебя не понимаю 🤷‍♂️");
            }

            return String.Join(" ", Answers);
        }
    }
}
