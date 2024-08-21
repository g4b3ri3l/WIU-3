using UnityEngine;

public class SlowDownEffector : MonoBehaviour
{
    public float slowDownFactor = 0.5f; // Adjust this value to set how much the player should be slowed down
    private float originalSpeed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MovementController movementController = other.GetComponent<MovementController>();
            if (movementController != null)
            {
                originalSpeed = movementController.speed; // Store the player's original speed
                movementController.speed *= slowDownFactor; // Slow down the player
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MovementController movementController = other.GetComponent<MovementController>();
            if (movementController != null)
            {
                movementController.speed = originalSpeed; // Restore the player's original speed
            }
        }
    }
}
