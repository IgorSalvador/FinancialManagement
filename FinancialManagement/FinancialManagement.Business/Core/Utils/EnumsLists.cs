using FinancialManagement.Business.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagement.Business.Core.Utils
{
    public static class EnumsLists
    {
        public static string GetProfile(Profile profile)
        {
            var description = profile switch
            {
                Profile.Administrator => "Administrator",
                Profile.General => "General",
                _ => "General"
            };

            return description;
        }
    }
}
