﻿using Drugs2020.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Drugs2020.PL.Commands
{
    public class AddToDbCommand : ICommand
    {
        private IAddToDb addToDbViewModel;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public AddToDbCommand(IAddToDb addToDbViewModel)
        {
            this.addToDbViewModel = addToDbViewModel;
        }

       

        public bool CanExecute(object parameter)
        {
            bool result = false;
            if (parameter != null)
            {
                result = (bool)parameter;
            }
            return result;
        }

        public void Execute(object parameter)
        {
            if (!addToDbViewModel.ItemAlreadyExists())
            {
                addToDbViewModel.AddItemToDb();
            }
            else 
            {
                addToDbViewModel.UserWantsToReplaceExistingItem();
            }
        }
    }
}
