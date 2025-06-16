using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpForce;

    private Animator animator;

  void Start()
  {
        animator = GetComponent<Animator>();
  }

  void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            animator.SetTrigger("jump");
        }
    }
}
