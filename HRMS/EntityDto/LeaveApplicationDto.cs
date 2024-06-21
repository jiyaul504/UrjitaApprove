using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.EntityDto
{
    public class LeaveApplicationDto
    {
        public int LeaveApplicationId { get; set; }

        public int EmployeeId { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; } // Assuming you want to display employee name

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End Date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        public string Reason { get; set; }

        
        public string Status { get; set; } // Pending, Approved, Rejected

        [Required(ErrorMessage = "Type of Leave is required")]
        public string TypeOfLeave { get; set; }

        public int ApproverId { get; set; } // New field for Approver's ID
        public EmployeeDto Approver { get; set; } // Navigation property for Approver

        [Display(Name = "Is Approved")]
        public bool IsApproved { get; set; }

        [Display(Name = "Department ID")]
        public int DepartmentId { get; set; } // Department ID for department-specific approval process

        [Display(Name = "Half Day")]
        public bool HalfDay { get; set; } // New field for Half Day

        [Display(Name = "Full Day")]
        public bool FullDay { get; set; } // New field for Full Day
        public bool IsHalfDayLeave() => HalfDay && !FullDay;
        public bool IsFullDayLeave() => FullDay && !HalfDay;
        public bool IsMultiDayLeave() => !IsHalfDayLeave() && !IsFullDayLeave() && StartDate != EndDate;

        public class DateGreaterThanAttribute : ValidationAttribute
        {
            private readonly string _comparisonProperty;

            public DateGreaterThanAttribute(string comparisonProperty)
            {
                _comparisonProperty = comparisonProperty;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var propertyInfo = validationContext.ObjectType.GetProperty(_comparisonProperty);

                if (propertyInfo == null)
                    return new ValidationResult($"Unknown property {_comparisonProperty}");

                var comparisonValue = (DateTime)propertyInfo.GetValue(validationContext.ObjectInstance);

                if ((DateTime)value < comparisonValue)
                    return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be greater than {_comparisonProperty}");

                return ValidationResult.Success;
            }
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsHalfDayLeave() && EndDate.Date != StartDate.Date)
                yield return new ValidationResult("For Half Day leave, End Date must be the same as Start Date.");

            if (IsFullDayLeave() && EndDate.Date != StartDate.Date)
                yield return new ValidationResult("For Full Day leave, End Date must be the same as Start Date.");

            if (IsMultiDayLeave() && EndDate <= StartDate)
                yield return new ValidationResult("End Date must be greater than Start Date for multi-day leave.");
        }
    }
}
