using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    float playerHP = 100f;
    bool alive = true;

    // Start is called before the first frame update
    public void TakeDamage(float damage)
    {
        if (!alive) {
            return;
        }
        playerHP -= damage;
        if (playerHP <= 0) {
            alive = false;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Game Over.");
    }

}
