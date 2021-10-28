using UnityEngine;

public static class LoaderSystem
{
    public static string JsonSerialization<T> (T serializatingObject)
    {
        var json = JsonUtility.ToJson(serializatingObject);
        return json;
    }

    public static T JsonDeserialization<T>(string key)
    {
        var deserializedObject = JsonUtility.FromJson<T>(key);
        return deserializedObject;
    }
}