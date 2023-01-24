using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3f;
    public float limit = 6f;
    public float distanceDrag = 3f;
    public float minDistanceSwipe = 2f;
    public float maxTimeSwipe = 1f;

    public bool isGrounded = false;

    private void OnTriggerEnter(Collider other)
    {
        var ground = other.gameObject.GetComponent<Ground>();
        if (ground)
        {
            Debug.Log("Enter " + isGrounded);
            this.isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var ground = other.gameObject.GetComponent<Ground>();
        if (ground)
        {
            Debug.Log("End " + isGrounded);
            this.isGrounded = false;
        }
    }


}
