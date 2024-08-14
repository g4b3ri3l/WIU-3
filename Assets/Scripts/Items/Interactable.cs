using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 0.5f;

    public CircleCollider2D circleCollider;

    public Transform player;

    public virtual void Interact()
    {
        //Meant to be Overwritten
        Debug.Log("Interacting with " + transform.name);
    }

    private void Update()
    {
        circleCollider.radius = radius;

        Vector3 distance = transform.position - player.position;

        //if ()
        //{

        //}
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }



}
