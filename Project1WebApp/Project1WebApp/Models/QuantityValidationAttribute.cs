using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project1WebApp.Models
{
    public class QuantityValidationAttribute : ValidationAttribute
    {
        
        public override bool IsValid(object value)
        {
            var _quantity = value as List<int>;
            foreach (int item in _quantity)
            {
                if (item < 0) return false;

            }
            return true;
        }
    }
}