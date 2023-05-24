using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierIdleState : IdleState { }

public class SoldierWalkState : WalkState { }

public class SoldierJumpState : JumpState { }

public class SoldierFallState : FallState { }

public class SoldierDashState : DashState { }

public class SoldierGroundBasicAttackState : RangedGroundBasicAttackState 
{
    public override void start(Soul soul, InputManager input)
    {
        base.start(soul, input);
        projectile = Resources.Load<GameObject>("Prefab/fireBall");
        Debug.Log(projectile.name);
    }
}

public class SoldierAirBasicAttackState : RangedAirBasicAttackState
{
    public override void start(Soul soul, InputManager input)
    {
        base.start(soul, input);
        projectile = Resources.Load<GameObject>("Prefab/fireBall");
        Debug.Log(projectile.name);
    }
}


