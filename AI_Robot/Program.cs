using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.IO;
namespace AI_Robot
{
    class Program
    {
        // 1. Упростить(улучшить) выход из программы с помощью - do While.
        // 2. Если Робот не знает слово - То сообщает об этом.
        // 3. И обучается новому слову - (записывает новый вопрос в - questions и новый ответ в - answers)
        static void Main(string[] args)
        {
            // string[] answers = { "Здравствуйте", "Хорошо", "Пока" };
            // string[] questions = { "Здравствуйте", "Как дела", "Пока" };
            List<string> answers = File.ReadAllLines("answers.txt").ToList();
            List<string> questions = File.ReadAllLines("questions.txt").ToList();
            //File.WriteAllText("questions.txt", questions);
            string iDoNotKnow = "Я еще маленькая, я не понимаю - объясни мне";
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            string question = "";
            do
            {
                question = Console.ReadLine(); // Задаем вопрос роботу
                for (int i = 0; i < questions.Count; i++)
                {
                    if (questions[i] == question) // Если знает(понимает)
                    {
                        Console.WriteLine(answers[i]); // То ответит - answer[i]
                        synth.Speak(answers[i]);
                        break;
                    }
                    else if (i == questions.Count - 1) // если все слова перебрали и не нашли вопрос, то ...
                    {
                        Console.WriteLine(iDoNotKnow);
                        synth.Speak(iDoNotKnow);
                        questions.Add(question); // добавляем вопрос в список вопросов 
                        answers.Add(Console.ReadLine()); // обучаем - добавляя ответ в список ответов
                        File.WriteAllLines("questions.txt", questions);
                        File.WriteAllLines("answers.txt", answers);
                    }
                }
            } while ("Пока" != question);
            Console.ReadKey();
        }
    }
}