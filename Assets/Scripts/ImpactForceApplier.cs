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

        // Automatically load settings from SettingsManager if available
        if (SettingsManager.Instance != null && SettingsManager.Instance.ImpactSettings != null)
        {
            extraForce = SettingsManager.Instance.ImpactSettings.extraForce;
            speedThreshold = SettingsManager.Instance.ImpactSettings.speedThreshold;
        }
        else
        {
            Debug.LogWarning("SettingsManager or ImpactSettings is not assigned.");
        }

        if (rb == null)
        {
            Debug.LogError("Rigidbody not found!");
        }
        else if (!rb.isKinematic)
        {
            Debug.LogWarning("Rigidbody should be kinematic.");
        }

        if (triggerCollider == null)
        {
            Debug.LogError("Collider not found!");
        }
        else if (!triggerCollider.isTrigger)
        {
            Debug.LogWarning("Collider should be set as Trigger!");
        }

        lastPosition = transform.position;
    }

    private void Update()
    {
        currentVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;

        /*if (triggerCollider != null)
        {
            bool shouldBeEnabled = currentVelocity.magnitude >= speedThreshold;
            if (triggerCollider.enabled != shouldBeEnabled)
            {
                triggerCollider.enabled = shouldBeEnabled;
            }
        }
        */
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
                otherRb.AddForce(impactDirection * extraForce * currentVelocity.magnitude, ForceMode.Impulse);
                if (speed >= speedThreshold * 3)
                {
                    // Try to find BruiseController on the impacted object or its parents
                    BruiseController bruiseController = other.GetComponentInParent<BruiseController>();
                    if (bruiseController != null)
                    {
                        bruiseController.SpawnBruise(other.ClosestPoint(transform.position));
                    }

                    // --- ProgressiveJointRelaxer logic ---
                    ProgressiveJointRelaxer relaxer = other.GetComponentInParent<ProgressiveJointRelaxer>();
                    if (relaxer != null)
                    {
                        relaxer.RelaxOneStep();
                    }
                }
                // --- End ProgressiveJointRelaxer logic ---

                onImpactForceApplied?.Invoke();
            }
        }
    }
}
