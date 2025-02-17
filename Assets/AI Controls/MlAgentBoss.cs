using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MlAgentBoss : Agent
{
    [SerializeField]private Transform player, player2, player3;
    [SerializeField] private GameObject playerObj, player2Obj, player3Obj;
    private List<GameObject> players = new List<GameObject>();
    private Movement mover;
    private Attack attack;
    private int lastAction;
    private int hitCount;

    // Initialiser agenten
    public override void Initialize()
    {

        players.Add(playerObj);
        players.Add(player2Obj);
        players.Add(player3Obj);
        //player = GameObject.FindWithTag("Player").transform; // Find spilleren via dens tag
        mover = GetComponent<Movement>(); // Find movement script
        attack = GetComponent<Attack>();
        Time.timeScale = 1;
    }

    public override void OnEpisodeBegin()
    {
        RespawnPlayer();
        hitCount = 0;
        transform.localPosition = new Vector3(Random.Range(-8f, 8f), -3, 0);
        player.localPosition = new Vector3(Random.Range(-8f, 8f), Random.Range(-3f, 1f), 0);
        player2.localPosition = new Vector3(Random.Range(-8f, 8f), Random.Range(-3f, 1f), 0);
        player3.localPosition = new Vector3(Random.Range(-8f, 8f), Random.Range(-3f, 1f), 0);
    }


    // OnActionReceived metoden bruges under træning
    public override void OnActionReceived(ActionBuffers actions)
    {
       // Debug.Log(actions.DiscreteActions[0]);
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
                AddReward(-5f);
                break;
        }
        lastAction = actions.DiscreteActions[0];

    }

    // Metode til at samle observationer (her bruger vi kun spillernes position)
    public override void CollectObservations(VectorSensor sensor)
    {
        foreach (GameObject p in players)
        {
            if (!p.activeSelf)
            {
                sensor.AddObservation(p.transform.position); // Tilføj spilleren position til observationen 
            }
        }
        sensor.AddObservation(transform.position); // Tilføj bossens position til observationen
    }

    public void OnHit()
    {
        Debug.Log("OW!");
        AddReward(200f);
        hitCount++;

        if (hitCount >= 3)
        {
            Debug.Log("all 3 hit" + hitCount);
            EndEpisode();
        }
            
    }

    public void RespawnPlayer()
    {
        playerObj.GetComponent<Helth>().HP = 1;
        player2Obj.GetComponent<Helth>().HP = 1;
        player3Obj.GetComponent<Helth>().HP = 1;
        playerObj.SetActive(true);
        player2Obj.SetActive(true);
        player3Obj.SetActive(true);
    }

}