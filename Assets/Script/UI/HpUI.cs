using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    [SerializeField] private Sprite fullHP;
    [SerializeField] private Sprite halfHP;
    [SerializeField] private Sprite EmtyHP;

    private GameObject[] Heart;

    public void UpdateUI(int currentHp)
    {
        Heart = new GameObject[3];
        Heart[0] = this.gameObject.transform.GetChild(0).gameObject; // 哭率
        Heart[1] = this.gameObject.transform.GetChild(1).gameObject; // 啊款单
        Heart[2] = this.gameObject.transform.GetChild(2).gameObject; // 坷弗率

        switch (currentHp)
        {
            case 6:
                Heart[0].GetComponent<Image>().sprite = fullHP;
                Heart[1].GetComponent<Image>().sprite = fullHP;
                Heart[2].GetComponent<Image>().sprite = fullHP;
                break;
            case 5:
                Heart[0].GetComponent<Image>().sprite = fullHP;
                Heart[1].GetComponent<Image>().sprite = fullHP;
                Heart[2].GetComponent<Image>().sprite = halfHP;
                break;
            case 4:
                Heart[0].GetComponent<Image>().sprite = fullHP;
                Heart[1].GetComponent<Image>().sprite  = fullHP;
                Heart[2].GetComponent<Image>().sprite  = EmtyHP;
                break;
            case 3:
                Heart[0].GetComponent<Image>().sprite  = fullHP;
                Heart[1].GetComponent<Image>().sprite  = halfHP;
                Heart[2].GetComponent<Image>().sprite  = EmtyHP;
                break;
            case 2: 
                Heart[0].GetComponent<Image>().sprite  = fullHP;
                Heart[1].GetComponent<Image>().sprite  = EmtyHP;
                Heart[2].GetComponent<Image>().sprite  = EmtyHP;
                break;
            case 1:
                Heart[0].GetComponent<Image>().sprite  = halfHP;
                Heart[1].GetComponent<Image>().sprite  = EmtyHP;
                Heart[2].GetComponent<Image>().sprite  = EmtyHP;
                break;
            case 0:
                Heart[0].GetComponent<Image>().sprite  = EmtyHP;
                Heart[1].GetComponent<Image>().sprite  = EmtyHP;
                Heart[2].GetComponent<Image>().sprite = EmtyHP;
                break;
        }
    }
}
