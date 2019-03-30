using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static int EXP = 0;
    public static int highestLevel = 1;

    public static void SaveGame(int EXP, int level)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savegame";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(EXP, level);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGame() {
        string path = Application.persistentDataPath + "/savegame";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            EXP = data.EXP;
            highestLevel = data.level;
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
    
    public static bool IsSaveFile()
    {
        string path = Application.persistentDataPath + "/savegame";
        return File.Exists(path);
    }
}
