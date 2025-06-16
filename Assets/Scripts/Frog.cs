using UnityEngine;

public class Frog : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private Animator animator;

    public float speed;

    public Transform rightCol;
    public Transform leftCol;

    public Transform headPoint;

    private bool colliding;

    public LayerMask layer;

    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.linearVelocity = new Vector2(speed, rigidbody2D.linearVelocityY);

        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);

        if (colliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }
    }


    bool playerDestroyed = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // float height = collision.contacts[0].point.y - headPoint.position.y;
            float height = collision.transform.position.y - headPoint.position.y;
            Player player = collision.gameObject.GetComponent<Player>();
            Debug.Log($"POWERUP: {player.isPoweredUp}");
            Debug.Log($"HEIGTH: {height}");
            Debug.Log($"PLAYERDESTROYED: {playerDestroyed}");

            if ((height > 0 && !playerDestroyed) || player.isPoweredUp)
            {
                speed = 0;
                animator.SetTrigger("die");
                boxCollider2D.enabled = false;
                circleCollider2D.enabled = false;
                rigidbody2D.bodyType = RigidbodyType2D.Kinematic;

                if (!player.isPoweredUp)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 4f, ForceMode2D.Impulse);
                }

                Destroy(gameObject, 0.7f);
            }
            else
            {
                playerDestroyed = true;
                GameController.instance.GameOver();
                Destroy(collision.gameObject);
            }
        }
    }
}
