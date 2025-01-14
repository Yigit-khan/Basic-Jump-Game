using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    public float jumpCooldown = 1f;
    public Rigidbody2D rb;

    public float elapsedTime = 0f;
    public List<float> jumpForceMultipliers = new List<float> { 1f, 1.5f, 2f, 2.2f };

    private float moveX;
    private bool canJump = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            StartCoroutine(Jump());
        }

        // Update the elapsed time
        elapsedTime += Time.deltaTime;

        // Adjust the jump force based on the elapsed time
        if (elapsedTime >= 180f) // 3 minutes
        {
            jumpForce = 10f * jumpForceMultipliers[3];
        }
        else if (elapsedTime >= 120f) // 2 minutes
        {
            jumpForce = 10f * jumpForceMultipliers[2];
        }
        else if (elapsedTime >= 60f) // 1 minute
        {
            jumpForce = 10f * jumpForceMultipliers[1];
        }
        else
        {
            jumpForce = 10f * jumpForceMultipliers[0];
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = moveX;
        rb.velocity = velocity;
    }

    private IEnumerator Jump()
    {
        canJump = false;
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }

    public void ApplyPowerUp(PowerUppSystem powerUp)
    {
        switch (powerUp.powerUpType)
        {
            case PowerUpType.JumpCooldownReset:
                StartCoroutine(ResetJumpCooldown());
                break;
            case PowerUpType.JumpForceMultiplier:
                StartCoroutine(ChangeJumpForce(powerUp.jumpForceMultiplier, powerUp.duration));
                break;
        }
    }

    private IEnumerator ResetJumpCooldown()
    {
        canJump = true;
        yield return null;
    }

    private IEnumerator ChangeJumpForce(float multiplier, float duration)
    {
        float originalJumpForce = jumpForce;
        jumpForce *= multiplier;
        Debug.Log("Jump force increased to: " + jumpForce);

        // Apply an immediate large upward force to simulate a rocket launch
        rb.AddForce(new Vector2(0, jumpForce * 10), ForceMode2D.Impulse);

        yield return new WaitForSeconds(duration);
        jumpForce = originalJumpForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<DeadLine>() != null)
        {
            Destroy(gameObject);
        }
    }
}