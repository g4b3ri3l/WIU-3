using UnityEngine;

public class SlowDownEffector : MonoBehaviour
{
    public float slowDownFactor = 0.5f; 
    private float originalSpeed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MovementController movementController = other.GetComponent<MovementController>();
            if (movementController != null)
            {
                originalSpeed = movementController.speed; 
                movementController.speed *= slowDownFactor;
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
                movementController.speed = originalSpeed; 
            }
        }
    }
}
