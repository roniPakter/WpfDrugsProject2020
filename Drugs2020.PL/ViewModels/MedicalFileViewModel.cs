﻿using Drugs2020.BLL.BE;
using Drugs2020.PL.Commands;
using Drugs2020.PL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drugs2020.PL.ViewModels
{
    class MedicalFileViewModel: IViewModel, IAddToDb  , INotifyPropertyChanged, IContainingVm, IScreenReplacementVM
    {
        private PhysicianShellViewModel containingShellVm;
        private MedicalFileModel medicalFileM;
        public event PropertyChangedEventHandler PropertyChanged;
        private Physician physicianUser;
        public AddToDbCommand AddToDbCommand { get; set; }
        public ScreenReplacementCommand ScreenReplacementCommand { get; set; }
        public MedicalFile MedicalFile
        {
            get { return medicalFileM.MedicalFile; }
            set {
                medicalFileM.MedicalFile = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("MedicalFile"));
                }
            }
        }
        public MedicalFileViewModel(PhysicianShellViewModel containingShellVm, string patientId, Physician physicianUser)
        {
            this.containingShellVm = containingShellVm;
            medicalFileM = new MedicalFileModel(patientId);
            AddToDbCommand = new AddToDbCommand(this);
            ScreenReplacementCommand = new ScreenReplacementCommand(this);
            this.physicianUser = physicianUser;
        }

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public void AddItemToDb()
        {
            try
            {
                medicalFileM.AddMedicalFileToDb();
            }
            catch (ArgumentException e) { containingShellVm.ShowMessage(e.Message); }
            catch (Exception e) { containingShellVm.ShowMessage(e.Message); }
        }

        public bool ItemAlreadyExists()
        {
            return medicalFileM.MedicalFileAlreadyExists();
        }

        public void UpdateExistingItem()
        {
            try
            {
                medicalFileM.UpdateMedicalFile();
            }
            catch (ArgumentException e) { containingShellVm.ShowMessage(e.Message); }
            catch (Exception e) { containingShellVm.ShowMessage(e.Message); }
        }
       
        public void ReplaceScreen(Screen desiredScreen)
        {
            containingShellVm.ReplaceScreen(desiredScreen);
        }

        public void UserWantsToReplaceExistingItem()
        {
            containingShellVm.LetUserDecide("A medical file already exists in the system. \nDo you want to override it?", new Action(UpdateExistingItem));
        }
    }
}
