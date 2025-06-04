using UnityEngine;

[RequireComponent(typeof(CustomJointLimitsController))]
public class ProgressiveJointRelaxer : MonoBehaviour
{
    [Header("Relaxation Step (degrees)")]
    public float stepLowAngularX = 10f;
    public float stepHighAngularX = 10f;
    public float stepAngularY = 10f;
    public float stepAngularZ = 10f;

    [Header("Maximum Relaxed Limits (degrees)")]
    public float maxRelaxedLowAngularX = -90f;
    public float maxRelaxedHighAngularX = 90f;
    public float maxRelaxedAngularY = 90f;
    public float maxRelaxedAngularZ = 90f;

    private CustomJointLimitsController jointLimitsController;
    private ConfigurableJoint joint;

    private void Awake()
    {
        jointLimitsController = GetComponent<CustomJointLimitsController>();
        joint = GetComponent<ConfigurableJoint>();
    }

    /// <summary>
    /// Instantly relaxes the joint limits by one step, not exceeding the maximum relaxed limits.
    /// </summary>
    public void RelaxOneStep()
    {
        if (joint == null || jointLimitsController == null)
            return;

        float newLowAngularX = Mathf.MoveTowards(joint.lowAngularXLimit.limit, maxRelaxedLowAngularX, stepLowAngularX);
        float newHighAngularX = Mathf.MoveTowards(joint.highAngularXLimit.limit, maxRelaxedHighAngularX, stepHighAngularX);
        float newAngularY = Mathf.MoveTowards(joint.angularYLimit.limit, maxRelaxedAngularY, stepAngularY);
        float newAngularZ = Mathf.MoveTowards(joint.angularZLimit.limit, maxRelaxedAngularZ, stepAngularZ);

        jointLimitsController.SetAngularLimits(newLowAngularX, newHighAngularX, newAngularY, newAngularZ);
    }
}