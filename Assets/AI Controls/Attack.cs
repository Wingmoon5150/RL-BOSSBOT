using UnityEngine;
using UnityEngine.InputSystem;
using Input = UnityEngine.Input;


public class Attack : MonoBehaviour
{
    public float attackCooldown = 0;
    public Movement mover;

    [SerializeField] private GameObject atBox;

    //I have no fucking clue what I'm doing
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        mover = GetComponent<Movement>();
        atBox.SetActive(false);
    }

    public void Strike()
    {
        if (attackCooldown < 1)
        {
            anim.SetBool("attacking", true);
            attackCooldown = 5;
        }
    }

    public void Hit()
    {
        if (atBox != null)
        {
            atBox.SetActive(true); // Show attack box
        }
    }

    private void Update()
    {
        if (attackCooldown < 5) attackCooldown -= 1;
    }


    public void AttackFinish()
    {
        anim.SetBool("attacking", false);
        attackCooldown -= 1;

        if (atBox != null)
        {
            atBox.SetActive(false); // Hide attack box after attack
        }
    }
}
