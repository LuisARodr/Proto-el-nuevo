using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.MemorySystem;

public class LoadAndSave : MonoBehaviour {
    
	void Awake () {
		if (MemorySystem.loadGame)
        {
            GameData gd = MemorySystem.LoadData("save.data");
            MemorySystem.loadGame = false;
            Vector3 pos = GetComponent<Transform>().position;
            GetComponent<Transform>().position = new Vector3(pos.x,gd.PosY,gd.PosX);
            //print("load at: " + new Vector3(pos.x, gd.PosY, gd.PosX));
        }
	}
    /*
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            MemorySystem.Save(new GameData(GetComponent<Transform>().position.x,
                GetComponent<Transform>().position.y), "save.data");
            print("saved");
        }
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Save")
        {
            MemorySystem.Save(new GameData(GetComponent<Transform>().position.z,
               GetComponent<Transform>().position.y), "save.data");
            //print("saved at: " + GetComponent<Transform>().position.z + " -- " +
            //   GetComponent<Transform>().position.y);
        }
    }
}
