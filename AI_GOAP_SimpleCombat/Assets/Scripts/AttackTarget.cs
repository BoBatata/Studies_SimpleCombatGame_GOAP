using UnityEngine;

public class AttackTarget : GAction
{
    private MeleeAttackBehavior meleeAttack;

    void Awake()
    {
        base.Awake();
        meleeAttack = GetComponent<MeleeAttackBehavior>();
    }

    public override bool PrePerform()
    {
        target = this.gameObject;
        meleeAttack.AttackPlayer();
        return true;
    }

    public override bool PostPerform()
    {
        //GWorld.Instance.GetWorld().ModifyState("TargetOnRange", -1);
        beliefs.ModifyState("TargetOnRange", -1);
        return true;
    }
}
