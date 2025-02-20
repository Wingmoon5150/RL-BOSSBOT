using UnityEngine;
using UnityEngine.SceneManagement;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Helth health;
    [SerializeField] private string scene;
    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(health.HP/30.5f, 0.6f, 1f);
        if (health.HP <= 0)
            SceneManager.LoadScene(scene);
    }
}
