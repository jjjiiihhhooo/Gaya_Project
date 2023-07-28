using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Effect : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        int rand = Random.Range(0,4);
        switch (rand)
        {
            case 0:
                animator.Play("Attack_Effect_01");
                break;
            case 1:
                animator.Play("Attack_Effect_02");
                break;
            case 2:
                animator.Play("Attack_Effect_03");
                break;
            case 3:
                animator.Play("Attack_Effect_04");
                break;
        }
    }
}
