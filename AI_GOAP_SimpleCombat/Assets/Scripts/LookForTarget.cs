using UnityEngine;

public class LookForTarget : GAction
{
    public override bool PrePerform()
    {
        //GameObject player = GameObject.FindWithTag("Player");
        //Vector3 direction = player.transform.position - this.transform.position;
        //Debug.DrawRay(this.transform.position, direction, Color.red);
        if (!PlayerOnSight())
        {
            return false;
        }
        return true;
    }

    public override bool PostPerform()
    {
        return true;
    }

    private void Update()
    {
        PlayerOnSight();
    }

    public bool PlayerOnSight()
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
        return true;
    }
}
