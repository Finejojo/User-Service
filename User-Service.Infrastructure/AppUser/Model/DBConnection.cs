namespace UserMicroservices.Entity
{
    public class DBConnection
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;

    }
}
