using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public float invincibilityDuration = 2.0f; // Duration of the immortal time
    public float flickerInterval = 0.1f; // How fast the sprite should flicker
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            PlayerHealth.instance.Damage();
            StartCoroutine(InmortalTime()); // Start immortal time
        }
    }

    // Coroutine for Immortal Time with Sprite Flicker
    private IEnumerator InmortalTime()
    {
        isInvincible = true; // Enable invincibility

        float elapsedTime = 0f;
        while (elapsedTime < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // Toggle visibility
            yield return new WaitForSeconds(flickerInterval); // Wait for the flicker interval
            elapsedTime += flickerInterval;
        }

        spriteRenderer.enabled = true; // Make sure sprite is visible after invincibility
        isInvincible = false; // Disable invincibility
    }
}
