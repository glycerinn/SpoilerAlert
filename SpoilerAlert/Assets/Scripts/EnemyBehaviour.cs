using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public EnemySO EnemySO;
    public int CurrentHealth;

    public void Awake()
    {
        CurrentHealth = EnemySO.MaxEnemyHealth;
    }

    public void Update()
    {
        if(CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnMouseDown()
    {
        takeDamage(1);
    }

    public void takeDamage(int damage)
    {
        CurrentHealth -= damage;
    }

}
