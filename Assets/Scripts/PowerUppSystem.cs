using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    JumpCooldownReset,
    JumpForceMultiplier
}

public class PowerUppSystem : MonoBehaviour
{
    public PowerUpType powerUpType;
    public float jumpForceMultiplier = 2f;
    public float duration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.ApplyPowerUp(this);
            Destroy(gameObject);
        }
    }
}
