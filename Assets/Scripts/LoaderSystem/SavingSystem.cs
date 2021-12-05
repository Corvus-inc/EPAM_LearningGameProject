using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace LoaderSystem
{
    public static class SavingSystem
    {
        public static void Save<T>(T stats, SaveName saveName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.dataPath + $"/{saveName.ToString()}.data";
            FileStream stream = new FileStream(path, FileMode.Create);

            var data =  stats;
            
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static T Load<T>(SaveName saveName, T defaultValue = default) where T : class, IStats
        {
            string path = Application.dataPath + $"/{saveName.ToString()}.data";
            if(File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                T data = formatter.Deserialize(stream) as T;
                
                return data;
            }
            else
            {
                Debug.LogError("Safe file not found in " + path);
                return defaultValue;
            }
        }
    }
}