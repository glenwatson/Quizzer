using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quizzer
{
    public class Question
    {
        public String Prompt { get; set; }
        public String Answer { get; set; }

        public override string ToString()
        {
            return Prompt + "? " + Answer;
        }
    }
}
