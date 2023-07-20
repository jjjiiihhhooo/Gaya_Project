using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    static private PlayerStatus instance;

    public static PlayerStatus Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public int HP = 6;
    public int Speed = 6;
    public int AtkDamage = 1;
    public Vector3 SavePoint = new Vector3(0,0,0);
}
