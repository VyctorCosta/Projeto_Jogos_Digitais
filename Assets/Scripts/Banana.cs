using UnityEngine;

public class Banana : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    public GameObject collected;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spriteRenderer.enabled = false;
            boxCollider2D.enabled = false;
            collected.SetActive(true);

            Punctuation.instance.IncreaseSpecialBanana();

            Destroy(gameObject, 0.8f);
        }
    }
}
