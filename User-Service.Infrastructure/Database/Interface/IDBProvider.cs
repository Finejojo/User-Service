using MongoDB.Driver;

    public interface IDBProvider
    {
        public IMongoDatabase Connect();
    }

