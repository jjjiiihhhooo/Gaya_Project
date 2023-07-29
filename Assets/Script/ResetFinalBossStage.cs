using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResetFinalBossStage : MonoBehaviour
{
    public GameObject BossStart;
    public GameObject Bullets;

    private void Start()
    {
        GameObject.Find("GameManager").GetComponent<GameOver>().ResetStage.AddListener(ResetStage);
        //RemoveListenerµµ °¡´É
    }

    public void ResetStage()
    {
        BossStart.GetComponent<StartBossStage>().ResetStartBossStage();
        
        for(int i = 0; i < Bullets.transform.childCount; i++)
        {
            Destroy(Bullets.transform.GetChild(i).gameObject);
        }
    }
}
