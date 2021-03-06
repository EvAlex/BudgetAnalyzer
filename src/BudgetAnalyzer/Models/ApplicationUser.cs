﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BudgetAnalyzer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
            : base()
        {
            Accounts = new List<BankAccount>();
        }

        public ApplicationUser(string userName)
            : base(userName)
        {
            Accounts = new List<BankAccount>();
        }

        public virtual ICollection<BankAccount> Accounts { get; set; }
    }
}
