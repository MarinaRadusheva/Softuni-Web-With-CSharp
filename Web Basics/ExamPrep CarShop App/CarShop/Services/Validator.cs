using CarShop.Models.Cars;
using CarShop.Models.Issues;
using CarShop.Models.Users;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static CarShop.Data.DataConstatnts;

namespace CarShop.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateIssue(AddIssueViewModel model)
        {
            var errors = new List<string>();
            if (model.Description.Length < MinLengthIssue)
            {
                errors.Add($"$Description must be between longer than {MinLengthIssue} symbols");
            }
            return errors;
        }
        public ICollection<string> ValidateNewCar(AddCarFormModel model)
        {
            var errors = new List<string>();
            if (model.Model.Length < CarModelMinLength || model.Model.Length > StringMaxLength)
            {
                errors.Add($"$Model must be between {CarModelMinLength} and { StringMaxLength} symbols");
            }
            if (!Regex.IsMatch(model.PlateNumber, CarPlateTemplate))
            {
                errors.Add("Car plate format is not valid");
            }

            return errors;
        }

        public ICollection<string> ValidateUserRegistration(RegisterUserFormModel model)
        {
            var errors = new List<string>();
            if (model.Username.Length < MinLengthUsername || model.Username.Length >= StringMaxLength)
            {
                errors.Add($"Username must be between {MinLengthUsername} and {StringMaxLength} symbols");
            }

            if (!Regex.IsMatch(model.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            {
                errors.Add("Email must be in valid format!");
            }
            if (model.Password.Length < MinLengthPassword || model.Password.Length > StringMaxLength)
            {
                errors.Add($"Password must be between {MinLengthPassword} and {StringMaxLength} symbols");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add("Pasword confirmation is not correct");
            }
            if (model.UserType != UserTypeClient && model.UserType != UserTypeMechanic)
            {
                errors.Add($"User type must be {UserTypeClient} or {UserTypeMechanic}");
            }

            return errors;
        }
    }
}
