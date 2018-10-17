using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.SystemControls;

public class BulletSpawner : MonoBehaviour {

    ObjectPooler objectPooler;
    
    bool facingRight;


    public Animator anim;

    private void Start() {
        objectPooler = ObjectPooler.Instance;
        facingRight = true;
    }

    // Use this for initialization
    private void Update() {
        float axis = Input.GetAxis("Horizontal");
        if (axis > 0) {
            facingRight = true;
        } else if (axis < 0) {
            facingRight = false;
        }
        if (Controllers.GetButton(1, "X", 2)) {
            anim.SetTrigger("Shoot");
            objectPooler.GetObjectFromPool("Blast", transform.position, facingRight);
            objectPooler.GetObjectFromPool("Bullet", transform.position, facingRight);
        }
    }
}
