using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruiseController : MonoBehaviour
{
    private GameObject bruisePrefab;

    public float maxBruiseDistance = 0.2f; // Maximum distance from transform
    public Vector3 offset = Vector3.zero; // Offset from transform      

    /// <summary>
    /// Spawns a bruise prefab at the given position and sets this object's transform as the parent.
    /// If the position is farther than maxBruiseDistance from this transform, it will be clamped.
    /// </summary>
    /// <param name="position">The world position to spawn the bruise at.</param>
    public void SpawnBruise(Vector3 position)
    {
        if (bruisePrefab == null)
        {
            Debug.LogWarning("Bruise prefab is not assigned.");
            return;
        }

        Vector3 origin = transform.position + transform.TransformVector(offset);
        Vector3 direction = position - origin;
        float distance = direction.magnitude;

        direction = direction.normalized * maxBruiseDistance;
        position = origin + direction;

        GameObject bruise = Instantiate(bruisePrefab, position, Quaternion.identity, this.transform);

        // Multiply bruise size by a random multiplier from 0.5 to 2
        float randomScale = Random.Range(0.5f, 2f);
        bruise.transform.localScale *= randomScale;
    }
}
