using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Quizzer
{
    public class QuestionParser
    {
        /// <summary>
        /// Format is:
        /// Question
        /// Answer
        /// 
        /// NextQuestion
        /// NextAnswer
        /// ...
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<Question> ParseFile(string path)
        {
            List<Question> questions = new List<Question>();
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while(!sr.EndOfStream)
                    {
                        string question = sr.ReadLine();
                        if (sr.EndOfStream)
                            throw new IOException("No answer given after question.");
                        string answer = sr.ReadLine();
                        if (!sr.EndOfStream)
                            sr.ReadLine();
                        questions.Add(new Question { Prompt = question, Answer = answer });

                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Problem reading the file:");
                Console.WriteLine(e.Message);
            }
            return questions;
        }
    }
}
