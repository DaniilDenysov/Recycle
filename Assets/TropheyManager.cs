using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TropheyManager : MonoBehaviour
{
    [SerializeField] private RectTransform list;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private string jsonPath;
    [SerializeField] private Sprite [] icons;
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
            instantiateList();
        }
        else
        {
            loadedData.trophey = new List<int>();
        }
        Debug.Log(jsonPath);
    }

    public void LoadData()
    {
        loadedData.trophey = new List<int>();
        loadedData = JsonUtility.FromJson<save>(File.ReadAllText(jsonPath));
    }

    public void instantiateList ()
    {
        GameObject temp;
        for (int i = 1;i < icons.Length;i++)
        {
           temp = Instantiate(iconPrefab, list.transform.position, Quaternion.identity);
           temp.transform.SetParent(list.transform);
            temp.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,0,0);
            temp.GetComponent<TropheyListPart>().assignTropheyInfo(loadedData.trophey.Contains(i),icons[i]);
        }
    }
}
