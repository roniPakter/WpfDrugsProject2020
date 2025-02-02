﻿using Drugs2020.BLL.BE;
using Drugs2020.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Drugs2020.PL.Commands
{
    public class SearchItemCommand : ICommand
    {
        private ISearch searchVm;

        public SearchItemCommand(ISearch searchVm)
        {
            this.searchVm = searchVm;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            bool result=false;

            if (parameter as string != "")
                result = true;

            return result;
        }

        public void Execute(object parameter)
        {
            searchVm.GetItem(parameter as string);           
        }
    }
}
