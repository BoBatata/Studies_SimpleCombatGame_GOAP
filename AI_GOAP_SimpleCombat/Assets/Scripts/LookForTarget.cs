using UnityEngine;

public class LookForTarget : GAction
{
    public override bool PrePerform()
    {
        TargetOnSight();
        if (!TargetOnSight())
        {
            TargetOnSight();
            return false;
        }
        return true;
    }

    public override bool PostPerform()
    {
        target = this.gameObject;
        return true;
    }



    public bool TargetOnSight()
    {
        RaycastHit hit;
        Vector3 offSet = new Vector3(0, .5f, 0);
        GameObject player = inventory.FindItemWithTag("Player");
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
        //GWorld.Instance.GetWorld().ModifyState("TargetIsVisible", 1);
        return true;
    }
}
