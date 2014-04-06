using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Quizzer;

namespace GUIView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IEnumerator<Question> CurrentQuestions;
        private List<Question> incorrectList;
        private bool randomize = true;
        private bool reprompt = true;
        public MainWindow()
        {
            InitializeComponent();
            List<Question> masterList = QuestionParser.ParseFile(@"..\..\..\nato.txt");
            SetCurrentQuestions(masterList);
        }

        private void btnCorrect_Click(object sender, RoutedEventArgs e)
        {
            MoveNext();
        }

        private void btnWrong_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentQuestions.Current != null)
            {
                incorrectList.Add(CurrentQuestions.Current);
            }
            MoveNext();
        }

        private void MoveNext()
        {
            if (!CurrentQuestions.MoveNext())
            {
                if (!reprompt || incorrectList.Count == 0) //if the user doesn't want to be asked questions again, or then got all of them right
                {
                    tbQuestion.Text = "Done!";
                    tbAnswer.Text = "";
                }
                else
                {
                    SetCurrentQuestions(incorrectList);
                }
            }
            else
            {
                DisplayCurrentQuestion();
            }
        }

        private void SetCurrentQuestions(IEnumerable<Question> questions)
        {
            if (randomize)
            {
                questions = questions.Randomize();
            }
            CurrentQuestions = questions.GetEnumerator();
            incorrectList = new List<Question>();
            CurrentQuestions.MoveNext();
            DisplayCurrentQuestion();
        }

        private void DisplayCurrentQuestion()
        {
            tbQuestion.Text = CurrentQuestions.Current.Prompt;
            tbAnswer.Text = CurrentQuestions.Current.Answer;
        }
    }

    static class Extension
    {
        private static readonly Random rand = new Random();
        public static IEnumerable<T>  Randomize<T>(this IEnumerable<T> collection)
        {
            T[] result = collection.ToArray();
            for(int i=0; i<result.Length; i++)
            {
                // don't swap the first index twice, else it could end up at the beginning again
                int swapIdx = rand.Next(result.Length);
                T tmp = result[i];
                result[i] = result[swapIdx];
                result[swapIdx] = tmp;
            }
            return result;
        }
    }
}
