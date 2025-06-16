using UnityEngine;

public class Fruit : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider2D;

    public GameObject collected;
    public int Score;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spriteRenderer.enabled = false;
            circleCollider2D.enabled = false;
            collected.SetActive(true);

            Punctuation.instance.totalScore += Score;
            GameController.instance.UpdateScoreText();

            Destroy(gameObject, 0.3f);
        }
    }
}
