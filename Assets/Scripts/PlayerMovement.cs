using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    private bool isAttacking;

    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        isAttacking = false;
    }

    void Update()
    {
        change = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.X) && !isAttacking)
        {
            animator.SetBool("Moving", false);
            StartCoroutine(Attack());
        }
        if (!isAttacking)
        {
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");

            // Evitar movimiento diagonal: priorizar un solo eje
            if (change.x != 0) change.y = 0;

            UpdateAnimation();
        }
    }

    void FixedUpdate()
    {
        if (!isAttacking)
        {
            MoveCharacter();
        }
    }

    void UpdateAnimation()
    {
        if (change != Vector3.zero)
        {
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("Moving", true);

            if (change.x > 0)
            {
                transform.localScale = new Vector3(-0.75f, 0.75f, 1);
            }
            else if (change.x < 0)
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

    void MoveCharacter()
    {
        if (change != Vector3.zero)
        {
            myRigidbody.MovePosition(
                transform.position + change * speed * Time.fixedDeltaTime
            );
        }
    }
    
    IEnumerator Attack()
    {
        isAttacking = true;
        SoundEffectManager.Play("LinkAtacking");
        animator.SetBool("Moving", false);
        animator.SetBool("Atacking", true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("Atacking", false);
        isAttacking = false;
    }
}
