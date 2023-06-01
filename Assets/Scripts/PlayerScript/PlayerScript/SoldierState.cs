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
        attackDelay[0] = 0.667f;
        attackDelay[1] = 0.667f;
        attackDelay[2] = 0.333f;
        projectile.Add(Resources.Load<GameObject>("Prefab/Projectile/SoldierProjectile1"));
        projectile.Add(Resources.Load<GameObject>("Prefab/Projectile/SoldierProjectile2"));
        switch (soul.AttackCount)
        {
            case 0:
            case 1:
                projectileIndex = 0;
                break;
            case 2:
                projectileIndex = 1;
                break;
        }
        createProjectile(soul, projectileIndex);
    }
    public override void update(Soul soul, InputManager input)
    {
        time += Time.deltaTime;
        switch (soul.AttackCount)
        {
            case 0:
            case 1:
                if (time >= (attackDelay[soul.AttackCount] * 0.5f) && !isAttack)
                {
                    isAttack = createProjectile(soul, projectileIndex);
                }
                break;
            default:
                break;

        }
    }
}

public class SoldierAirBasicAttackState : RangedAirBasicAttackState
{
    public override void start(Soul soul, InputManager input)
    {
        base.start(soul, input);
        projectile = Resources.Load<GameObject>("Prefab/Projectile/SoldierProjectile1");
        Debug.Log(projectile.name);
    }
}


