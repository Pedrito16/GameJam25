using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] Vector2 direction;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRenderer;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector2(horizontal, vertical).normalized;

        rb.linearVelocity = direction * speed;
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Horizontal", Mathf.Abs(horizontal));

        if (horizontal >= 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
