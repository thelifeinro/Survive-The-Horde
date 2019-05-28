using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static int EXP = 0;
    public static int highestLevel = 1;
    public static int aLev = 0;
    public static int rLev = 0;
    public static int wLev = 0;
    public static int tLev = 0;

    public static void SaveGame(int EXP, int level, bool success, int aLev, int rLev, int wLev, int tLev)
    {
        if (success)
        {
            //unlock next level if locked
            if (highestLevel == level)
                highestLevel = level+1;
        }
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savegame";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(EXP, highestLevel, aLev, rLev, wLev, tLev);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void NewGame()
    {
        highestLevel = 1;
        EXP = 0;
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savegame";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(EXP, highestLevel, 0, 0,0, 0);

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
            aLev = data.aLev;
            rLev = data.rLev;
            wLev = data.wLev;
            tLev = data.tLev;

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
