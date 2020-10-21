namespace API.Helpers
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ValuesCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
        public string VisitsCollectionName { get; set; }
        public string CalendarNotesCollectionName { get; set; }
        public string FamiliesCollectionName { get; set; }
        public string PrivateNotesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ResetPasswordsName { get; set; }
    }

    public interface IDatabaseSettings
    {
        string ResetPasswordsName { get; set; }
        string ValuesCollectionName { get; set; }
        string UsersCollectionName { get; set; }
        string VisitsCollectionName { get; set; }
        string CalendarNotesCollectionName { get; set; }
        string FamiliesCollectionName { get; set; }
        string PrivateNotesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}