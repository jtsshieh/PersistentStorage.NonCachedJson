using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PersistentStorage.NonCached;

namespace PersistentStorage.NonCachedJson
{
    public class NonCachedJsonStorage<T> : INonCachedStorageMethod<T>
    {
        public JSONProperties Properties { get; set; }

        public string Name
        { 
            get
            { 
                return "PersistentStorage.NonCachedJsonStorage"; 
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
            
            object Object = QueryParser.SetValue(query, JsonConvert.DeserializeObject(JsonFile), value);

            File.WriteAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile), JsonConvert.SerializeObject(Object));
        }
    }
}
