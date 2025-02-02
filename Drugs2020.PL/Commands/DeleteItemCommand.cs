﻿using Drugs2020.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Drugs2020.PL.Commands
{
    public class DeleteItemCommand : ICommand
    {
        private IDelete vm;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DeleteItemCommand(IDelete vm)
        {
            this.vm = vm;
        }
        public bool CanExecute(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }
            return true;
        }

        public void Execute(object parameter)
        {
            vm.DeleteSelected(parameter);
        }
    }
}
