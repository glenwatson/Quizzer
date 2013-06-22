using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Quizzer
{
    class Program
    {
        private static int timer = 800;
        static void Main(string[] args)
        {
            List<Question> masterList = QuestionParser.ParseFile(@"C:\Users\glen.watson\Documents\Visual Studio 2010\Projects\Quizzer\Quizzer\compassDegrees.txt");
            bool run = true;
            Console.WriteLine("Press J if you get the question correct.\r\nReady?");
            Console.ReadLine();
            while (run)
            {
                List<Question> incorrectList = new List<Question>(masterList);
                do
                {
                    incorrectList = GetIncorrectList(incorrectList, false, false);
                }
                while (incorrectList.Count > 0);

                Console.WriteLine("Done! Again? (n)");
                if (Console.ReadLine() == "n")
                {
                    run = false;
                }
            }
        }

        private static List<Question> GetIncorrectList(List<Question> questionList, bool randomize, bool reprompt)
        {
            if(randomize)
            {
               Randomize(questionList);
            }
            List<Question> incorrectList = new List<Question>();
            foreach (Question q in questionList)
            {
                Console.WriteLine(q.Prompt);
                if (timer == -1)
                {
                    Console.WriteLine();
                }
                else
                {
                    Thread.Sleep(timer);
                }
                Console.WriteLine(q.Answer);
                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.WriteLine();
                if (reprompt)
                {
                    if (key.Key == ConsoleKey.J) //right
                    {

                    }
                    else //wrong
                    {
                        incorrectList.Add(q);
                    }
                }
            }
            return incorrectList;
        }

        private static void Randomize(List<Question> list)
        {
            Random rand = new Random();
            for (int idx = 0; idx < list.Count; idx++)
            {
                int swapIdx = rand.Next(list.Count);
                Question temp = list[idx];
                list[idx] = list[swapIdx];
                list[swapIdx] = temp;
            }
        }
    }

}
