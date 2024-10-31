using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move Infor")]
    public float movespeed;
    public float JumpPorce;

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] LayerMask whatIsGround;


    #region Components
    public Animator amr { get; private set; }
    public Rigidbody2D rgb { get; private set; }

    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    #endregion


    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        // Khởi tạo các trạng thái với tham số cần thiết
        idleState = new PlayerIdleState(stateMachine, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, this, "Move");
        jumpState = new PlayerJumpState(stateMachine, this, "Jump");
        airState = new PlayerAirState(stateMachine, this, "Jump");
    }

    private void Start()
    {
        amr = GetComponentInChildren<Animator>();
        rgb = GetComponent<Rigidbody2D>();

        // Đặt trạng thái ban đầu cho stateMachine
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();

    }

    public void SetVolocity(float _xvelocity, float _yvelocity)
    {
        rgb.velocity = new Vector2(_xvelocity, _yvelocity);
    }

    public bool IsGroundDectected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround); 

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

}