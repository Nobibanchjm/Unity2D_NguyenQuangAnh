using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] float attackCooldown;
    [SerializeField] int damage;
    [SerializeField] LayerMask playerLayer;
    float colldownTimer = Mathf.Infinity;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] float range;
    Animator anmr;
    GameSession gameSession;
    BossPatrol BossPatrol;

    [SerializeField] int bulletDamage = 10; // Sát thương từ viên đạn là 5

    // Tạo biến tĩnh để lưu trữ máu boss
    private static int currentHealth = 100; // Sức khỏe hiện tại của boss

    void Awake()
    {
        anmr = GetComponent<Animator>();
        gameSession = FindObjectOfType<GameSession>();
        BossPatrol = GetComponentInParent<BossPatrol>();

        // Khởi tạo sức khỏe của boss
        if (currentHealth == 100) // Chỉ khởi tạo một lần
        {
            currentHealth = GetMaxHealth(); // Lấy sức khỏe tối đa
        }
    }

    public void Update()
    {
        colldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (colldownTimer >= attackCooldown)
            {
                colldownTimer = 0;
                anmr.SetTrigger("Attack");
            }
        }

        if (BossPatrol != null)
        {
            BossPatrol.enabled = !PlayerInSight();
        }

        // Kiểm tra nếu máu của boss giảm xuống 0
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }

    // Gây sát thương cho người chơi
    void DamagePlayer()
    {
        // Kiểm tra nếu va chạm với người chơi
        if (PlayerInSight())
        {
            FindObjectOfType<PlayerMovement>().DieByBoos();
        }
    }

    // Phương thức xử lý khi va chạm với đạn
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) // Kiểm tra nếu va chạm với đối tượng có tag "Bullet"
        {
            TakeDamage(bulletDamage); // Trừ 5 máu
            anmr.SetTrigger("Hit"); // Chuyển sang hoạt ảnh "hit"
        }
    }

    // Phương thức trừ máu
    void TakeDamage(int damage)
    {
        currentHealth -= damage; // Trừ máu của boss
        Debug.Log("Boss bị trúng đạn! Máu còn lại: " + currentHealth);
    }

    // Phương thức xử lý khi boss chết
    void Die()
    {
        if (anmr != null) // Kiểm tra xem anmr có còn tồn tại không
        {
            anmr.SetTrigger("Die"); // Chuyển sang hoạt ảnh "chết"
            
        }
        Destroy(gameObject);
    }

    //Gía trị cho thanh máu 

    public int GetHealth()
    {
        return currentHealth; //Trả về sức khỏe hiện tại
    }

    public int GetMaxHealth()
    {
        return 100; // Trả về giá trị máu tối đa
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
