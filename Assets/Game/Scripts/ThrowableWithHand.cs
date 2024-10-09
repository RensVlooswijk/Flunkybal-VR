using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab; // Assumes you're using Oculus SDK, adjust accordingly.

public class ThrowableWithHand : MonoBehaviour
{
    private Rigidbody rb;
    private bool isHeld = false;
    private Vector3 lastPosition;
    private Vector3 throwVelocity;
    private HandGrabInteractor handGrabber;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isHeld)
        {
            // Calculate velocity based on position change
            throwVelocity = (transform.position - lastPosition) / Time.deltaTime;
            lastPosition = transform.position;
        }
    }

    // This method is triggered when the ball is grabbed
    public void OnGrab(HandGrabInteractor handGrab)
    {
        isHeld = true;
        rb.isKinematic = true; // Turn off physics while held
        lastPosition = transform.position; // Capture the position at grab time
        handGrabber = handGrab; // Capture reference to the hand grabber
    }

    // This method is triggered when the ball is released
    public void OnRelease()
    {
        isHeld = false;
        rb.isKinematic = false; // Re-enable physics on release
        rb.velocity = throwVelocity; // Apply the velocity for the throw
        handGrabber = null; // Clear hand grabber reference
    }
}
