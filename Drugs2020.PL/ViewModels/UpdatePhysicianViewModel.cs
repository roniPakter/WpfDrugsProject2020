﻿using Drugs2020.BLL.BE;
using Drugs2020.PL.Commands;
using Drugs2020.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drugs2020.PL.ViewModels
{
    class UpdatePhysicianViewModel : IUpdateInDb, IGoBackScreenVM, IViewModel
    {
        private UpdatePhysicianModel updatePhysicianM;

        private AdminShellViewModel containingVm;
        public UpdateInDbCommand UpdateDbCommand { get; set; }
        public bool IsNewPhysician { get; }
        public BackCommand BackCommand { get; set; }
        public Physician Physician
        {
            get { return updatePhysicianM.Physician; }
            set { updatePhysicianM.Physician = value; }
        }
        public Array SexEnumValues => Enum.GetValues(typeof(Sex));

        public UpdatePhysicianViewModel(AdminShellViewModel containingVm, Physician physicianToUpdate)
        {
            updatePhysicianM = new UpdatePhysicianModel();
            this.containingVm = containingVm;
            Physician = physicianToUpdate;
            UpdateDbCommand = new UpdateInDbCommand(this);
            IsNewPhysician = false;
            BackCommand = new BackCommand(this);
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public async void UpdateInDb()
        {
            containingVm.startProcessing("Updating on database");
            await Task.Run(() =>
            {
                try
                {
                    updatePhysicianM.UpdatePhysicianInDb();
                    containingVm.finishProcessing("Success!");
                }
                catch (ArgumentException ex) { containingVm.ShowMessage(ex.Message); }
                catch (Exception ex) { containingVm.ShowMessage(ex.Message); }
                GoBack();
            });
        }

        public void GoBack()
        {
            containingVm.ReplaceScreen(Screen.PHYSICIANS_MANAGEMENT);
        }
    }
}
