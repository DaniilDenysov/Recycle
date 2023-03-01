using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text premiumCoin, goldCoin, silverCoin;
    [SerializeField] private string jsonPath;
    private Save loadedData = new Save();

  

    void Start()
    {
        jsonPath = Application.dataPath + "/saveFile.json";
        if (File.Exists(jsonPath))
        {
            LoadData();
        }
        else
        {
            loadedData.P = 0;
            loadedData.G = 0;
            loadedData.S = 0;
        }
        Debug.Log(jsonPath);
    }

    public void LoadData()
    {
        loadedData = JsonUtility.FromJson<Save>(File.ReadAllText(jsonPath));
        premiumCoin.text = $"{loadedData.P}";
        goldCoin.text = $"{loadedData.G}";
        silverCoin.text = $"{loadedData.S}";
    }

    public void SaveData()
    {
        loadedData = JsonUtility.FromJson<Save>(File.ReadAllText(jsonPath));
        premiumCoin.text = $"{loadedData.P}";
        goldCoin.text = $"{loadedData.G}";
        silverCoin.text = $"{loadedData.S}";
    }

    private void OnApplicationQuit()
    {
        File.WriteAllText(jsonPath, JsonUtility.ToJson(loadedData));
    }
}
