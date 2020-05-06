using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{

    private Animator anim;

    private string WALK_PARAMETER = "isWalking";
    private string RUN_PARAMETER = "isRunning";
    private string ATTACK_PARAMETER = "isAttacking";

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Walking(bool walk)
    {
        anim.SetBool(WALK_PARAMETER, walk);
        
    }
    public void Running(bool run)
    {
        anim.SetBool(RUN_PARAMETER, run);
        
    }

    public void Attacking(bool attack)
    {
        anim.SetBool(ATTACK_PARAMETER, attack);
    }

    public void Idling()
    {
        anim.Play("IdleNormal");
    }
}
