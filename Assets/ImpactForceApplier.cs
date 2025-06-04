using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ImpactForceApplier : MonoBehaviour
{
    [Header("Force Settings")]
    public float extraForce = 10f;          
    public float speedThreshold = 1f;       

    [Header("ImpactEvent")]
    public UnityEvent onImpactForceApplied;

    private Rigidbody rb;
    private Collider triggerCollider;
    private Vector3 lastPosition;
    private Vector3 currentVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        triggerCollider = GetComponent<Collider>();

        if (rb == null)
        {
            Debug.LogError(" Rigidbody not found!");
        }
        else if (!rb.isKinematic)
        {
            Debug.LogWarning(" Not Kinimatic Rigidbody.");
        }

        if (triggerCollider == null)
        {
            Debug.LogError("Collider not found !");
        }
        else if (!triggerCollider.isTrigger)
        {
            Debug.LogWarning("Collider need to be Trigger!");
        }

        lastPosition = transform.position;
    }

    private void Update()
    {
      
        currentVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;


        if (triggerCollider != null)
        {
            bool shouldBeEnabled = currentVelocity.magnitude >= speedThreshold;
            if (triggerCollider.enabled != shouldBeEnabled)
            {
                triggerCollider.enabled = shouldBeEnabled;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody otherRb = other.attachedRigidbody;

        if (otherRb != null && otherRb != rb)
        {
            float speed = currentVelocity.magnitude;

            if (speed >= speedThreshold)
            {
                Vector3 impactDirection = currentVelocity.normalized;
                
                otherRb.AddForce(impactDirection * extraForce* currentVelocity.magnitude, ForceMode.Impulse);

                onImpactForceApplied?.Invoke();
            }
        }
    }
}
