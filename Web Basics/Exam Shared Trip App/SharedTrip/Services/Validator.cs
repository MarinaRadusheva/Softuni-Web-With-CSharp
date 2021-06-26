using SharedTrip.Models.Trips;
using SharedTrip.Models.Users;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using static SharedTrip.Data.DataConstants;

namespace SharedTrip.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateUser(RegisterUserFormModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < UserMinLength || model.Username.Length > UserMaxLength)
            {
                errors.Add($"Username '{model.Username}' is not valid. It must be between {UserMinLength} and {UserMaxLength} characters long.");
            }

            if (!Regex.IsMatch(model.Email, EmailTemplate))
            {
                errors.Add($"Email {model.Email} is not a valid e-mail address.");
            }

            if (model.Password.Length < PassWordMinLength || model.Password.Length > PasswordMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {PassWordMinLength} and {PasswordMaxLength} characters long.");
            }

            if (model.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contain whitespaces.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Password and its confirmation are different.");
            }

            return errors;
        }

        public ICollection<string> ValidateTrip(AddTripFormModel model)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(model.StartPoint) || string.IsNullOrWhiteSpace(model.EndPoint))
            {
                errors.Add("Starting and ending points cannot be empty");
            }
            var date = DateTime.TryParseExact(model.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
            if (!date)
            {
                errors.Add("Date format is not correct");
            }
            if (model.Seats < MinSeats || model.Seats > MaxSeats)
            {
                errors.Add($"Invalid number of seats. Seats must be between {MinSeats} and {MaxSeats}");
            }
            if (string.IsNullOrWhiteSpace(model.Description) || model.Description.Length > TripDescriptionMaxLength)
            {
                errors.Add($"Description cannot be empty and must be less than {TripDescriptionMaxLength} symbols.");
            }

            if (model.ImagePath != "" && !Uri.IsWellFormedUriString(model.ImagePath, UriKind.Absolute))
            {
                errors.Add($"Image '{model.ImagePath}' is not valid. It must be a valid URL.");
            }

            return errors;
        }
    }
}
