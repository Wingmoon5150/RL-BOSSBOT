using UnityEngine;
using UnityEngine.InputSystem;
using Input = UnityEngine.Input;


public class Attack : MonoBehaviour
{
    public float attackCooldown = 0;

    //I have no fucking clue what I'm doing
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    public void Strike()
    {
        if (attackCooldown < 1)
        {
            anim.SetBool("attacking", true);
            attackCooldown = 5;
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
    }

}
