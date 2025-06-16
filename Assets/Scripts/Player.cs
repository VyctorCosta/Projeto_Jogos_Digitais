using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;

    private Rigidbody2D rig;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isJumping;
    private bool isAbleToDoubleJump;
    private bool isRunning;
    private int timeInPoweredUp = 0;
    public bool isPoweredUp = false;

    private readonly Color[] powerUpColors = new Color[5];


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // powerUpColors[0] = new Color(177f, 189f, 207f, 1f);
        // powerUpColors[1] = new Color(144f, 169f, 207f, 1f);
        // powerUpColors[2] = new Color(132f, 167f, 217f);
        // powerUpColors[3] = new Color(113f, 150f, 205f);
        // powerUpColors[4] = new Color(95f, 130, 181);

        powerUpColors[0] = Color.lightCyan;
        powerUpColors[1] = Color.cyan;
        powerUpColors[2] = Color.darkCyan;
        powerUpColors[3] = Color.skyBlue;
        powerUpColors[4] = Color.softBlue;

        StartCoroutine(ChangeColorEverySecond());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Run();
        PowerUp();
    }

    void Move()
    {
        if (isRunning)
        {
            Speed += 2;
        }
        Vector3 movement = new(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += Speed * Time.deltaTime * movement;

        if (Input.GetAxis("Horizontal") > 0f)
        {
            animator.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (Input.GetAxis("Horizontal") < 0f)
        {
            animator.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        if (Input.GetAxis("Horizontal") == 0f)
        {
            animator.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                isAbleToDoubleJump = true;
                animator.SetBool("jump", true);
            }
            else
            {
                if (isAbleToDoubleJump)
                {
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    isAbleToDoubleJump = false;
                }
            }
        }
    }

    void Run()
    {
        if (Input.GetButtonDown("Run"))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6) // Layer 6: Ground
        {
            isJumping = false;
            animator.SetBool("jump", false);
        }
        if (collision.gameObject.CompareTag("Trampoline"))
        {
            animator.SetBool("jump", true);
            isAbleToDoubleJump = true;
        }

        // DEATHS
        if (collision.gameObject.CompareTag("Spike") && !isPoweredUp)
        {
            GameController.instance.GameOver();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Saw") && !isPoweredUp)
        {
            GameController.instance.GameOver();
            Destroy(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6) // Layer 6: Ground
        {
            isJumping = true;
        }
    }

    void PowerUp()
    {
        if (!isPoweredUp)
        {
            if (Input.GetButtonDown("Power") && Punctuation.instance.specialBanana > 0)
            {
                isPoweredUp = true;
                Punctuation.instance.DecreaseSpecialBanana();
            }
        }

    }

    IEnumerator ChangeColorEverySecond()
    {
        while (true)
        {
            if (isPoweredUp)
            {
                int index = Random.Range(0, powerUpColors.Length);
                spriteRenderer.color = powerUpColors[index];

                timeInPoweredUp++;

                if (timeInPoweredUp > 50)
                {
                    isPoweredUp = false;
                    timeInPoweredUp = 0;
                    spriteRenderer.color = Color.white;
                }
            }


            yield return new WaitForSeconds(0.2f);
        }
    }
}