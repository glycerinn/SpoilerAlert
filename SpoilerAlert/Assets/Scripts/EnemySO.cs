using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies")]
public class EnemySO : ScriptableObject
{
    public new string name;
    public int MaxEnemyHealth;
    public float EnemySpeed;
    public float SpoilerSpeed;
}
