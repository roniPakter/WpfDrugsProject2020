﻿using Drugs2020.BLL.BE;
using System.Data.Entity;

namespace Drugs2020.DAL
{
    public class PharmacyContext : DbContext
    {
        public PharmacyContext() : base("test_19")
        {}

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Physician> Physicians { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<MedicalFile> MedicalFiles { get; set; }
        public DbSet<Recept> Recepts { get; set; }
    }
}

