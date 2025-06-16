using UnityEngine;
using UnityEngine.SceneManagement;

public class PassPhase : MonoBehaviour
{
    public string lvlName;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(lvlName);
        }
    }
}
