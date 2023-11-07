using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourcesListWrapper
{
    public List<RescourceObject> _resources = new List<RescourceObject>();
    public ResourcesListWrapper(List<RescourceObject> _resources)
    {
        this._resources = _resources;
    }
}
