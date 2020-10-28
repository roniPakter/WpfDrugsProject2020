﻿using Drugs2020.BLL.BE;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drugs2020.PL.ViewModels
{
    //אפשר למחוק הכל!!!!!!!!1

    class PhysicianPanelViewModel : INotifyPropertyChanged, IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Physician PhysicianUser { get; }
        public PhysicianPanelViewModel(IUser physicianUser)
        {
            PhysicianUser = physicianUser as Physician;
        }

        public void GoBack()
        {
            throw new NotImplementedException();
        }
    }
}
