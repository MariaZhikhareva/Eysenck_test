using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Eysenck
{
    class NoCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var par = parameter as MainPageViewModel;
            if (par != null)
            {
                if (par.currentQuestionIndex < par.questions.Count())
                {
                    par.answers.Add(false);
                    par.currentQuestionIndex++;
                    if (par.currentQuestionIndex < par.questions.Count())
                    {
                        par.CurrentQuestion = par.questions[par.currentQuestionIndex];
                    }
                }
            }
        }
    }
}
