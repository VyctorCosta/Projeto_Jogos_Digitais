using UnityEngine;

public class FallingPlataform : MonoBehaviour
{
    public float fallingTime;

    private TargetJoint2D target;
    private BoxCollider2D boxCollider2D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Layer 6: Ground
        {
            Invoke(nameof(Falling), fallingTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.layer == 7) // Layer 7: GameOver
        {
            Destroy(gameObject);
        }
    }

    void Falling()
    {
        target.enabled = false;
        boxCollider2D.isTrigger = true;
    }
}
