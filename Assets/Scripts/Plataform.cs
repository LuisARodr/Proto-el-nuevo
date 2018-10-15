using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour {

    public GameObject Player;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entro m");
        if (other.gameObject.name == "StandingCollider")
        {
            Player.transform.parent = transform;
            Debug.Log("Entro");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Entro s");
        if (other.gameObject.name == "StandingCollider")
        {
            Player.transform.parent = null;
            Debug.Log("Salio");
        }
    }



}
