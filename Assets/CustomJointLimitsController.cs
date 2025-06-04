using UnityEngine;

/// <summary>
/// Allows changing angular limits of a ConfigurableJoint at runtime.
/// </summary>
[RequireComponent(typeof(ConfigurableJoint))]
public class CustomJointLimitsController : MonoBehaviour
{
    private ConfigurableJoint joint;

    private void Awake()
    {
        joint = GetComponent<ConfigurableJoint>();
        if (joint == null)
        {
            Debug.LogError("ConfigurableJoint component not found.");
        }
    }

    /// <summary>
    /// Sets the angular limits for the joint.
    /// </summary>
    /// <param name="lowAngularX">Low angular X limit (in degrees).</param>
    /// <param name="highAngularX">High angular X limit (in degrees).</param>
    /// <param name="angularY">Angular Y limit (in degrees).</param>
    /// <param name="angularZ">Angular Z limit (in degrees).</param>
    public void SetAngularLimits(float lowAngularX, float highAngularX, float angularY, float angularZ)
    {
        if (joint == null) return;

        SoftJointLimit lowX = joint.lowAngularXLimit;
        lowX.limit = lowAngularX;
        joint.lowAngularXLimit = lowX;

        SoftJointLimit highX = joint.highAngularXLimit;
        highX.limit = highAngularX;
        joint.highAngularXLimit = highX;

        SoftJointLimit yLimit = joint.angularYLimit;
        yLimit.limit = angularY;
        joint.angularYLimit = yLimit;

        SoftJointLimit zLimit = joint.angularZLimit;
        zLimit.limit = angularZ;
        joint.angularZLimit = zLimit;
    }
}   