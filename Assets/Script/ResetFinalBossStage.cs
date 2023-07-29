using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetFinalBossStage : MonoBehaviour
{
    public GameObject BossStart;
    private void Start()
    {
        GameObject.Find("GameManager").GetComponent<GameOver>().ResetStage.AddListener(ResetStage);
        //RemoveListenerµµ °¡´É
    }

    public void ResetStage()
    {
        BossStart.GetComponent<StartBossStage>().ResetStartBossStage();
    }
}
