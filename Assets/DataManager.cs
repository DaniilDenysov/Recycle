using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    [SerializeField] private static string jsonPath;
    private save loadedData = new save();

    public class save
    {
        public List<int> trophey;
    }

    void Start()
    {
        jsonPath = Application.dataPath + "/saveFile.json";
        if (File.Exists(jsonPath))
        {
            LoadData();
        }
        else
        {
            loadedData.trophey = new List<int>();
        }
        Debug.Log(jsonPath);
    }

    public void LoadData ()
    {
        loadedData.trophey = new List<int>();
        loadedData = JsonUtility.FromJson<save>(File.ReadAllText(jsonPath));
    }   

    public bool alreadyExists (int ID)
    {
        return loadedData.trophey.Contains(ID);
    }

    public void saveData (int ID)
    {
        save newSave = new save();
        newSave.trophey = new List<int>();
        if (loadedData != null)
        {
            if (!loadedData.trophey.Contains(ID))
            {
                
                newSave.trophey.AddRange(loadedData.trophey);
                newSave.trophey.Add(ID);
                JsonUtility.ToJson(newSave, true);
                File.WriteAllText(jsonPath, JsonUtility.ToJson(newSave));
            }
        }
        else
        {
            newSave.trophey = new List<int>();
            newSave.trophey.Add(ID);
            JsonUtility.ToJson(newSave, true);
            File.WriteAllText(jsonPath, JsonUtility.ToJson(newSave));
        }
       
    }
}
