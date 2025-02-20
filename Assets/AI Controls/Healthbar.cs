using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Helth health;
    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(health.HP/10.5f, 0.6f, 1f);
    }
}
