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
            var Data = System.IO.File.ReadAllText(@"C:\Users\leha\Documents\ИАЦ\Bot\MakeevTelegramBot\json2.json");
            Questions = JsonConvert.DeserializeObject<Dictionary<string, string>>(Data);

            //Код выполняется при старте программы

            Console.WriteLine("Здравствуйте, вас приветствует бот-помощник, введите Ваш вопрос:");
            while (true)
            {
                //Код выполняется бесконечное число раз
                var Result = AnswerQuestion();
                if (!Result) //Если в переменной Result лежит false
                {
                    return;
                }
            }
 
        }
        static bool AnswerQuestion ()
        {
            var UserQuestion = Console.ReadLine().ToLower();
            List<string> Answers = new List<string>();

            

            

            //Console.WriteLine($"Ваш вопрос: {UserQuestion}"); - Так объявляется переменная UserQuestion в строке

            foreach (var Question in Questions)
            {
                if (UserQuestion.Contains (Question.Key))
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

            if (Answers.Count == 0)
            {
                Answers.Add("Я тебя не понимаю 🤷‍♂️");
            }

            Console.WriteLine(String.Join(", ", Answers));


            if (UserQuestion.Contains("надоело"))
            {
                Console.WriteLine("Спасибо, с Вами было приятно общаться!");
                return false;
            }
            return true;
        }
    }
}
