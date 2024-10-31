using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    private string animBoolName;

    protected float xInput;

    protected Rigidbody2D rb;

    public PlayerState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName)
    {
        this.stateMachine = _stateMachine;
        this.player = _player;
        this.animBoolName = _animBoolName;
    }


    public virtual void Enter()
    {
        player.amr.SetBool(animBoolName, true);
        rb = player.rgb;
    }


    public virtual void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        player.amr.SetFloat("yVelocity", rb.velocity.y);

    }


    public virtual void Exit()
    {
        player.amr.SetBool(animBoolName, false);
    }
}
