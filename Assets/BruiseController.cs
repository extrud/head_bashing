using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruiseController : MonoBehaviour
{
    private GameObject bruisePrefab;

    private void Start()
    {
        // Get the bruise prefab from the settings manager at startup
        if (SettingsManager.Instance != null && SettingsManager.Instance.ImpactSettings != null)
        {
            bruisePrefab = SettingsManager.Instance.ImpactSettings.bruisePrefab;
        }
        else
        {
            Debug.LogWarning("SettingsManager or ImpactSettings is not assigned.");
        }
    }

    /// <summary>
    /// Spawns a bruise prefab at the given position and sets this object's transform as the parent.
    /// </summary>
    /// <param name="position">The world position to spawn the bruise at.</param>
    public void SpawnBruise(Vector3 position)
    {
        if (bruisePrefab == null)
        {
            Debug.LogWarning("Bruise prefab is not assigned.");
            return;
        }
        GameObject bruise = Instantiate(bruisePrefab, position, Quaternion.identity, this.transform);
    }
}
