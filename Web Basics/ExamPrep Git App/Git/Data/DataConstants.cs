namespace Git.Data
{
    public class DataConstants
    {
        public const int IdMaxLength = 40;

        public const int UserMinLength = 5;
        public const int UserMaxLength = 20;
        public const int PassWordMinLength = 6;
        public const int PasswordMaxLength = 20;
        public const string EmailTemplate = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const int RepoMinLength = 3;
        public const int RepoMaxLength = 10;
        public const string RepoTypePublic = "Public";
        public const string RepoTypePrivate = "Private";

        public const int CommitMinLength = 5;
    }
}
