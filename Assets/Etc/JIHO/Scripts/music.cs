
using UnityEngine;

public class music : MonoBehaviour
{
    private void Start()
    {
        Sound.instance.Play(Sound.instance.audioDictionary["Start"], true);
    }
}
