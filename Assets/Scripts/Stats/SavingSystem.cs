using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

namespace Stats
{
    public static class SavingSystem
    {
        public static void Save<T>(T playerStats, string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.dataPath + $"/{fileName}.data";
            FileStream stream = new FileStream(path, FileMode.Create);

            var data =  playerStats;
            
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static T Load<T>(string fileName, T defaultValue = default) where T : PlayerStats
        {
            string path = Application.dataPath + $"/{fileName}.data";
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