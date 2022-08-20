using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Color [] background,ground;
    [SerializeField] private GameObject[] render;
    public int Map;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Season") || PlayerPrefs.GetInt("Season") == 4) Map = Random.Range(0, background.Length);
        else Map = PlayerPrefs.GetInt("Season");
        Camera.main.backgroundColor = background[Map];
        render[Map].SetActive(true);
    }

}
