
using UnityEngine;
using UnityEngine.UI;

public class Playtime : MonoBehaviour
{
    public Text ptText;
    private float Second;
    private int Minute, Hour, Day;

    void Update()
    {
        Second += Time.deltaTime;
        if (Second >= 60)
        {
            Second -= 60;
            Minute += 1;
        }
        if (Minute >= 60)
        {
            Minute -= 60;
            Hour += 1;
        }
        if (Hour >= 24)
        {
            Hour -= 24;
            Day += 1;
        }
        ptText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", Hour, Minute, (int)Second);
    }

}
