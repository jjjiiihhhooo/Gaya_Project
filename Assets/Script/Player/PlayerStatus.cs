using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(GameOver))]
public class PlayerStatus : MonoBehaviour
{
    #region Singleton
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
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    [SerializeField] private GameOver gameOver;
    public int HP = 6;
    public int MaxHp = 6;
    public int Speed = 6;
    public int AtkDamage = 1;
    public int LifeCount = 5;
    public Vector3 SavePoint;

    private void Start()
    {
        SavePoint = new Vector3(0, 0, 0);
    }

    public void SetHp(int _Hp)
    {
        HP += _Hp; // 체력을 바꾼다

        if( HP <= 0 ) // 체력이 0이면
        {
            HP = MaxHp;
            LifeCount -= 1; // 전체목숨 하나 감소
            gameOver.Die();
            if ( LifeCount <= 0) // 진짜 게임 오버
            {
                Debug.Log("게임 끝! 처음부터!");
            }
        }
    }
}
