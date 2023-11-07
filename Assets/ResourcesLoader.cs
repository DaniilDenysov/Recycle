using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ResourcesLoader : MonoBehaviour
{
    [SerializeField] private Spawn[] spawns;

    private void Start()
    {
        spawns = FindObjectsOfType<Spawn>();
    }

    private void Update()
    {
        foreach (Spawn s in spawns)
        {
            s.ClearRescources();
            List<ResourcesListWrapper> _resources = new List<ResourcesListWrapper>();
        
            foreach (string link in s.GetResourcesLinks())
            {
                GameObject[] loadedResources = Resources.LoadAll<GameObject>(link);
                List<RescourceObject> chapter = new List<RescourceObject>();

                foreach (GameObject resource in loadedResources)
                {
                    chapter.Add(new RescourceObject(resource));
                }

                _resources.Add(new ResourcesListWrapper(chapter));
            }
            s.RefreshResources(_resources);
        }
    }
}
