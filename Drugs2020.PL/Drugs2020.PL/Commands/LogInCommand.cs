﻿using Drugs2020.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Drugs2020.PL.Commands
{
    class LogInCommand : ICommand
    {
        private LogInViewModel logInViewModel;

        public LogInCommand(LogInViewModel logInViewModel)
        {
            this.logInViewModel = logInViewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            var result = false;

            var values = parameter as string[];

            if (parameter != null)
            {
                result = true;
            }

            return result;
        }

        public void Execute(object parameter)
        {
            
        }
    }
}
