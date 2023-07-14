using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    private void Start()
    {
        Sound.instance.Play(Sound.instance.audioDictionary["Start"], true);
    }
}
