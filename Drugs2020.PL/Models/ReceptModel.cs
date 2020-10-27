﻿using Drugs2020.BLL;
using Drugs2020.BLL.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drugs2020.PL.Models
{
    class ReceptModel
    {
        private IBL bl;
        public Recept Recept { get; set; }
        public List<Drug> DrugList { get; set; }
        public List<Recept> AllRecepts { get; set; }
        public string Conflicts { get; set; }
        private string patientId;

        public ReceptModel(string patientId, string physicianId)
        {
            bl = new BLImplementation();
            this.patientId = patientId;
            Recept = new Recept(patientId, physicianId);
            DrugList = bl.GetAllDrugs();
            AllRecepts = bl.GetAllReceptsOfPatient(patientId);

        }

        public void AddRecept()
        {
            Conflicts = bl.checkConflicts(Recept.IdCodeOfDrug, AllRecepts.Select(x => x.IdCodeOfDrug).ToList());
            //לתת לו להחליט אם רוצה להוסיף
            bl.AddRecept(Recept);
            AllRecepts = bl.GetAllReceptsOfPatient(patientId);
        }

        public bool ReceptAlreadyExists()
        {
            return false;//צריך לממש את זה!!!
        }

        public void CreatePDF()
        {
            bl.CreatePDF(Recept);
        }
    }
}
