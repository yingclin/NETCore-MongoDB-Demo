# .NET Core 2.1 存取 MongoDB 之 CRUD 範例

ASP&#x2E;NET Core 2.1 存取 MongoDB 的 CRUD 範例專案。

## 背景資訊

以簡單的 Book 類別為資料單元做基本 CRUD 的存取示範。

* .NET MongoDB Driver 的版本相容資訊
  * 2.7 版 才支援 MongoDB 4.0。

### 專案的執行環境

* Visual Studio 2017 15.8.7
* .NET Core 2.1
* Mongo.Driver 2.7
* MongoDB 4 (Docker)

## 安裝 MongoDB.Driver

安裝最新版，今時今日為版本 2.7.0。

* 至 [NuGet 套件管理員] 搜尋 "MongoDB.Driver" 並安裝。  

  或
* 至 [套件管理主控台] 執行
    ```
    Install-Package MongoDB.Driver
    ```
## 建立 Model

### Book.cs
```
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
```
* `ObjectId` 用來儲存 MongoDB 產生的專用識別碼。
* 利用 `BsonElement` 來指定欄位名稱。

## 取得 Book 的 Collection

### 建立 Client

建立起 `MongoClient`。  

初始化 MongoClient 的連線寫法有很多種，範例使用最基本的連線字串寫法，更多寫法可看 參考資料2。

1) 使用預設連線 (localhost:27017)
    ```
    MongoClient client = new MongoClient();
    ```
2) 連線字串
    ```
    MongoClient client = new MongoClient("mongodb://localhost:27017");
    ```
3) 密碼保護的連線字串
    ```
    MongoClient client= new MongoClient("mongodb://{username}:{password}@{host}:{port}/{Database}")
    如：  
    MongoClient client= new MongoClient("mongodb://user1:password1@127.0.0.1:27017/testdb");
    ```

### 拿到 Collection - IMongoCollection<T>

取得 Database
```
IMongoDatabase db = client.GetDatabase(DbName);
```
取得 Collection
```
IMongoCollection<Book> bookCollection = db.GetCollection<Book>(CollectionName);
```

## 以 Collection 做 CRUD

### List
```
collection.Find(new BsonDocument()).ToList();
或
collection.AsQueryable().ToList();
```
### Get
id：MongoDB ObjectId
```
var filter = Builders<Book>.Filter.Eq(o => o.Id, id);
Book book = collection.Find(filter).FirstOrDefault();
```
### Create
```
collection.InsertOne(book);
```
### Update
```
var filter = Builders<Book>.Filter.Eq(o => o.Id, id);
ReplaceOneResult result = BookCollection.ReplaceOne(filter, book);
```
### Delete
```
var filter = Builders<Book>.Filter.Eq(o => o.Id, id);
DeleteResult result = collection.DeleteOne(filter);
```

## 參考資料及圖片來源

1. [C# and .NET MongoDB Driver](https://docs.mongodb.com/ecosystem/drivers/csharp/)
2. [C# 搭配 MongoDB 的連線寫法](https://blog.yowko.com/mongodb-connectionstring/)
3. [Update all properties of object in MongoDb](https://stackoverflow.com/questions/30893012/update-all-properties-of-object-in-mongodb)
4. [Docker版MongoDB的安裝](https://www.jianshu.com/p/2181b2e27021)
