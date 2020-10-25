﻿using Drugs2020.BLL.BE;
using Drugs2020.PL.Commands;
using Drugs2020.PL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drugs2020.PL.ViewModels
{
    class UpdateDrugViewModel : INotifyPropertyChanged, IUpdateInDb, IGoBackScreenVM, IViewModel, IAddIngrediantToDrug, IDelete
    {
        private UpdateDrugModel updateDrugM;
        public event PropertyChangedEventHandler PropertyChanged;

        private DrugsManagementViewModel containingVm;
        public UpdateInDbCommand UpdateDbCommand { get; set; }
        public bool IsNewDrug { get; }
        public BackCommand BackCommand { get; set; }
        public AddIngredientToDrugCommand AddIngredientCommand { get; set; }
        public DeleteItemCommand DeleteIngredientCommand { get; set; }
        public Drug Drug
        {
            get { return updateDrugM.Drug; }
            set { updateDrugM.Drug = value; }
        }
        private ActiveIngredient ingredientToAdd;
        public ActiveIngredient IngredientToAdd
        {
            get { return ingredientToAdd; }
            set
            {
                ingredientToAdd = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("IngredientToAdd"));
            }
        }
        public ObservableCollection<ActiveIngredient> Ingredients { get; set; }

        public UpdateDrugViewModel(DrugsManagementViewModel containingVm, Drug drugToUpdate)
        {
            updateDrugM = new UpdateDrugModel();
            this.containingVm = containingVm;
            Drug = drugToUpdate;
            UpdateDbCommand = new UpdateInDbCommand(this);
            IsNewDrug = false;
            BackCommand = new BackCommand(this);
            IngredientToAdd = new ActiveIngredient();
           // Ingredients = new ObservableCollection<ActiveIngredient>(drugToUpdate.Composition);
            AddIngredientCommand = new AddIngredientToDrugCommand(this);
            DeleteIngredientCommand = new DeleteItemCommand(this);
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public void UpdateInDb()
        {
            updateDrugM.UpdatePatientInDb();
            containingVm.Items.Remove(containingVm.Items.Single(i => i.IdCode == Drug.IdCode));
            containingVm.Items.Add(Drug);
            GoBack();
        }

        public void GoBack()
        {
            containingVm.ReturnToContaining();
        }

        public void AddIngredientToDrug()
        {
            Ingredients.Add(IngredientToAdd);
        //    Drug.Composition.Add(IngredientToAdd);
            IngredientToAdd = new ActiveIngredient();
        }

        public void RemoveItemFromDb(object ingredient)
        {
            ActiveIngredient activeIngredient = ingredient as ActiveIngredient;
            Ingredients.Remove(activeIngredient);
          //  Drug.Composition.Remove(activeIngredient);
        }

        public bool IsUserSureToDelete()
        {
            return new DeleteDecisionViewmodel("active ingredient").Decision;
        }
    }
}
