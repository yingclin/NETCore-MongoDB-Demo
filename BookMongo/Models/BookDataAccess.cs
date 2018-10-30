using BookMongo.Models.BookMongo.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMongo.Models
{
    public class BookDataAccess
    {
        private const string DbName = "BookstoreDb";
        private const string CollectionName = "Books";
        //
        private readonly IMongoDatabase _db;

        public BookDataAccess()
        {
            _db = CreateClient().GetDatabase(DbName);
        }

        private MongoClient CreateClient()
        {
            return new MongoClient($"mongodb://bookstoredb:bookstoredbpass@192.168.99.100:27017/{DbName}");
        }

        private IMongoCollection<Book> BookCollection => _db.GetCollection<Book>(CollectionName);

        public IEnumerable<Book> List()
        {
            return BookCollection.Find(new BsonDocument()).ToList();
            //return BookCollection.AsQueryable().ToList();
        }

        public Book Get(ObjectId id)
        {
            var filter = Builders<Book>.Filter.Eq(o => o.Id, id);
            return BookCollection.Find(filter).FirstOrDefault();
        }

        public Book Create(Book p)
        {
            BookCollection.InsertOne(p);
            return p;
        }

        public void Update(ObjectId id, Book p)
        {

            p.Id = id;
            var filter = Builders<Book>.Filter.Eq(o => o.Id, id);
            ReplaceOneResult result = BookCollection.ReplaceOne(filter, p);
        }

        public void Remove(ObjectId id)
        {
            var filter = Builders<Book>.Filter.Eq(o => o.Id, id);
            DeleteResult result = BookCollection.DeleteOne(filter);
        }
    }
}
