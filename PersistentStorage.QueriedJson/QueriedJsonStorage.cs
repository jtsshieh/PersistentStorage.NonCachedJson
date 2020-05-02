using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PersistentStorage.Queried;

namespace PersistentStorage.QueriedJson
{
    public class QueriedJsonStorage<T> : IQueriedStorageMethod<T>
    {
        public JSONProperties Properties { get; set; }

        public string Name
        { 
            get
            { 
                return "PersistentStorage.QueriedJsonStorage"; 
            } 
        }

        public void Initialize(IProperties Properties)
        {
            this.Properties = (JSONProperties)Properties;
            if (File.Exists(Path.Combine(this.Properties.DataFilePath, this.Properties.DataFile)))
            {
                return;
            }
            Directory.CreateDirectory(this.Properties.DataFilePath);
            File.Create(this.Properties.DataFile).Dispose();
            File.WriteAllText(Path.Combine(this.Properties.DataFilePath, this.Properties.DataFile), JsonConvert.SerializeObject(Activator.CreateInstance<T>()));
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public B GetValue<B>(string query)
        {
            string JsonFile = File.ReadAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile));

            return QueryParser.GetValue<B>(query, JsonConvert.DeserializeObject(JsonFile));
        }

        public void SetValue<B>(string query, B value)
        {
            string JsonFile = File.ReadAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile));

            SaveObject(QueryParser.SetValue(query, JsonConvert.DeserializeObject(JsonFile), value));
        }

        public void InsertArray<B>(string query, B item, int index)
        {
            string JsonFile = File.ReadAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile));

            SaveObject(QueryParser.InsertArray(query, JsonConvert.DeserializeObject(JsonFile), item, index));
        }

        public void PushArray<B>(string query, B item)
        {
            string JsonFile = File.ReadAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile));

            SaveObject(QueryParser.PushArray(query, JsonConvert.DeserializeObject(JsonFile), item));
        }

        public void RemoveAtArray<B>(string query, int index)
        {
            string JsonFile = File.ReadAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile));

            SaveObject(QueryParser.RemoveAtArray<B>(query, JsonConvert.DeserializeObject(JsonFile), index));
        }

        public void RemoveArray<B>(string query, B item)
        {
            string JsonFile = File.ReadAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile));

            SaveObject(QueryParser.RemoveArray(query, JsonConvert.DeserializeObject(JsonFile), item));
        }

        private void SaveObject(object Object)
        {
            File.WriteAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile), JsonConvert.SerializeObject(Object));
        }
    }
}
