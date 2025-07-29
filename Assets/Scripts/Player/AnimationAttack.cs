using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAttack : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void AttackMelee()
    {
        anim.SetTrigger("Attack");
    }

    public void MagicAttack()
    {
        anim.SetTrigger("Power");
    }


}
