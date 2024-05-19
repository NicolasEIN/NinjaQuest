using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPointData", menuName = "SO/SpawnPoint", order = 0)]
public class SpawnPointDataSO : ScriptableObject
{
    public GameObject spawnPointObject; // Reference to the GameObject representing the spawn point
    public Vector2 spawnPosition; // Additional field to store the spawn point's world position
    // Add other fields as needed (rotation, etc.)
}