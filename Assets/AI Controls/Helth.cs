using UnityEngine;
using UnityEngine.SceneManagement;

public class Helth : MonoBehaviour
{
    [SerializeField] public int HP;
    [SerializeField] private MlAgentBoss reinforcementLearningArtificialIntelligence;
    [SerializeField] private GameObject goalBox;

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
            HP--;
            if (HP <= 0) Die();
        }
    }

    private void Die()
    {

    }
}
