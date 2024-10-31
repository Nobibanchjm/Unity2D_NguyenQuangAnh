using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Sensitivity Infor")]
    [SerializeField] float tocdochay = 10f;
    [SerializeField] float nhay = 7f;
    private float facingDir = 1;
    private bool facingRight =true;

    [Header("Rush info")]
    [SerializeField] float DashSpeed;
    [SerializeField] float DashDuration; //Thời lượng Drush
    float DashTime;     //Thời gian Drush
    [SerializeField] float DashCoolDonwn;
    float DashCoolDownTimer; //Thời gian hồi Drush


    Rigidbody2D rgb;
    Animator amr;

    float xInput;
    float yInput;


    [Header("Attacking Infor")]
    [SerializeField] float ComboTime;
    [SerializeField] float ComboTimeWinDow;
    private bool isAttacking;
    private int comboCounter;

    [Header("Collision Infor")]
    [SerializeField] float groundCheckDistance; //Kiểm tra mặt đất
    [SerializeField] LayerMask whatIsGround;
    bool isGroud;





    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        amr = GetComponent<Animator>();
    }


    void Update()
    {
        Move();
        CheckInput();
        AnimatorController();
        FlipController();
        CollisionCheck(); //Kiểm tra xem nhân vật có trên mặt đất hay không?
        DashTime -= Time.deltaTime;
        DashCoolDownTimer -= Time.deltaTime;
        ComboTimeWinDow -= Time.deltaTime;

    }


    private void CollisionCheck()
    {
        isGroud = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }


    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();
        }
    }


    private void StartAttackEvent()
    {
        if (!isGroud) return;

        if (ComboTimeWinDow < 0)
        {
            comboCounter = 0;
        }
        isAttacking = true;
        ComboTimeWinDow = ComboTime;
    }


    private void DashAbility()
    {
        if (DashCoolDownTimer < 0 && !isAttacking)
        {
            DashCoolDownTimer = DashCoolDonwn;
            DashTime = DashDuration;
        }

    }

    private void Move()
    {
        if (isAttacking) rgb.velocity = new Vector2(0f ,0f);
        else if (DashTime > 0)
        {
            rgb.velocity = new Vector2(facingDir * DashSpeed, 0); //cho y bằng 0 để khi dash trên không không bị thay đổi y
        }
        else
        {
            rgb.velocity = new Vector2(xInput * tocdochay, rgb.velocity.y);
        }

    }


    private void Jump()
    {
        if (isGroud)
        {
            rgb.velocity = new Vector2(rgb.velocity.x, nhay);

        }

    }


    void AnimatorController()
    {
        bool isRun = rgb.velocity.x != 0; //Khi mà vận tốc khác không là true
        amr.SetFloat("yVelocity", rgb.velocity.y);
        amr.SetBool("IsRun", isRun);
        amr.SetBool("IsGround", isGroud);
        amr.SetBool("IsDrushing", DashTime > 0);
        amr.SetBool("IsAttacking", isAttacking);
        amr.SetInteger("comboCounter", comboCounter);
    }


    void FlipSprite()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }


    private void FlipController()
    {
        if (rgb.velocity.x > 0 && !facingRight)
        {
            FlipSprite();
        }
        else if (rgb.velocity.x < 0 && facingRight)
        {
            FlipSprite();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
        //Tạo ra một tia từ vị chí của nhân vật
    }

    public void AttackOver()
    {
        isAttacking = false;
        comboCounter++;
        if (comboCounter > 2)
        {
            comboCounter = 0;
        }
    }    
}
   