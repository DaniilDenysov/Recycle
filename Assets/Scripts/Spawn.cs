using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] SceneBackgraund;
    public Vector2 [] ClaudSpawns;// l // r
    [SerializeField] private Vector2 SpawnArea, PartArea;
    public int TrashChoice, ClaudNum, Map;
    public float TrashSpawnRange, PartSpawnRange = 0.15f, CloudSpawnRange = 10f;
    public GameObject  Part, nowTrash, Claud, Trash, PauseBtn;
    [SerializeField] private Color [] BackgraundColor;
    [SerializeField] private Camera Cam;
    [SerializeField] private UniversalSceneController USC;
    [SerializeField] private PanelController Panel;
    void Start()
    {
      //  Reg[Random.Range(0, 4)].SetActive(true);
        Map = Random.Range(0, SceneBackgraund.Length - 1);
        SceneBackgraund[Map].SetActive(true);
        Cam.backgroundColor = BackgraundColor[Map];
        InvokeRepeating("ClaudSpawn", CloudSpawnRange, CloudSpawnRange);
        InvokeRepeating("TrashSpawn", TrashSpawnRange, TrashSpawnRange);
       // InvokeRepeating("SpawnPart", PartSpawnRange, PartSpawnRange);
    }
    public void Continue ()
    {
        PauseBtn.GetComponent<Pause>().Continue();
    }
    void TrashSpawn ()
    {
        if (USC.isPaused == false)
        {          
            nowTrash = Instantiate(Trash, transform.position = new Vector2(Random.Range(SpawnArea.x, -SpawnArea.x), SpawnArea.y), Quaternion.identity);
            nowTrash.GetComponent<TrashController>().Panel = Panel;
            nowTrash.GetComponent<TrashController>().USC = USC;
        }
    }
    void ClaudSpawn()
    {
        GameObject Cl = Instantiate(Claud, ClaudSpawns[Random.Range(0, ClaudSpawns.Length - 1)], Quaternion.identity);
        Cl.GetComponent<Cloud>().dir = false;
    }
    void SpawnPart()
    {
        Instantiate(Part, new Vector2(Random.Range(PartArea.x, -PartArea.x), PartArea.y), Quaternion.Euler(0, 0, Random.Range(0, 90)));
    }
}
