using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpedizioniSPA.Validations
{
    public class PecValidation : ValidationAttribute
    {

        protected override ValidationResult IsValid(object Pec, ValidationContext validationContext)
        {
            System.Diagnostics.Debug.WriteLine("Email: " + Pec);

            string pecToCheck = Pec.ToString();

            if (pecToCheck.Contains("pec"))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("L'email inserita non è valida");
            }


        }


    }
}