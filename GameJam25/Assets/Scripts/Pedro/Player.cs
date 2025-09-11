using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] Vector2 direction;
    [SerializeField] Vector2 lastSavedPosition;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;


    public bool canMove;
    public static Player instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SavePosition()
    {
        PassInfo.instance.lastSavedPos = transform.position;
    }
    public void LoadPosition(Vector2 position)
    {
        transform.position = position;
    }
    void Update()
    {
        rb.linearVelocity = Vector2.zero;
        if (!canMove) return;

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
