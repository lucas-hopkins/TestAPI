using Dapper;
using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Repositories
{
    public class ActorRepository
    {
        private readonly IDbConnection _dbConnection;

        public ActorRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        
        public  object GetByID(int id)
        {
            string sQuery = "SELECT first_name, last_name FROM actor WHERE actor_id = @Id";
            var result = _dbConnection.QueryFirstOrDefault(sQuery, new { Id = id });
            return result;
        }

        public IEnumerable<Actor> GetAll()
        {
            string sQuery = "SELECT * FROM actor";
            var actors = new List<Actor>();
            using (var reader = _dbConnection.ExecuteReader(sQuery))
            {
                while (reader.Read())
                {
                    Actor a = new Actor();
                    a.Id = reader.GetInt32(reader.GetOrdinal("actor_id"));
                    a.FirstName = reader.GetString(reader.GetOrdinal("first_name"));
                    a.LastName = reader.GetString(reader.GetOrdinal("last_name"));
                    a.LastUpdate = reader.GetDateTime(reader.GetOrdinal("last_update"));
                    actors.Add(a);
                }
            }
            return actors;
        }

        public void AddActor(string FName, string LName)
        {
            string sQuery = "INSERT INTO actor (first_name, last_name,last_update) values (@FirstName, @LastName, NOW())";
            _dbConnection.Execute(sQuery, new { FirstName = FName, LastName = LName });
        }

        public void DeleteActor(int id)
        {
            string sQuery = "DELETE FROM actor WHERE actor_id = @Id";
            _dbConnection.Execute(sQuery, new { Id = id });
        }
    }


}
