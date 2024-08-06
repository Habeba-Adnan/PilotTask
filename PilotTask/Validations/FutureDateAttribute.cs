﻿using System.ComponentModel.DataAnnotations;

namespace PilotTask.Validations
{
    public class FutureDateAttribute: ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime date) 
            {
                return date > DateTime.Now;
            }
            return false;
        }
    }
}
