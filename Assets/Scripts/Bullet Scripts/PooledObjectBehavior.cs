using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObjectBehavior : MonoBehaviour {

    protected ObjectPooler objectPooler;
    
    [SerializeField, Range(0, 1)]
    protected float maxLifeTime;
    protected float lifeTime;
    [SerializeField]
    protected string poolTag;

    protected virtual void Awake() {}
    
    protected virtual void Start () {
        objectPooler = ObjectPooler.Instance;
        lifeTime = 0;
    }
    
    protected virtual void Update () {
        lifeTime += Time.deltaTime;
        if (lifeTime > maxLifeTime) {
            ReturnBulletToPool();
        }
    }

    protected virtual void OnEnable() {}

    protected virtual void ReturnBulletToPool() {
        lifeTime = 0;
        objectPooler.ReturnObjectToPool(poolTag, gameObject);
    }
}
