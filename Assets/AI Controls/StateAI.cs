using UnityEngine;

public class StateAI : MonoBehaviour
{
    [SerializeField] private Transform bossPos;
    private Movement mover;
    private int state;
    private int randomDir;
    private int jumpcd;
    private int runDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mover = GetComponent<Movement>();
        state = Random.Range(1, 4);
        randomDir = Random.Range(-1, 1);
        randomDir *= 2;
        randomDir++;
        Debug.Log("current state is: " + state);
        state = 4;
        runDir = 1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 1: //run
                mover.Move(randomDir);
                if (Random.Range(1, 101) == 100)
                    mover.Jump();
                break;

            case 2: //jump
                if (jumpcd < 1)
                {
                    mover.Jump();
                    jumpcd = 10;
                }
                else jumpcd -= 1;
                break;

            case 3: //flee
                if (bossPos.position.x < transform.position.x)
                    mover.Move(1);
                else mover.Move(-1);
                break;

            case 4: //juke
                if (Random.Range(1, 300) == 1)
                    runDir *= -1;
                if (transform.localPosition.x >= 7.5f)
                    runDir = -1;
                else if (transform.localPosition.x <= -7.5f)
                    runDir = 1;
                mover.Move(runDir);
                if (bossPos.localPosition.x > transform.localPosition.x && runDir == 1 && bossPos.localPosition.y < -3f)
                    mover.Jump();
                if (bossPos.localPosition.x < transform.localPosition.x && runDir == -1 && bossPos.localPosition.y < -3f)
                    mover.Jump();
                    break;
        }
    }

    public void GetNewState()
    {
        state = Random.Range(1, 4);
        randomDir = Random.Range(-1, 1);
        randomDir *= 2;
        randomDir++;
        Debug.Log("current state is: " + state);
    }
}
