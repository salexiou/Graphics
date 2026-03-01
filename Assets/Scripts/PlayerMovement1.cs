using System.Collections;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 1.5f;
    private float jumpHeight = 0.7f;
    private float gravityValue = -9.81f;
    private bool isAttacking = false;
    private bool isJumping = false;
    public bool isAxePickedUp = false;
    public float attackRange = 2f; 
    public int attackDamage = 3; 
    private bool isWalking = false;
    private bool isIdle = false;

    public AudioSource attackSound;

    private bool canMove = true;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            return; 
        }

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float moveForward = Input.GetAxis("Vertical") * playerSpeed;
        float moveSide = Input.GetAxis("Horizontal") * playerSpeed;

        Vector3 move = new Vector3(moveSide, 0, moveForward);

        if (moveForward > 0)
        {
            move = transform.TransformDirection(move);
        }
        else
        {
            move = new Vector3(moveSide, 0, moveForward * 3);
            move = transform.TransformDirection(move);
        }

        if (move != Vector3.zero && !isAttacking && !isJumping)
        {
            controller.Move(move * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(move);

            float rotationSpeed = 1f; 
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (!AnimatorIsPlaying("Walk"))
            {
                isWalking = true;
                animator.SetTrigger("WalkTrigger");
                StartCoroutine(ResetWalkState(animator.GetCurrentAnimatorStateInfo(0).length));
            }
        }
        else if (move == Vector3.zero && !isAttacking && !isJumping)
        {
            if (!AnimatorIsPlaying("Idle"))
            {
                isIdle = true;
                animator.SetTrigger("IdleTrigger");
                animator.Play("Idle");
                StartCoroutine(ResetIdleState(animator.GetCurrentAnimatorStateInfo(0).length));
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer && !isAttacking && !isJumping)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
            if (!AnimatorIsPlaying("Jump"))
            {
                isJumping = true;
                animator.SetTrigger("JumpTrigger");
                StartCoroutine(ResetJumpState(animator.GetCurrentAnimatorStateInfo(0).length));
            }
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.G) && !isAttacking && !isJumping && isAxePickedUp)
        {
            isAttacking = true;
            animator.SetTrigger("AttackTrigger");
            StartCoroutine(ResetAttackState(animator.GetCurrentAnimatorStateInfo(0).length));
            attackSound.Play();
            Attack();
        }
    }

    void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Zombie"))
            {
                ZombieHealthSimple zombieHealth = collider.GetComponent<ZombieHealthSimple>();
                if (zombieHealth != null)
                {
                    int currentHealth = zombieHealth.health;
                    int damageDealt = Mathf.Min(currentHealth, attackDamage); 
                    currentHealth -= damageDealt;
                    zombieHealth.health = currentHealth;

                    if (currentHealth <= 0)
                    {
                        Die(collider.gameObject);
                    }

                    break;
                }
            }
        }
    }

    public void IncreaseAttackDamage(int amount)
    {
        attackDamage += amount;
    }

    void Die(GameObject zombieObject)
    {
        zombieObject.SetActive(false);
    }

    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    bool AnimatorIsPlaying(string stateName)
    {
        return AnimatorIsPlaying() && animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    IEnumerator ResetAttackState(float length)
    {
        yield return new WaitForSeconds(length);
        isAttacking = false;
        animator.ResetTrigger("AttackTrigger");
    }

    IEnumerator ResetIdleState(float length)
    {
        yield return new WaitForSeconds(length);
        isIdle = false;
        animator.ResetTrigger("IdleTrigger");
    }

    IEnumerator ResetJumpState(float length)
    {
        yield return new WaitForSeconds(length);
        isJumping = false;
        animator.ResetTrigger("JumpTrigger");
    }

    IEnumerator ResetWalkState(float length)
    {
        yield return new WaitForSeconds(length);
        isWalking = false;
        animator.ResetTrigger("WalkTrigger");
    }

    public void FreezeMovement()
    {
        canMove = false;
    }

    public void ResumeMovement()
    {
        canMove = true;
    }
}
