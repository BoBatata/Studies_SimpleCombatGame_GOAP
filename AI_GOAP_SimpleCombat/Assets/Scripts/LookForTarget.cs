using UnityEngine;

public class LookForTarget : GAction
{
    Vector3 offSet = new Vector3(0, .5f, 0);
    public override bool PrePerform()
    {
        RaycastHit hit;
        Vector3 direction = target.transform.position - this.transform.position;
        GameObject player = inventory.FindItemWithTag("Player");

        Debug.DrawRay(this.transform.position  + offSet, direction + offSet, Color.red);
        if (Physics.Raycast(transform.position + offSet, direction + offSet, out hit , Mathf.Infinity))
        {
            if (hit.transform.tag == player.tag)
            {
                player = target;
            }
        }
        return true;
    }

    public override bool PostPerform()
    {
        return true;
    }
}
