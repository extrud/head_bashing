using UnityEngine;

[CreateAssetMenu(fileName = "ImpactSettings", menuName = "ScriptableObjects/ImpactSettingsSO", order = 1)]
public class ImpactSettingsSO : ScriptableObject
{
    [Header("Impact Force Applier Settings")]
    public float extraForce = 10f;
    public float speedThreshold = 1f;

    [Header("Bruise Controller Settings")]
    public GameObject bruisePrefab;
}