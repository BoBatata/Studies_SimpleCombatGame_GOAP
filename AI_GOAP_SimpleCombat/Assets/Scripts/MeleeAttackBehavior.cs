using UnityEngine;

public class MeleeAttackBehavior : MonoBehaviour
{
    //Debug
    private Color color;

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

        color = Color.green;
        canAttack = false;
    }

    private void Update()
    {
        if (canAttack)
        {
            AttackPlayer();
        }
        else
        {
            color = Color.green;
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
                color = Color.red;
                player.TakeDamage();
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        Gizmos.color = Color.white;
        float distance = Vector3.Distance(this.gameObject.transform.position, attackPoint.transform.position);
        Gizmos.DrawWireSphere(this.gameObject.transform.position, distance + attackRadius);
    }
}
