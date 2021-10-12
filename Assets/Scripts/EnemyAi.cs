using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 15;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    int ATTACKS = 3;
    float attackCooldown = 2f;
    float damageCooldown = 1.5f;
    float damageCooldownStart = 0;
    List<string> attackMoves;
    // damage point the monter gives to the player per attack
    float damagePoint = 10f;

    // Monster states
    bool isProvoked = false;
    bool attackState = false;
    float lastAttack = 0f;
    bool isWalking = false;

    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.isStopped = false;
        anim = GetComponent<Animator>();
        attackMoves = new List<string>();
        for (int i = 0; i < ATTACKS; i++) {
            attackMoves.Add(string.Format("Attack_{0}", i+1));
        }
    }

    bool IsAlive() {
        return GetComponent<EnemyHealth>().IsAlive();
    }

    void RandomAttack(){
        int at = Random.Range(0, ATTACKS-1);
        anim.SetTrigger(attackMoves[at]);
    }

    public void StartDamageCooldown() {
        if (damageCooldownStart == 0) {
            damageCooldownStart = Time.time;
            navMeshAgent.isStopped = true;
            anim.SetBool("DamageCooldown", true);
            return;
        }
    }

    bool IsDamageCooldown() {
        if (navMeshAgent.isStopped == false || damageCooldownStart == 0) {
            return false;
        }
        if (Time.time - damageCooldownStart >= damageCooldown) {
            navMeshAgent.isStopped = false;
            damageCooldownStart = 0;
            anim.SetBool("DamageCooldown", false);
            return false;
        }
        return true;

    }

    public void Respawn() {
        navMeshAgent.isStopped = false;
        isProvoked = false;
        isWalking = false;
        attackState = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsAlive()) {
            navMeshAgent.SetDestination(transform.position);
            navMeshAgent.isStopped = true;
            return;
        }
        if (IsDamageCooldown()) {
            return;
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(isProvoked) {
            EngageTarget();
        } else if (distanceToTarget <= chaseRange) {
            isProvoked = true;
        } else {
            attackState = false;
            anim.SetBool("Walking", false);
            isWalking = false;
        }
    }

    private void EngageTarget(){
        if (distanceToTarget <= navMeshAgent.stoppingDistance) {
            anim.SetBool("Walking", false);
            isWalking = false;
            attackTarget();
        }
        if (distanceToTarget > navMeshAgent.stoppingDistance) {
            chaseTarget();
        }
    }

    private void chaseTarget() {
        attackState = false;
        if (!isWalking) {
            anim.SetTrigger("Walking");
            anim.SetBool("Walking", true);
            isWalking = true;
            navMeshAgent.SetDestination(target.position);
        } else if (anim.GetCurrentAnimatorStateInfo(0).IsTag("walking")){
            navMeshAgent.SetDestination(target.position);
        }
    }

    private void attackTarget() {
        if (!attackState) {
            attackState = true;
            RandomAttack();
            lastAttack = Time.time;
        } else {
            if (Time.time - lastAttack >= attackCooldown) {
                RandomAttack();
                lastAttack = Time.time;
            }
        }
    }

    public void AttackEventCallback() {
        if (target == null) {
            return;
        }
        target.GetComponent<PlayerHealth>().TakeDamage(damagePoint);
    }
    
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
