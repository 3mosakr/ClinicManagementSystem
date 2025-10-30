using System;
using System.ComponentModel.DataAnnotations;
using ClinicManagementSystem.ViewModel.DoctorAvailability;

namespace ClinicManagementSystem.Validations
{
    public class EndTimeAfterStartTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (DoctorAvailabilityViewModel)validationContext.ObjectInstance;

            if (model.EndTime <= model.StartTime)
            {
                return new ValidationResult("End time must be after start time.");
            }

            return ValidationResult.Success;
        }
    }
}
