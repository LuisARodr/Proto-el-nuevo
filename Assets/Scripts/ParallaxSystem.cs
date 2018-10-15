using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class ParallaxSystem : MonoBehaviour {

    [SerializeField]
    GameObject layer1, layer2, layer3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.hasChanged) {
            layer1.transform.position = new Vector3(layer1.transform.position.x, layer1.transform.position.y, transform.position.z / 8);
            layer2.transform.position = new Vector3(layer2.transform.position.x, layer2.transform.position.y, transform.position.z / 5);
            layer3.transform.position = new Vector3(layer3.transform.position.x, layer3.transform.position.y, transform.position.z / 2);
            transform.hasChanged = false;
        }
	}
}
