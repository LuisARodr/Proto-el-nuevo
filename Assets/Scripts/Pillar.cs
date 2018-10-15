using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour {

    [SerializeField]
    int golpes = 3; float TiempoParpadeo = .5f;
    [SerializeField]
    Material mat1,  mat2;
 
    bool coll = false;
    Material mat;
    MeshRenderer rend;
    private void Awake()
    {
        rend = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        if(TiempoParpadeo > 0 && coll)
        {
            TiempoParpadeo -= Time.deltaTime;
        }
        if(TiempoParpadeo  <0.01f)
        {
            TiempoParpadeo = .5f;
            coll = false;
            rend.material = mat1;
        } 
        if(golpes <= 0)
        {
            Destroy(this.gameObject);
            golpes = 3;
        }
    }
        private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Bullet(Clone)")
        {
            Debug.Log("eNTRO la bala");
            
            rend.material = mat2;
            coll = true;
            golpes--;
        }

    }      
}
