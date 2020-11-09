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
    class AddMedicalRecordViewModel:INotifyPropertyChanged,  IAddToDb,  IViewModel, IContainingVm
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private MedicalRecordModel medicalRecordModel;
        private PhysicianShellViewModel containingVm;
        private string patientId;
        private Physician physicianUser;
        public AddMedicalRecordViewModel(PhysicianShellViewModel containingVm, string patientId, Physician physicianUser, MedicalRecord medicalRecordExists = null)
        {
            if(medicalRecordExists ==null)
                this.medicalRecordModel = new MedicalRecordModel(patientId, physicianUser);
            else
                this.medicalRecordModel = new MedicalRecordModel(patientId, physicianUser, medicalRecordExists);
            this.containingVm = containingVm;
            AddToDbCommand = new AddToDbCommand(this);
            this.patientId = patientId;
            this.physicianUser = physicianUser;
        }

        public AddToDbCommand AddToDbCommand { get; set; }
        public MedicalRecord MedicalRecord{ 
            set 
            { 
                medicalRecordModel.MedicalRecord=value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("MedicalRecord"));
                }
            } 
            get
            {
                return medicalRecordModel.MedicalRecord;
            } 
        }


        public void AddItemToDb()
        {
            medicalRecordModel.AddMedicalRecordToDb();
            containingVm.ReplaceScreen(Screen.PATIENT_DATA);
        }

        public bool ItemAlreadyExists()
        {
          return false;
        }

        void IAddToDb.UserWantsToReplaceExistingItem() { }
    }
}
