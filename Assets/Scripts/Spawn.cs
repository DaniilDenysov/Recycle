using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Unity.Jobs;

public class Spawn : MonoBehaviour
{
    [SerializeField] protected string _prefabName;
    [SerializeField] protected List<string> _resourcesLink;
    [Range(1, 100)]
    [SerializeField] protected float _timeRange = 2f,_spawnRange = 10f;
    [SerializeField] protected Color _spawnRangeGizmosColor;
    private float _timeToNextSpawn;
    protected List<GameObject> _cashedList;
    private bool _gameStopped = false;
    public virtual void Start()
    {
         InvokeRepeating(nameof(SpawnObject),_timeRange,_timeRange);
        _cashedList = new List<GameObject>();
        GameBrakeManager.OnBrake += GameBrakeManager_OnBrake;
     /*   for (int i = 0; i < _resourcesLink.Count; i++)
        {
            _cashedList.AddRange(Resources.Load<GameObject>(_resourcesLink[UnityEngine.Random.Range(0, _resourcesLink.Count)]));
        }*/
       // Thread newThread = new Thread(new ThreadStart(FixedUpdate));
       Thread newThread2 = new Thread(new ThreadStart(SpawnObject));
        //_timeRange *= 10;
        _timeToNextSpawn = _timeRange;
        SubscribeToTimeChanges();

      //  StartCoroutine(Spawning());
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

    private void TicTac_OnStopTimeChanges(object sender, int e)
    {
        _timeRange = ((_timeRange / e) * 100);
    }

    private void TicTac_OnStartTimeChaneges(object sender, int e)
    {
        _timeRange = (_timeRange/100)*e;
    }

   /* public virtual void FixedUpdate ()
    {
        if (_gameStopped) return;
        _timeToNextSpawn -= Time.fixedDeltaTime;
        if (_timeToNextSpawn > 0) return;
        _timeToNextSpawn = _timeRange;
        SpawnObject();
    }*/

/*

 * 
    private void OnDrawGizmos()
    {
        Gizmos.color = _spawnRangeGizmosColor;
        Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y, transform.position.z),
         new Vector3(_spawnRange, 0.1f, 0));
    }
    */
    public virtual bool CanBeSpawned (GameObject prefab)
    {
        return true;
    }

    public virtual void SpawnObject ()
    {
          int _localRange = UnityEngine.Random.Range(0, _resourcesLink.Count);
          int _prefabNumber = UnityEngine.Random.Range(1, Resources.LoadAll<GameObject>(_resourcesLink[_localRange]).Length);
          GameObject temp = Resources.Load<GameObject>(_resourcesLink[_localRange] + _prefabName + _prefabNumber);
          Debug.Log("Temp:" + temp + " " + _prefabName + _prefabNumber + " " + _resourcesLink[_localRange]);
     //   GameObject temp = _cashedList[UnityEngine.Random.Range(0, _cashedList.Count)];
        if (CanBeSpawned(temp)) Instantiate(temp, new Vector3(UnityEngine.Random.Range(_spawnRange, -_spawnRange),
            transform.position.y, 2), Quaternion.identity);

    }
}