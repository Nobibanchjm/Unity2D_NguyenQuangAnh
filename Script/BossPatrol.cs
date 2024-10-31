using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] Transform leftPatrol;
    [SerializeField] Transform rightPatrol;

    // Boss
    [SerializeField] Transform Boss;

    // Move speed
    [SerializeField] float movespeed;
    Vector2 initScale;
    bool moveLeft; // Biến để theo dõi hướng di chuyển
    [SerializeField] Animator anmt;

    [SerializeField] float Thoigiannghi = 1f; // Thời gian nghỉ giữa các lần di chuyển

    void Awake()
    {
        if (Boss != null) // Kiểm tra null cho Boss
        {
            initScale = Boss.localScale;
            moveLeft = true; // Khởi tạo hướng di chuyển ban đầu
        }
    }

    void Update()
    {
        // Chỉ gọi Patrol nếu Boss vẫn còn tồn tại
        if (Boss != null)
        {
            Patrol(); // Gọi phương thức tuần tra trong Update
        }
    }

    void OnDisable()
    {
        // Chỉ thực hiện khi đối tượng vẫn còn tồn tại
        if (anmt != null)
        {
            anmt.SetBool("Moving", false); // Dừng chuyển động
        }
    }

    void Patrol()
    {
        // Kiểm tra hướng di chuyển
        if (moveLeft)
        {
            // Nếu boss đã đến leftPatrol, thay đổi hướng
            if (Boss.position.x <= leftPatrol.position.x)
            {
                StartCoroutine(RestAndChangeDirection(false)); // Nghỉ trước khi đổi hướng sang phải
            }
            else
            {
                Move(-1); // Di chuyển sang trái
            }
        }
        else
        {
            // Nếu boss đã đến rightPatrol, thay đổi hướng
            if (Boss.position.x >= rightPatrol.position.x)
            {
                StartCoroutine(RestAndChangeDirection(true)); // Nghỉ trước khi đổi hướng sang trái
            }
            else
            {
                Move(1); // Di chuyển sang phải
            }
        }
    }

    // Di chuyển boss
    void Move(int direction)
    {
        if (anmt != null) // Kiểm tra null cho Animator
        {
            anmt.SetBool("Moving", true); // Đặt hoạt ảnh di chuyển
        }

        if (Boss != null) // Kiểm tra null cho Boss
        {
            // Đổi hướng của boss
            Boss.localScale = new Vector2(Mathf.Abs(initScale.x) * direction, initScale.y);
            // Di chuyển boss theo hướng
            Boss.position = new Vector2(Boss.position.x + Time.deltaTime * movespeed * direction, Boss.position.y);
        }
    }

    // Coroutine để nghỉ và thay đổi hướng
    IEnumerator RestAndChangeDirection(bool moveLeftDirection)
    {
        if (anmt != null) // Kiểm tra null cho Animator
        {
            anmt.SetBool("Moving", false); // Dừng hoạt ảnh di chuyển
        }

        yield return new WaitForSeconds(Thoigiannghi); // Nghỉ trong khoảng thời gian nhất định
        moveLeft = moveLeftDirection; // Thay đổi hướng di chuyển
    }
}
