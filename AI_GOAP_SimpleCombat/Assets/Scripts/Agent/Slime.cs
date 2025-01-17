using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : GAgent
{
    private GameObject targetEnemy;

    [Header("Attack Variables")]
    [SerializeField] public Transform attackPoint;
    [SerializeField] public float attackRadius;
    [SerializeField] public LayerMask attackMask;

    void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("damagePlayer", 1, false);
        goals.Add(s1, 5);

        SubGoal s2 = new SubGoal("targetInRange", 1, false);
        goals.Add(s2, 3);

        SubGoal s3 = new SubGoal("wanderToPoint", 1, false);
        goals.Add(s3, 1);

        targetEnemy = GWorld.Instance.GetQueue("players").RemoveResource();
        inventory.AddItem(targetEnemy);
        GWorld.Instance.GetQueue("players").AddResource(targetEnemy);
    }

    public bool TargetInRange()
    { 
        float distance = Vector3.Distance(this.gameObject.transform.position, attackPoint.transform.position);
        Gizmos.DrawWireSphere(this.gameObject.transform.position, distance + attackRadius);

        Collider[] targets = Physics.OverlapSphere(this.gameObject.transform.position, distance + attackRadius, attackMask);
        if (targets != null)
        {
            beliefs.ModifyState("TargetOnRange", 0);
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        Gizmos.color = Color.white;
        float distance = Vector3.Distance(this.gameObject.transform.position, attackPoint.transform.position);
        Gizmos.DrawWireSphere(this.gameObject.transform.position, distance + attackRadius);
    }
}
