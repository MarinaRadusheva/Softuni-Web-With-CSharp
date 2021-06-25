namespace CarShop.Data
{
    public class DataConstatnts
    {
        public const int StringMaxLength = 20;
        public const int IdMaxLength = 40;
        public const int MinLengthUsername = 4;
        public const int MinLengthPassword = 5;
        public const string UserTypeMechanic = "Mechanic";
        public const string UserTypeClient = "Client";
        public const int CarModelMinLength = 5;
        public const int CarPlateNumberMaxLength = 8;
        public const string CarPlateTemplate = @"[A-Z]{2}[0-9]{4}[A-Z]{2}";
        public const int MinLengthIssue = 5;
    }
}
