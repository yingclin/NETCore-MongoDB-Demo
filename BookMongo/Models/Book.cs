using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookMongo.Models
{
    namespace BookMongo.Models
    {
        public class Book
        {
            public ObjectId Id { get; set; }
            [BsonElement("book-id")]
            public string BookId { get; set; }
            [BsonElement("book-name")]
            public string BookName { get; set; }
            [BsonElement("price")]
            public int Price { get; set; }
            [BsonElement("category")]
            public string Category { get; set; }
            [BsonElement("author")]
            public string Author { get; set; }
        }
    }
}
