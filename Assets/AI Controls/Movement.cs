using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    private Animator anim;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Collider2D Collider;
    private bool doubleJump;
    private bool hasJumped;
    private bool grounded = true;

    private bool IsGrounded()
    {
        RaycastHit2D ray = Physics2D.BoxCast(Collider.bounds.center, Collider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return ray.collider != null;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        anim.SetBool("run", horizontalInput != 0);
        if (horizontalInput != 0)
        {
            sr.flipX = horizontalInput > 0;
        }
        //if (horizontalInput <-0.1f && horizontalInput*3 < transform.localScale.x || horizontalInput >0.1f && horizontalInput * 3 > transform.localScale.x)
        //    transform.localScale = new Vector2(horizontalInput*3, 3);
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocityY);
        if (Input.GetKey(KeyCode.W) && !hasJumped)
        {

            if (IsGrounded())
            {
                doubleJump = true;
                Jump();
            }
            else if (doubleJump)
            {
                doubleJump = false;
                Jump();
            }
            hasJumped = true;
        }
        else if (!Input.GetKey(KeyCode.W)) hasJumped = false;
    }

    private void Jump()
    {
        rb.linearVelocityY = 15;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayer) grounded = true;
    }
}
