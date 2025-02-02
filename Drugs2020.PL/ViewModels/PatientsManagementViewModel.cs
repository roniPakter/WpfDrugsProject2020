﻿using Drugs2020.BLL;
using Drugs2020.BLL.BE;
using Drugs2020.PL.Commands;
using Drugs2020.PL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Drugs2020.PL.ViewModels
{
    class PatientsManagementViewModel : INotifyPropertyChanged, IEdit, IDelete, ISearch, IViewModel, IContainingVm ,IScreenReplacementVM
    {
        private PatientManagementModel patientManagementM;
        private AdminShellViewModel containingShellVm;

        public event PropertyChangedEventHandler PropertyChanged;
        public ScreenReplacementCommand ScreenReplacementCommand { get; set; }
        public EditingItemCommand EditCommand { get; set; }
        public DeleteItemCommand DeleteCommand { get; set; }
        public SearchItemCommand SearchCommand { get; set; }
        public ObservableCollection<Patient> Items { get; set; }
        private Patient selectedItem;
        public Patient SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedItem"));
            }
        }
        public PatientsManagementViewModel(AdminShellViewModel shellViewModel)
        {
            patientManagementM = new PatientManagementModel();
            Items = new ObservableCollection<Patient>(patientManagementM.Patients);
            Items.CollectionChanged += PatientsChanged;
            this.containingShellVm = shellViewModel;
            EditCommand = new EditingItemCommand(this);
            DeleteCommand = new DeleteItemCommand(this);
            SearchCommand = new SearchItemCommand(this);
            ScreenReplacementCommand = new ScreenReplacementCommand(this);
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        private void PatientsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            try
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    patientManagementM.SyncWithDb();
                }
            }
            catch(Exception ex) { containingShellVm.ShowMessage(ex.Message); }
        }

        public void OpenEditingScreen(object selectedPatient)
        {
            containingShellVm.CurrentVM = new UpdatePatientViewModel(containingShellVm, selectedPatient as Patient);
        }

        public void RemoveItemFromDb(object selectedPatient)
        {
            Patient patient = selectedPatient as Patient;
            try
            {
                patientManagementM.RemoveFromDb(patient);
            }
            catch (ArgumentException ex) { containingShellVm.ShowMessage(ex.Message); }
            catch (Exception ex) { containingShellVm.ShowMessage(ex.Message); }
            Items.Remove(patient);
        }

        public bool IsUserSureToDelete()
        {
            return new DeleteDecisionViewmodel("patient").Decision;
        }

        public void GetItem(string id)
        {
            SelectedItem = Items.SingleOrDefault(i => i.ID == id);
        }

        public void ReplaceScreen(Screen desiredScreen)
        {
            containingShellVm.ReplaceScreen(Screen.ADD_PATIENT_SCREEN);
        }

        public void DeleteSelected(object selectedPatient)
        {
            containingShellVm.LetUserDecide("Are you sure you want to delete this patient from the system?", new Action(() => RemoveItemFromDb(selectedPatient)));
        }
    }
}
