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

        public void SetValue(string Query, string Value)
        {
            string JsonFile = File.ReadAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile));

            JObject Object = JsonConvert.DeserializeObject(JsonFile) as JObject;

            JToken Token = Object.SelectToken(Query);

            Token.Replace(Value);

            File.WriteAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile), Object.ToString());
        }

        public void SetValue(string Query, int Value)
        {
            string JsonFile = File.ReadAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile));

            JObject Object = JsonConvert.DeserializeObject(JsonFile) as JObject;

            JToken Token = Object.SelectToken(Query);

            Token.Replace(Value);

            File.WriteAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile), Object.ToString());
        }

        public void SetValue(string Query, bool Value)
        {
            string JsonFile = File.ReadAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile));

            JObject Object = JsonConvert.DeserializeObject(JsonFile) as JObject;

            JToken Token = Object.SelectToken(Query);

            Token.Replace(Value);

            File.WriteAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile), Object.ToString());
        }

        public string GetString(string Query)
        {
            string JsonFile = File.ReadAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile));

            JObject Object = JsonConvert.DeserializeObject(JsonFile) as JObject;

            JToken Token = Object.SelectToken(Query);

            return Token.ToObject<string>();
        }

        public int GetInt(string Query)
        {
            string JsonFile = File.ReadAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile));

            JObject Object = JsonConvert.DeserializeObject(JsonFile) as JObject;

            JToken Token = Object.SelectToken(Query);

            return Token.ToObject<int>();

        }

        public bool GetBool(string Query)
        {
            string JsonFile = File.ReadAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile));

            JObject Object = JsonConvert.DeserializeObject(JsonFile) as JObject;

            JToken Token = Object.SelectToken(Query);

            return Token.ToObject<bool>();

        }

        public object GetType(string Query)
        {
            string JsonFile = File.ReadAllText(Path.Combine(Properties.DataFilePath, Properties.DataFile));

            JObject Object = JsonConvert.DeserializeObject(JsonFile) as JObject;

            JToken Token = Object.SelectToken(Query);

            return Token.ToObject<object>();
        }
    }
}
