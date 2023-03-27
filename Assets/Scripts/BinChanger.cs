using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinChanger : MonoBehaviour
{

    [SerializeField] private State _state;
    private List<GameObject> _sortingList = new List<GameObject>();
    private List<Vector3> _positions = new List<Vector3>();
    [Range(0, 100)]
    [SerializeField] private float _gapBetween = 0.25f,_range = 5;
    [SerializeField] protected Color _rangeGizmosColor;

    public enum State
    {
        Linear,
        Binary  
    }

    public static BinChanger instance { get; set; }
    private int Copacity;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Bin[] bin = FindObjectsOfType<Bin>();
        foreach (Bin b in bin) _sortingList.Add(b.gameObject);
        if (PlayerPrefs.HasKey("Difficulty") && PlayerPrefs.GetInt("Difficulty") > 0) ChoseRandomly(PlayerPrefs.GetInt("Difficulty"));
        else ChoseRandomly(2);
    }

    private void OnDrawGizmos()
    {
        Bin[] bin = FindObjectsOfType<Bin>();
        foreach (Bin b in bin) _sortingList.Add(b.gameObject);
        Gizmos.color = _rangeGizmosColor;
        for (int i = 0; i < 4; i++)
        {
            Gizmos.DrawCube(new Vector3((-_range / 2) + (i * (_gapBetween * 2)), -3.75f, 1), new Vector3(0.3f,0.3f,0.3f));
            _sortingList[i].transform.position = new Vector3((-_range / 2) + (i * (_gapBetween * 2)), -3.75f, 1);
        }
        Gizmos.DrawCube(new Vector3(transform.position.x, -3.75f, transform.position.z),
         new Vector3(_range, 0.1f, 0));
        _sortingList.Clear();
    }

    public int GetCopacity() => Copacity;

    public void ChoseRandomly (int copacity)
    {
        Copacity = copacity;
        for (int i = _sortingList.Count; i > copacity; i--)
        {
            Debug.Log("Destroyed:" + i);
            Destroy(_sortingList[i - 1].gameObject);
            _sortingList.Remove(_sortingList[i - 1]);
        }

        switch (_state)
        {
            case State.Linear:
                for (int i = 0; i <= _sortingList.Count; i++)
                {
                    Debug.Log("S:" + i);
                    int r = Random.Range(0, _sortingList.Count);
                    _sortingList[r].transform.position = new Vector3((-_range/2) + (i*(_gapBetween*2)), _sortingList[r].transform.position.y, _sortingList[r].transform.position.z);
                    _sortingList.Remove(_sortingList[r]);
                }

               break;
            case State.Binary:
               
                for (int i = 0; i < 4; i++)
                {
                    _positions.Add(new Vector3((-_range / 2) + (i * (_gapBetween * 2)), _sortingList[0].transform.position.y, _sortingList[0].transform.position.z));
                    Debug.Log("Pos:" + (-_range / 2) + (i * (_gapBetween * 2)));
                }
                while (_sortingList.Count > 0)
                {
                    int _randomPosition = Random.Range(0, _positions.Count);
                    int _randomBin = Random.Range(0, _sortingList.Count);
                    _sortingList[_randomBin].transform.position = _positions[_randomPosition];
                    _sortingList.Remove(_sortingList[_randomBin]);
                    _positions.Remove(_positions[_randomPosition]);
                }

                break;
        }
    }
}
