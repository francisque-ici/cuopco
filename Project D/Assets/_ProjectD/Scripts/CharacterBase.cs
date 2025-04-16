using System.Collections;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public float WalkSpeed;
    public Vector3 MoveDirection;
    public bool isStunned = false;
    public float RotationSpeed;
    private Rigidbody rb;

    [SerializeField] private Animator _animator;

    // Dash settings
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1.5f;
    private float lastDashTime = -Mathf.Infinity;
    private bool isDashing = false;

    void Start()
    {
        WalkSpeed = 7f;
        RotationSpeed = 2f;
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError($"{name} is missing a Rigidbody component!");
        }
    }

    public void Move()
    {
        if (isStunned || isDashing) return;

        rb.velocity = MoveDirection * WalkSpeed;
    }

    public void Rotate()
    {
        if (MoveDirection.magnitude > 0.1f)
        {
            Quaternion moveRotation = Quaternion.LookRotation(MoveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, moveRotation, RotationSpeed);
        }
    }

    public void Animate()
    {
        if (MoveDirection.magnitude == 0) _animator.SetBool("isMove", false);
        else _animator.SetBool("isMove", true);
    }

    public void Dash()
    {
        if (isDashing || isStunned || Time.time < lastDashTime + dashCooldown || MoveDirection.magnitude == 0)
            return;

        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        AudioManager.Instance.Dash.Play();
        isDashing = true;
        lastDashTime = Time.time;

        rb.velocity = MoveDirection.normalized * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
    }

    public void Stun(float duration)
    {
        Debug.Log("STUN");
        if (!isStunned)
        {
            StartCoroutine(StunCoroutine(duration));
        }
    }

    private IEnumerator StunCoroutine(float duration)
    {
        isStunned = true;
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("banana"))
        {
            AudioManager.Instance.Fall.Play();
            Destroy(collision.gameObject);
            _animator.Play("Fall");
            Stun(1.25f);
        }      
    }
}
