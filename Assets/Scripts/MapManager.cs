using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance { get; set; }

    public Color [] background,ground;
    [SerializeField] private GameObject[] render;
    public int Map;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("Season") || PlayerPrefs.GetInt("Season") == 4) Map = Random.Range(0, background.Length);
        else Map = PlayerPrefs.GetInt("Season");
        Camera.main.backgroundColor = background[Map];
        render[Map].SetActive(true);
    }

}
