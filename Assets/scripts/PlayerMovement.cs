using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Velocidade que o jogador vai andar!")]
    public float speed;
    public float jumpForce;
    public float knockbackForce = 10f;
    public float invincibilityTime = 0.5f;

    private float LastHorizontal = 1f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private bool canTakeDamage = true;
    private bool isGrounded;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        if (move != 0)
        {
            LastHorizontal = move;
        }

        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }

        if (collision.collider.CompareTag("Spike") && canTakeDamage)
        {
            Vector2 knockDir = new Vector2(-Mathf.Sign(LastHorizontal), 0);
            StartCoroutine(ReactToSpike(knockDir));
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private IEnumerator ReactToSpike(Vector2 knockDir)
    {
        canTakeDamage = false;
        rb.linearVelocity = knockDir * knockbackForce;

        // Piscar para indicar dano
        float elapsed = 0f;
        while (elapsed < invincibilityTime)
        {
            spriteRenderer.color = new Color(1, 0, 0); // vermelho
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.2f;
        }

        canTakeDamage = true;
    }
}
