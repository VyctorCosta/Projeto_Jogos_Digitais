using UnityEngine;

public class Saw : MonoBehaviour
{

    public float speed;
    public float moveTime;

    private bool directionRight = false;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        if (directionRight)
        {
            transform.Translate(speed * Time.deltaTime * Vector2.right);
        }
        else
        {
            transform.Translate(speed * Time.deltaTime * Vector2.left);
        }

        timer += Time.deltaTime;
        if (timer >= moveTime)
        {
            directionRight = !directionRight;
            timer = 0f;

            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);

        }
    }
}
