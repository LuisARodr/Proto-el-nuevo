using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class Bullet : PooledObjectBehavior {
    
    [SerializeField]
    float velocity;
    Rigidbody bulletRigidBody;
    
    bool collide;

    Vector3 lastPosition;

    override protected void Awake() {
        bulletRigidBody = GetComponent<Rigidbody>();
    }
    
    override protected void Start() {
        base.Start();
        collide = false;
        bulletRigidBody.velocity = transform.forward * velocity * Time.deltaTime;
    }
    
    override protected void Update() {
        base.Update();
        if (!collide) {
            lastPosition = transform.position;
            bulletRigidBody.velocity = transform.forward * velocity * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        collide = true;
        bulletRigidBody.velocity = Vector3.zero;
        objectPooler.GetObjectFromPool("Explosion", lastPosition, true);
        ReturnBulletToPool();
    }
    private void OnTriggerEnter(Collider other) {
        collide = true;
        bulletRigidBody.velocity = Vector3.zero;
        objectPooler.GetObjectFromPool("Explosion", lastPosition, true);
        ReturnBulletToPool();
    }

    override protected void ReturnBulletToPool() {
        collide = false;
        base.ReturnBulletToPool();
    }

}
