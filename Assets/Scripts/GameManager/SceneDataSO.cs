using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "SO/Scene", order = 0)]
public class SceneDataSO : ScriptableObject
{
    public string sceneName; // Name of the scene
    public string sceneAssetPath; // Path to the scene asset file
    public SpawnPointDataSO spawnPointDataSO; // Data for the scene's spawn point
    // Add other scene-specific data as needed
}