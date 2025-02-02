﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Drugs2020.BLL.BE
{
    public class Admin : Person, IUser
    {
        public string Password { get; set; }
        public Admin(string id, string fName, string lName,  Sex sex, string phone, string email, string password, string address, DateTime birthDate) : base(id, fName, lName, sex, phone, email, address, birthDate)
        {
            Password = password;
        }

        public Admin() { }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public string GetPassword()
        {
            return Password;
        }
        public string GetName()
        {
            return FirstName + " " + LastName;
        }
    }
}