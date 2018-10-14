using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    [SerializeField]
    float posX;
    [SerializeField]
    float posY;

    public float PosX
    {
        get
        {
            return posX;
        }

        set
        {
            posX = value;
        }
    }

    public float PosY
    {
        get
        {
            return posY;
        }

        set
        {
            posY = value;
        }
    }

    public GameData(float posX, float posY)
    {
        this.posX = posX;
        this.posY = posY;
    }

    public GameData() { }
   
    public Vector2 PosVector
    {
        get
        {
            return new Vector2(posX, posY);
        }
    }
}
