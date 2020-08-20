
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Waka.Brokers
{
    public class StorageBroker<T> : IStorageBroker<T> where T : class
    {
        private string file { get; set; }

        public StorageBroker()
        {
            EntityName();
        }

        private void EntityName()
        {
            var fullJson = typeof(T).ToString().ToLower();
            var splitName = fullJson.Split(".").Reverse().ToArray();
            file = splitName[0];
        }

        public void Delete(T entity)
        {
            var initialJson = File.ReadAllText($"./Data/{file}.json");

            List<T> nigerianStates = JsonConvert.DeserializeObject<List<T>>(initialJson);
            var comingEntity = JsonConvert.SerializeObject(entity);

            List<T> newList = new List<T>();
            foreach (var item in nigerianStates)
            {
                var itemToDel = JsonConvert.SerializeObject(item);
                
                if (itemToDel != comingEntity)
                {
                    newList.Add(item);
                }
            }

            var completeJson = JsonConvert.SerializeObject(newList);
            File.WriteAllText($"./Data/{file}.json", completeJson);
        }

        public IEnumerable<T> GetAll()
        {
            var initialJson = File.ReadAllText($"./Data/{file}.json");
            List<T> nigerianStates = JsonConvert.DeserializeObject<List<T>>(initialJson);
            return nigerianStates;
        }

        public T Post(T entity)
        {
            var initialJson = File.ReadAllText($"./Data/{file}.json");
            if (initialJson.Length > 15)
            {
                List<T> publicPlaces = JsonConvert.DeserializeObject<List<T>>(initialJson);
                publicPlaces.Add(entity);
                var completeJson = JsonConvert.SerializeObject(publicPlaces);
                File.WriteAllText($"./Data/{file}.json", completeJson);
            }
            else
            {
                List<T> freshDb = new List<T>();
                freshDb.Add(entity);
                var completeJson = JsonConvert.SerializeObject(freshDb);
                File.WriteAllText($"./Data/{file}.json", completeJson);
            }
            
           

            return entity;
        }

        public void Update(T entityToUpdate, T compareEntity)
        {
            var initialJson = File.ReadAllText($"./Data/{file}.json");
            List<T> nigerianStates = JsonConvert.DeserializeObject<List<T>>(initialJson);
            var dbState = JsonConvert.SerializeObject(compareEntity);
            List<T> newList = new List<T>();
            foreach (var item in nigerianStates)
            {
                var itemToDel = JsonConvert.SerializeObject(item);

                if (itemToDel != dbState)
                {
                    newList.Add(item);
                }
            }
            newList.Add(entityToUpdate);
            var completeJson = JsonConvert.SerializeObject(newList);
            File.WriteAllText($"./Data/{file}.json", completeJson);

            
        }

    }
}
