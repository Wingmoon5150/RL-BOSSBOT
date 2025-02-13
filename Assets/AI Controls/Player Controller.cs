using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool hasJumped;
    private Movement mover;
    private Attack attacker;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mover = GetComponent<Movement>();
        attacker = GetComponent<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        mover.Move(horizontalInput);
        if (Input.GetKey(KeyCode.W))
        {
            if(!hasJumped)
            {
                hasJumped = true;
                mover.Jump();
            }
        }
        else hasJumped = false;
        if (Input.GetKey(KeyCode.J)) attacker.Strike();
    }

}
