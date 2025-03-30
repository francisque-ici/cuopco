using System.Collections;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public float WalkSpeed;
    public Vector3 MoveDirection;
    public bool isStunned = false;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError($"{name} is missing a Rigidbody component!"); // Báo lỗi nếu không tìm thấy rigidbody
        }
    }

    public void Move()
    {
        rb.velocity = MoveDirection * WalkSpeed;
    }

    public void Stun(float duration) // gọi hàm và chuyền vào duration (thời gian bị choáng)
    {
        if (!isStunned)
        {
            StartCoroutine(StunCoroutine(duration)); // Tạo ra 1 nhánh xử lí khácác
        }
    }

    private IEnumerator StunCoroutine(float duration) // 1 Nhánh xử lí khác
    {
        isStunned = true;
        rb.velocity = Vector3.zero; // Ngừng di chuyển ngay lập tức
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }
}
