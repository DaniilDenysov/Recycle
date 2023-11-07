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
    protected List<GameObject> _cashedList;
    private bool _gameStopped = false;
    protected ObservableCollection<StackPlate> stack = new ObservableCollection<StackPlate>();
    public virtual void Start()
    {
        _cashedList = new List<GameObject>();
        GameBrakeManager.OnBrake += GameBrakeManager_OnBrake;
      //  Thread spawnThread = new Thread(new ThreadStart(SpawnObject));
      //  Thread updateThread = new Thread(new ThreadStart(FixedUpdate));
        _timeToNextSpawn = _timeRange;
        SubscribeToTimeChanges();

        stack.CollectionChanged += OnStackChanged;
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

    public class StackPlate 
{
        private int _localRange = 0, _prefabNumber = 0;
        public StackPlate(int _localRange, int _prefabNumber)
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
            return ((StackPlate)obj).GetLocalRange() == _localRange && ((StackPlate)obj).GetPrefabNumber() == _prefabNumber;
        }
        public override string ToString()
        {
            return "Loc. range: " + _localRange + " PrefabNumber: " + _prefabNumber;
        }
    }

    public virtual void OnStackChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (stack.Count > _stackSize)
        {
            stack.RemoveAt(0);
        }
        foreach (StackPlate s in stack)
        {
            Debug.Log(s);
        }
    }

    public virtual bool InStack (StackPlate garbage)
    {
        Debug.Log("Contains " + garbage + " " + stack.Contains(garbage));
        return stack.Contains(garbage);
    }

    private void GameBrakeManager_OnBrake(object sender, bool e)
    {
        _gameStopped = e;
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
            int _prefabNumber = UnityEngine.Random.Range(1, Resources.LoadAll<GameObject>(_resourcesLink[_localRange]).Length + 1);

            while (stack.Contains(new StackPlate(_localRange, _prefabNumber)))
            {
                _localRange = UnityEngine.Random.Range(0, _resourcesLink.Count);
                _prefabNumber = UnityEngine.Random.Range(1, Resources.LoadAll<GameObject>(_resourcesLink[_localRange]).Length + 1);
            }

            GameObject temp = Resources.Load<GameObject>(_resourcesLink[_localRange] + _prefabName + _prefabNumber);
          //  if (CanBeSpawned(temp))
       //     {
                Instantiate(temp, new Vector3(UnityEngine.Random.Range(_spawnRange, -_spawnRange), transform.position.y, 2), Quaternion.identity);
                stack.Add(new StackPlate(_localRange, _prefabNumber));
          //  }
        }
        else
        {
            int _chapter = UnityEngine.Random.Range(0, _resources.Count);
            Instantiate(_resources[_chapter]._resources[UnityEngine.Random.Range(0, _resources[_chapter]._resources.Count)].GetPrefab(), new Vector3(UnityEngine.Random.Range(_spawnRange, -_spawnRange), transform.position.y, 2), Quaternion.identity);
        }
    }
}