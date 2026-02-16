using UnityEngine;

/// <summary>
/// Fixes the operating room pushing the VR player around.
///
/// The operating_room FBX imports with MeshColliders on every piece of
/// furniture / walls / ceiling. These collide with the XR rig's
/// CharacterController / capsule collider, pushing the player back.
///
/// This script removes ALL colliders and ALL Rigidbodies from the
/// operating room model immediately (DestroyImmediate so they're gone
/// before physics runs even one frame). The scene "Plane" object
/// acts as the floor.
///
/// Attach to the operating_room GameObject.
/// </summary>
public class OperatingRoomCollisionFix : MonoBehaviour
{
    private void Awake()
    {
        FixColliders();
    }

    private void FixColliders()
    {
        int removed = 0;

        // Remove ALL colliders from the operating room -- no exceptions.
        // The scene has a separate "Plane" for the floor.
        foreach (var col in GetComponentsInChildren<Collider>(true))
        {
            if (col == null) continue;
            DestroyImmediate(col);
            removed++;
        }

        // Remove ALL Rigidbodies
        foreach (var rb in GetComponentsInChildren<Rigidbody>(true))
        {
            if (rb != null) DestroyImmediate(rb);
        }

        Debug.Log($"[RoomCollisionFix] Removed {removed} colliders from operating room.");
    }
}
