using UnityEngine;

public class SnapXROriginOnStart : MonoBehaviour
{
    [Header("References")]
    public Transform xrOrigin;   // XR Origin (XR Rig)
    public Transform spawnPoint; // Empty at the place you want to start

    [Header("Timing")]
    [Tooltip("How long (in seconds) to keep forcing the rig onto the spawn point at startup.")]
    public float snapDuration = 1f;

    float elapsed;

    void Start()
    {
        ApplySnap();
    }

    void Update()
    {
        if (elapsed < snapDuration)
        {
            ApplySnap();
            elapsed += Time.deltaTime;
        }
    }

    void ApplySnap()
    {
        if (xrOrigin != null && spawnPoint != null)
        {
            xrOrigin.position = spawnPoint.position;
            xrOrigin.rotation = spawnPoint.rotation;
        }
    }
}

