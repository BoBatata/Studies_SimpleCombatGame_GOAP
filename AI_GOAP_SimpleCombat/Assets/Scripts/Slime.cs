using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : GAgent
{
    [SerializeField] private bool targetSpotted;
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

        targetSpotted = false;

        targetEnemy = GWorld.Instance.GetQueue("players").RemoveResource();
        inventory.AddItem(targetEnemy);
        GWorld.Instance.GetQueue("players").AddResource(targetEnemy);
    }

    private void Update()
    {

        if (!targetSpotted)
        {
            TargetOnSight();
        }
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


    public bool TargetOnSight()
    {
        RaycastHit hit;
        Vector3 offSet = new Vector3(0, .5f, 0);
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 direction = player.transform.position - this.transform.position;

        Debug.DrawRay(this.transform.position + offSet, direction + offSet, Color.red);
        if (Physics.Raycast(transform.position + offSet, direction + offSet, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag != player.tag)
            {
                return false;
            }
        }
        beliefs.ModifyState("TargetIsVisible", 0);
        targetSpotted = true;
        return true;
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
