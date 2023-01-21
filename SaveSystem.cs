using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer (PlayerStats playerstats)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + PlayerStats.fileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(playerstats);
        //Debug.Log(Application.persistentDataPath);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + PlayerStats.fileName;
        
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void DeletePlayer1()
    {
        if (File.Exists(Application.persistentDataPath + "/profile1.data"))
        {
            File.Delete(Application.persistentDataPath + "/profile1.data");
        }

        else
        {
            Debug.LogError("Save file not found");
        }
    }
    public static void DeletePlayer2()
    {
        if (File.Exists(Application.persistentDataPath + "/profile2.data"))
        {
            File.Delete(Application.persistentDataPath + "/profile2.data");
        }

        else
        {
            Debug.LogError("Save file not found");
        }
    }
    public static void DeletePlayer3()
    {
        if (File.Exists(Application.persistentDataPath + "/profile3.data"))
        {
            File.Delete(Application.persistentDataPath + "/profile3.data");
        }

        else
        {
            Debug.Log("Save file not found");
            return;
        }
    }
}
