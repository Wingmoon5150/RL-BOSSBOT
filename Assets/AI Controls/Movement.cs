using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject atBox;

    private Animator anim;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Collider2D Collider;
    private bool doubleJump;
    private bool grounded = true;
    public int faceDir;

    private void IsGrounded()
    {
        RaycastHit2D ray = Physics2D.BoxCast(Collider.bounds.center, Collider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        grounded = ray.collider != null;
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
    public void Move(float direction)
    {
        float horizontalInput = direction;
        anim.SetBool("run", horizontalInput != 0);

        if (horizontalInput != 0)
        {
            sr.flipX = horizontalInput > 0;

            if (horizontalInput < 0) faceDir = -1;
            else if (horizontalInput > 0) faceDir = 1;

            // Flip attack boxens scale
            if (atBox != null)
            {
                //Flip Scale
                Vector3 scale = atBox.transform.localScale;
                scale.x = Mathf.Abs(scale.x) * -faceDir; // faceDir styrer flip
                atBox.transform.localScale = scale;

                //Flip position
                Vector3 pos = atBox.transform.localPosition;
                pos.x = Mathf.Abs(pos.x) * faceDir;
                atBox.transform.localPosition = pos;
            }
        }

        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocityY);
    }

    public void Jump()
    {
            IsGrounded();
            if (grounded)
            {
                doubleJump = true;
                rb.linearVelocityY = 15;
                grounded = false;
            }
            else if (doubleJump)
            {
                doubleJump = false;
                rb.linearVelocityY = 15;
            }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayer) grounded = true;
    }
}
