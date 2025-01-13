using UnityEngine;

public class MeleeAttackBehavior : MonoBehaviour
{
    [Header("Attack Variables")]
    private Transform attackPoint;
    private float attackRadius;
    private LayerMask attackMask;
    public bool canAttack;

    private Slime agent;
    //private Collider[] targets;

    private void Awake()
    {
        agent = GetComponent<Slime>();
        attackPoint = agent.attackPoint;
        attackRadius = agent.attackRadius;
        attackMask = agent.attackMask;

        canAttack = false;
    }

    private void Update()
    {
        if (canAttack)
        {
            AttackPlayer();
        }
    }

    public void AttackPlayer()
    {
        Collider[] targets = Physics.OverlapSphere(attackPoint.position, attackRadius, attackMask);
        foreach (Collider p in targets)
        {
            if(p.TryGetComponent(out Player player))
            {
                print("damage!");
                player.TakeDamage();
            }
        }

    }
}
