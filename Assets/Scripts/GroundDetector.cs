using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Movement3D.isGrounded = true;
    }
    private void OnTriggerExit(Collider other)
    {
        Movement3D.isGrounded = false;
    }
}
