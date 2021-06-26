using System;
using System.Collections.Generic;
using System.Linq;
namespace SharedTrip.Data
{
    public class DataConstants
    {
        public const int IdMaxLength = 40;

        public const int UserMinLength = 5;
        public const int UserMaxLength = 20;
        public const int PassWordMinLength = 6;
        public const int PasswordMaxLength = 20;
        public const string EmailTemplate = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const int MinSeats = 2;
        public const int MaxSeats = 6;
        public const int TripDescriptionMaxLength = 80;
        

        
    }
}
