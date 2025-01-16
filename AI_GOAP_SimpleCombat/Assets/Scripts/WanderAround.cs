using UnityEngine;

public class WanderAround : GAction
{
    [SerializeField] private GameObject pointPrefab;
    private Slime slime;

    private void Awake()
    {
        base.Awake();
        slime = GetComponent<Slime>();
    }

    public override bool PrePerform()
    {
        float distance = Vector3.Distance(this.gameObject.transform.position, slime.attackPoint.transform.position);
        float range = slime.attackRadius + distance;
        Vector3 randomPos = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        target = Instantiate(pointPrefab, randomPos, Quaternion.identity);
        return true;
    }

    public override bool PostPerform()
    {
        Destroy(target.gameObject);
        TargetOnSight();
        return true;
    }

    public void TargetOnSight()
    {
        RaycastHit hit;
        Vector3 offSet = new Vector3(0, .5f, 0);
        GameObject player = inventory.FindItemWithTag("Player");
        Vector3 direction = player.transform.position - this.transform.position;

        Debug.DrawRay(this.transform.position + offSet, direction + offSet, Color.red);
        if (Physics.Raycast(transform.position + offSet, direction + offSet, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == player.tag)
            {
                beliefs.ModifyState("TargetIsVisible", 0);
            }
        }
    }
}
