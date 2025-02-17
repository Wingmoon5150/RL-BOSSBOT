using UnityEngine;

public class Helth : MonoBehaviour
{
    [SerializeField] private int HP;
    [SerializeField] private MlAgentBoss reinforcementLearningArtificialIntelligence;

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("owie " + collision);
    //    if(collision.transform.tag == "Attack")
    //    {
    //        reinforcementLearningArtificialIntelligence.OnHit();
    //        HP--;
    //        if (HP <= 0) Die();
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("owie " + collision);
        if (collision.transform.tag == "Attack")
        {
            reinforcementLearningArtificialIntelligence.OnHit();
            HP--;
            if (HP <= 0) Die();
        }
    }

    private void Die()
    {
        //hi this does nothing right now :3
    }
}
