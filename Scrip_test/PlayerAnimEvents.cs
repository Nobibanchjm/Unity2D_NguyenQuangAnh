using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimrEvens : MonoBehaviour
{
    PlayerController player;

    void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }


    public void AnimationTrigger()
    {
        player.AttackOver();
    }    
}
