using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "EnemyData/EnemyStats", order = 1)]
public class EnemyData : ScriptableObject
{
    public float moveSpeed = 3f;
    public float attackDistance = 1.5f;
    public float detectionRadius = 5f;
}
