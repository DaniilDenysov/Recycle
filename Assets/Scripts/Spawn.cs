using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Collections.ObjectModel;
using Unity.Jobs;

public class Spawn : MonoBehaviour
{
    [SerializeField] protected string _prefabName;
    [SerializeField] protected List<string> _resourcesLink;
    [SerializeField] protected List<ResourcesListWrapper> _resources = new List<ResourcesListWrapper>();
    [SerializeField] protected bool _loadDirectly = false;
    [Range(0, 100)]
    [SerializeField] protected float _timeRange = 2f, _spawnRange = 10f;
    [SerializeField] protected Color _spawnRangeGizmosColor;
    [Range(0, 3)]
    [SerializeField] protected int _stackSize = 3;
    private float _timeToNextSpawn;
    private int _resourcesCount;
    private bool _gameStopped = false;
    protected Queue<ObjectData> stack = new Queue<ObjectData>();

    public virtual void Start()
    {
        EventManager.OnGameStateChanged += EventManager_OnGameStateChanged;
        _timeToNextSpawn = _timeRange;
        SubscribeToTimeChanges();
        _resourcesCount = CountAllObjects();
    }

    private void EventManager_OnGameStateChanged()
    {
        _gameStopped = !_gameStopped;
    }

    public string GetPrefabName() { return _prefabName; }
    public List<string> GetResourcesLinks () { return _resourcesLink; }
    public void ClearRescources ()
    {
        _resources.Clear();
    }
    public void RefreshResources(List<ResourcesListWrapper> list)
    {
        _resources = list;
    }

    public class ObjectData 
    {
        private int _localRange = 0, _prefabNumber = 0;

        public ObjectData(int _localRange, int _prefabNumber)
        {
            this._localRange = _localRange;
            this._prefabNumber = _prefabNumber;
        }
        public int GetLocalRange()
        {
            return _localRange;
        }
        public int GetPrefabNumber()
        {
            return _prefabNumber;
        }
        public override bool Equals(object obj)
        {
            return ((ObjectData)obj).GetLocalRange() == _localRange && ((ObjectData)obj).GetPrefabNumber() == _prefabNumber;
        }
        public override string ToString()
        {
            return "Loc. range: " + _localRange + " PrefabNumber: " + _prefabNumber;
        }
    }

    public virtual bool InStack (ObjectData garbage)
    {
        Debug.Log("Contains " + garbage + " " + stack.Contains(garbage));
        return stack.Contains(garbage);
    }

    IEnumerator Spawning ()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeRange);
            SpawnObject();
        }
    }

    public virtual void SubscribeToTimeChanges ()
    {
        TicTac.OnStartTimeChaneges += TicTac_OnStartTimeChaneges;
        TicTac.OnStopTimeChanges += TicTac_OnStopTimeChanges;
    }

    public virtual void TicTac_OnStopTimeChanges(object sender, int e)
    {
        _timeRange = ((_timeRange / e) * 100);
    }

    public virtual void TicTac_OnStartTimeChaneges(object sender, int e)
    {
        _timeRange = (_timeRange/100)*e;
    }
    public virtual void FixedUpdate ()
    {
        if (_gameStopped) return;
        _timeToNextSpawn -= Time.fixedDeltaTime;
        if (_timeToNextSpawn > 0) return;
        _timeToNextSpawn = _timeRange;
        SpawnObject();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = _spawnRangeGizmosColor;
        Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y, transform.position.z),
         new Vector3(_spawnRange*2, 0.1f, 0));
    }
   
    public virtual bool CanBeSpawned (GameObject prefab)
    {
        return true;
    }

    public virtual void SpawnObject ()
    {

        if (!_loadDirectly)
        {

            int _localRange = UnityEngine.Random.Range(0, _resourcesLink.Count);
            int _prefabNumber = UnityEngine.Random.Range(1, Resources.LoadAll<GameObject>(_resourcesLink[_localRange]).Length+1);

            if (stack.Count>=_resourcesCount)
            {
                Debug.Log("Stack overflow!");
                stack.Clear();
            }

            while (stack.Contains(new ObjectData(_localRange, _prefabNumber)))
            {
                _localRange = UnityEngine.Random.Range(0, _resourcesLink.Count);
                _prefabNumber = UnityEngine.Random.Range(1, Resources.LoadAll<GameObject>(_resourcesLink[_localRange]).Length+1);
            }

            GameObject temp = Resources.Load<GameObject>(_resourcesLink[_localRange] + _prefabName + _prefabNumber);
          //  if (CanBeSpawned(temp))
       //     {
                Instantiate(temp, new Vector3(UnityEngine.Random.Range(_spawnRange, -_spawnRange), transform.position.y, 2), Quaternion.identity);
                stack.Enqueue(new ObjectData(_localRange, _prefabNumber));
          //  }
        }
        else
        {
            int _chapter = UnityEngine.Random.Range(0, _resources.Count);
            Instantiate(_resources[_chapter]._resources[UnityEngine.Random.Range(0, _resources[_chapter]._resources.Count)].GetPrefab(), new Vector3(UnityEngine.Random.Range(_spawnRange, -_spawnRange), transform.position.y, 2), Quaternion.identity);
        }
    }

    private int CountAllObjects()
    {
        int _count = 0;
        foreach (string _path in _resourcesLink)
        {
            _count += Resources.LoadAll<GameObject>(_path).Length;
        }
        return _count;
    }
}