using System.Collections;
using System.Collections.Generic;
using HyperCasualPack;
using HyperCasualPack.Pickables;
using HyperCasualPack.Pools;
using UnityEngine;

public class CookieMachine : PickableCollectorBase
{
    [SerializeField] PickablePoolerSO _poolerSO;
    public Transform cookieSpawnTransform;
    
    void Awake()
    {
        spawnedPickables = new Stack<Pickable>();
    }

    public void SpawnCookie()
    {
        if (spawnedPickables.Count <= 8)
        {
            Pickable pickable = _poolerSO.TakeFromPool();
            pickable.transform.SetPositionAndRotation(cookieSpawnTransform.position,Quaternion.identity);
            spawnedPickables.Push(pickable);
        }

    }
    protected override IEnumerator CollectFromPlayer(InventoryManager inventoryManager)
    {
        yield return null;
        
    }

    public override PickablePoolerSO GetPool()
    {
        return _poolerSO;
    }

}
