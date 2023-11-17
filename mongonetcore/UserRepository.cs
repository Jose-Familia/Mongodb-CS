using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace mongonetcore
{
    /// <summary>
    /// Class used to access Mongo DB.
    /// </summary>
    public class UsersRepository
    {
        private IMongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<User> _usersCollection;

        public UsersRepository(string connectionString)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("blog");
            _usersCollection = _database.GetCollection<User>("users");
        }

        /// <summary>
        /// Checking is connection to the database established.
        /// </summary>
        public bool CheckConnection()
        {
            try
            {
                _database.ListCollections();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returning all data from 'users' collection.
        /// </summary>
        public async Task<List<User>> GetAllUsers()
        {
            return await _usersCollection.Find(new BsonDocument()).ToListAsync();
        }

        /// <summary>
        /// Returning all users with the defined value of defined field.
        /// </summary>
        public async Task<List<User>> GetUsersByField(string fieldName, string fieldValue)
        {
            var filter = Builders<User>.Filter.Eq(fieldName, fieldValue);
            var result = await _usersCollection.Find(filter).ToListAsync();

            return result;
        }

        /// <summary>
        /// Inserting passed user into the database.
        /// </summary>
        public async Task InsertUser(User user)
        {
            await _usersCollection.InsertOneAsync(user);
        }

        /// <summary>
        /// Removing user with defined _id.
        /// </summary>
        /// <returns>
        /// True - If user was deleted.
        /// False - If user was not deleted.
        /// </returns>
        public async Task<bool> DeleteUserById(ObjectId id)
        {
            var filter = Builders<User>.Filter.Eq("_id", id);
            var result = await _usersCollection.DeleteOneAsync(filter);
            return result.DeletedCount != 0;
        }

        /// <summary>
        /// Removing all data from 'users' collection. 
        /// </summary>
        /// <returns>
        /// Number of deleted users.
        /// </returns>
        public async Task<long> DeleteAllUsers()
        {
            var filter = new BsonDocument();
            var result = await _usersCollection.DeleteManyAsync(filter);
            return result.DeletedCount;
        }

    }
//Main function to run the delete user functions 
    public class Test {
        static async Task Main(string[] args){
            UsersRepository _mongoDbRepo = new UsersRepository("mongodb://127.0.0.1:27017");
            var user = new User()
            {
                Name = "Nikola",
                Age = 30,
                Blog = "rubikscode.net",
                Location = "Beograd"
            };
            await _mongoDbRepo.InsertUser(user);
            user = new User()
            {
                Name = "Vanja",
                Age = 27,
                Blog = "eventroom.net",
                Location = "Beograd"
            };
            await _mongoDbRepo.InsertUser(user);
            user = new User()
            {
                Name = "Simona",
                Age = 0,
                Blog = "babystuff.com",
                Location = "Beograd"
            };
            await _mongoDbRepo.InsertUser(user);
            Console.WriteLine("List of all Users");
            var users = await _mongoDbRepo.GetAllUsers();
             foreach (var doc in users)
            {
                Console.WriteLine(doc.Name);
            }
            Console.WriteLine("Deleting the User Simona");
            var deleteUser = await _mongoDbRepo.GetUsersByField("name", "Simona");
            var result = await _mongoDbRepo.DeleteUserById(deleteUser.Single().Id);
            Console.WriteLine("List of Users after deletion");
            users = await _mongoDbRepo.GetAllUsers();
             foreach (var doc in users)
            {
                Console.WriteLine(doc.Name);
            }
            Console.WriteLine("Deleting All Users");
            await _mongoDbRepo.DeleteAllUsers();
            users = await _mongoDbRepo.GetAllUsers();
            Console.WriteLine("List After Deleting All users");
            foreach (var doc in users)
            {
                Console.WriteLine(doc.Name);
            }
        }
    }
}