using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Eysenck
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<string> questions { get; set; }

        int ExtrIntrCount;
        int NeuroCount;
        int LieCount;
        string typeResult;
        string lieResult;

        List<int> ExtrIntrTrueList;
        List<int> ExtrIntrFalseList;
        List<int> NeuroTrueList;
        List<int> LieTrueList;
        List<int> LieFalseList;
        public List<bool> answers { get; set; }

        public ICommand YesCom { get; set; }
        public ICommand NoCom { get; set; }
        public int _currentQuestionIndex;
        public int currentQuestionIndex
        {
            get
            {
                return _currentQuestionIndex;
            }
            set
            {
                _currentQuestionIndex = value;
                if (_currentQuestionIndex == 57)
                {
                    GetResult();
                    GetType();
                }
            }
        }

        public string currentQuestion { get; set; }
        public string CurrentQuestion
        {
            get
            {
                return currentQuestion;
            }
            set
            {
                currentQuestion = value;
                DoPropertyChanged("CurrentQuestion");
            }
        }

        public void GetResult()
        {
            for (int i = 0; i < ExtrIntrTrueList.Count(); i++)
            {
                if (answers[ExtrIntrTrueList[i] - 1])
                {
                    ExtrIntrCount++;
                }
            }
            for (int i = 0; i < ExtrIntrFalseList.Count(); i++)
            {
                if (!answers[ExtrIntrFalseList[i] - 1])
                {
                    ExtrIntrCount++;
                }
            }
            for (int i = 0; i < NeuroTrueList.Count(); i++)
            {
                if (answers[NeuroTrueList[i] - 1])
                {
                    NeuroCount++;
                }
            }
            for (int i = 0; i < LieTrueList.Count(); i++)
            {
                if (answers[LieTrueList[i] - 1])
                {
                    LieCount++;
                }
            }
            for (int i = 0; i < LieFalseList.Count(); i++)
            {
                if (!answers[LieFalseList[i] - 1])
                {
                    LieCount++;
                }
            }
        }
        public void GetType()
        {
            if (ExtrIntrCount > 12 && NeuroCount > 11)
            {
                typeResult = Eysenck.Properties.Settings.Default.Choleric;
            }
            if (ExtrIntrCount > 12 && NeuroCount <= 11)
            {
                typeResult = Eysenck.Properties.Settings.Default.Sanguine;
            }
            if (ExtrIntrCount <= 12 && NeuroCount > 11)
            {
                typeResult = Eysenck.Properties.Settings.Default.Melancholic;
            }
            if (ExtrIntrCount <= 12 && NeuroCount <= 11)
            {
                typeResult = Eysenck.Properties.Settings.Default.Phlegmatic;
            }
            if (LieCount >= 4)
            {
                lieResult = Eysenck.Properties.Settings.Default.Lie;
            }
            else
            {
                lieResult = Eysenck.Properties.Settings.Default.NormLie;
            }
            try
            {
                File.WriteAllText(Eysenck.Properties.Settings.Default.OutFile, "Ваш темперамент: " + typeResult + " В ответах: " + lieResult);
                MessageBox.Show("Ваш темперамент: " + typeResult + "\nВ ответах: " + lieResult + "\nДанные сохранены в файл");
            }
            catch
            {
                MessageBox.Show("Ваш темперамент: " + typeResult + "\nВ ответах: " + lieResult + "\nДанные не удалось сохранить в файл");
            }
            Environment.Exit(0);
        }


        public MainPageViewModel()
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            answers = new List<bool>();
            YesCom = new YesCommand();
            NoCom = new NoCommand();


            ExtrIntrTrueList = new List<int> { 1, 3, 8, 10, 13, 17, 22, 25, 27, 39, 44, 46, 49, 53, 56 };
            ExtrIntrFalseList = new List<int> { 5, 15, 20, 29, 32, 34, 37, 41, 51 };
            NeuroTrueList = new List<int> { 2, 4, 7, 9, 11, 14, 16, 19, 21, 23, 26, 28, 31, 33, 35, 38, 40, 43, 45, 47, 50, 52, 55, 57 };
            LieTrueList = new List<int> { 6, 24, 36 };
            LieFalseList = new List<int> { 12, 18, 30, 42, 48, 54 };


            GetQuestions gq = new GetQuestions();
            questions = gq.getQuestions();
            _currentQuestionIndex = 0;
            currentQuestion = questions[currentQuestionIndex];
            if (questions.Count() == 0)
            {
                MessageBox.Show("Вопросы не загрузились. Возможно, отсутствует доступ к файлу.");
            }
        }

        private void DoPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
