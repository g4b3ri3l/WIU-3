using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 0.5f;

    public CircleCollider2D circleCollider;

    public Transform interactionPoint;

    public Transform player;

    bool hasInteracted = false;

    private void Start()
    {
        circleCollider.radius = radius;

        if (interactionPoint == null)
        {
            interactionPoint = transform;
        }
    }

    public virtual void Interact()
    {
        //Meant to be Overwritten
        Debug.Log("Interacting with " + transform.name);
    }

    private void Update()
    {
        if (!hasInteracted)
        {
            Vector3 distance = interactionPoint.position - player.position;

            if (Vector3.Magnitude(distance) <= radius)
            {
                Interact();

                hasInteracted = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        if (interactionPoint == null)
        {
            interactionPoint = transform;
        }
        Gizmos.DrawWireSphere(interactionPoint.position, radius);
    }



}
