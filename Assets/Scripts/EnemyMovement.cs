using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Colisiones
{
    public float speed = 2f;
    private Vector3 direction;
    private Rigidbody2D rb;
    private Animator animator;
    public float pushForce = 5f;
    public float pushDistance = 0.5f;
    public float changeDirectionTime = 2f;
    private float changeDirectionTimer;
    public GameObject gameOverScreen;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        ChooseRandomDirection();
        changeDirectionTimer = changeDirectionTime;
    }

    protected override void Update()
    {
        base.Update();
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0)
        {
            ChooseRandomDirection();
            changeDirectionTimer = changeDirectionTime;
        }
        MoveEnemy();
        UpdateAnimation();
    }

    void MoveEnemy()
    {
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
        newPosition.z = 0;
        rb.MovePosition(newPosition);
    }

    void ChooseRandomDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        direction = new Vector3(randomX, randomY).normalized;
    }

    void UpdateAnimation()
    {
        if (direction != Vector3.zero)
        {
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);
            animator.SetBool("Moving", true);
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-0.75f, 0.75f, 1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(0.75f, 0.75f, 1);
            }
        }
        else
        {
            animator.SetBool("Moving", false);
            if (animator.GetFloat("moveX") > 0)
            {
                transform.localScale = new Vector3(-0.75f, 0.75f, 1);
            }
            else if (animator.GetFloat("moveX") < 0)
            {
                transform.localScale = new Vector3(0.75f, 0.75f, 1);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PushPlayer(collision.gameObject));
        }
    }

    private IEnumerator PushPlayer(GameObject player)
{
    var playerMovement = player.GetComponent<Movement>();
    Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
    Vector2 pushDirection = (player.transform.position - transform.position).normalized;
    float elapsedTime = 0f;
    float pushDuration = 0.2f;
    float invulnerableTime = 0.5f;

    // Si el jugador no tiene vida, lo hacemos invulnerable y desactivamos su movimiento.
    if (DataJuego.data.vida <= 0)
    {
        DataJuego.data.isInvulnerable = true;
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        gameOverScreen.SetActive(true); // Mostramos la pantalla de game over
        yield break; // Terminamos la corrutina ya que el jugador está sin vida
    }

    if (!DataJuego.data.isInvulnerable)
    {
        playerMovement.enabled = false;
        SoundEffectManager.Play("LinkDMG");
        DataJuego.data.vida -= 1;
        DataJuego.data.isInvulnerable = true;
        player.GetComponent<Animator>().SetTrigger("TakeDamage");
        player.GetComponent<Animator>().SetBool("isInvulnerable", true);

        if (DataJuego.data.vida <= 0)
        {
            DataJuego.data.isInvulnerable = true; // Aseguramos que se quede invulnerable
            gameOverScreen.SetActive(true); // Mostramos la pantalla de game over
            yield break;
        }
    }

    // Lógica de empuje
    while (elapsedTime < pushDuration)
    {
        playerRb.velocity = pushDirection * pushForce;
        elapsedTime += Time.deltaTime;

        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, pushDirection, pushDistance);
        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            pushDirection = -pushDirection;
            break;
        }

        yield return null;
    }

    playerRb.velocity = Vector2.zero;
    yield return new WaitForSeconds(invulnerableTime);

    // Solo reactivamos movimiento si el jugador tiene vida
    if (DataJuego.data.vida > 0)
    {
        DataJuego.data.isInvulnerable = false;
        playerMovement.enabled = true;
        player.GetComponent<Animator>().SetBool("isInvulnerable", false);
    }
}

}
