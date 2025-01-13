using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoToTarget : GAction
{
    public override bool PrePerform()
    {
        target = inventory.FindItemWithTag("Player");

        if (target == null)
        {
            return false;
        }
        return true;
    }

    public override bool PostPerform()
    {
        beliefs.ModifyState("TargetIsVisible", -1);
        beliefs.ModifyState("TargetOnRange", 0);
        //GWorld.Instance.GetWorld().ModifyState("TargetOnRange", 1);
        return true;
    }
}
