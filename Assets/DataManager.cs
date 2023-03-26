using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{

    public static DataManager instance;

    [SerializeField] private static string jsonPath;
    [SerializeField] private save loadedData = new save();

    [System.Serializable]
    public class save
    {
        public List<int> trophey;
    }

    private void Awake()
    {
        instance = this;
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
        loadedData = JsonUtility.FromJson<save>(File.ReadAllText(jsonPath));
        if (loadedData == null)
        {
            loadedData = new save();
            loadedData.trophey = new List<int>();
        }
    }   

    public bool alreadyExists (int ID)
    {
        if (loadedData.trophey != null) return loadedData.trophey.Contains(ID);
        return false;
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
