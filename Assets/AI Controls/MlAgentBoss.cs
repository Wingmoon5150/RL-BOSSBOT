using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MlAgentBoss : Agent
{
    [SerializeField]private Transform player;
    private Movement mover;
    private Attack attack;
    private int lastAction;

    // Initialiser agenten
    public override void Initialize()
    {
        //player = GameObject.FindWithTag("Player").transform; // Find spilleren via dens tag
        mover = GetComponent<Movement>(); // Find movement script
        attack = GetComponent<Attack>();
        Time.timeScale = 1;
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-8f, 8f), -3, 0);
        player.localPosition = new Vector3(Random.Range(-8f, 8f), Random.Range(-3f, 1f), 0);
    }


    // OnActionReceived metoden bruges under træning
    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log(actions.DiscreteActions[0]);
        AddReward(-0.05f);
        switch (actions.DiscreteActions[0])
        {
            case 0:
                mover.Move(-1f);
                break;

            case 1:
                mover.Move(1f);
                break;

            case 2:
                if (lastAction != 2)
                mover.Jump();
                AddReward(-3f);
                break;

            case 3:
                attack.Strike();
                break;
        }
        lastAction = actions.DiscreteActions[0];

    }

    // Metode til at samle observationer (her bruger vi kun spillernes position)
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(player.position); // Tilføj spilleren position til observationen
        sensor.AddObservation(transform.position); // Tilføj bossens position til observationen
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Debug.Log("OW!");
        if (collision.transform.tag == "Player")
        {
            SetReward(200f);
            EndEpisode();
        }
    }

}