using UnityEngine;

public class ZombieHealthSimple : MonoBehaviour
{
    public int health = 5; 

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false); 
    }
}
