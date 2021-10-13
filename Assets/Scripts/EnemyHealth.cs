using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] float respawnSeconds = 5f;
    Animator anim;
    bool alive = true;
    float HP;
    float respawnCountdownStart;

    void Start() {
        anim = GetComponent<Animator>();
        HP = hitPoints;
    }

    public bool IsAlive() {
        return alive;
    }

    public void TakeDamage(float damage) {
        if (!alive) {
            return;
        }
        HP -= damage;
        //Debug.Log("HP: " + HP.ToString());
        GetComponent<EnemyAi>().StartDamageCooldown();
        anim.SetTrigger(string.Format("Take_Damage_{0}",Random.Range(1,3)));
        if (HP <= damage && alive) {
            HP = 0;
            Die();
            return;
        }
    }

    private void Die() {
        anim.SetBool("Die", true);
        alive = false;
        respawnCountdownStart = Time.time;
    }

    void Update() {
        if (!alive) {
            Respawn();
        }
    }

    float RespawnCountdown() {
        float countDown = respawnSeconds - (Time.time - respawnCountdownStart);
        //Debug.Log(string.Format("Respawn countdown: {0}", countDown));
        return countDown;
    }

    private void Respawn()
    {
        if (RespawnCountdown() <= 0f) {
            alive = true;
            HP = hitPoints;
            anim.SetBool("Die", false);
            anim.SetTrigger("Rest_1");
            GetComponent<EnemyAi>().Respawn();
        }
    }

}
