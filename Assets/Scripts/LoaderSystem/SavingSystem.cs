using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace LoaderSystem
{
    public static class SavingSystem
    {
        public static void Save<T>(T stats, SaveName saveName)
        {
            var formatter = new BinaryFormatter();
            var path = Application.dataPath + $"/{saveName.ToString()}.data";
            var stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, stats);
            stream.Close();
        }

        public static T Load<T>(SaveName saveName, T defaultValue = default) where T : class, IStats
        {
            var path = Application.dataPath + $"/{saveName.ToString()}.data";
            if(File.Exists(path))
            {
                var formatter = new BinaryFormatter();
                var stream = new FileStream(path, FileMode.Open);

                var data = formatter.Deserialize(stream) as T;
                
                return data;
            }

            Debug.LogError("Safe file not found in " + path);
            return defaultValue;
        }
    }
}