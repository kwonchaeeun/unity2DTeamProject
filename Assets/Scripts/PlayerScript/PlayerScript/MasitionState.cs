using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasitionIdleState : IdleState { }

public class MasitionWalkState : WalkState { }

public class MasitionJumpState : JumpState { }

public class MasitionFallState : FallState { }

public class MasitionDashState : DashState { }

public class MasitionGroundBasicAttackState : RangedGroundBasicAttackState
{
    public override void start(Soul soul, InputManager input)
    {
        audioClip = Resources.Load<AudioClip>("Sound/Soldier/Attack/Attack");
        base.start(soul, input);
        attackDelay[0] = attackDelay[1] = attackDelay[2] = 0.417f;
        projectile.Add(Resources.Load<GameObject>("Prefab/Projectile/MasitionBaseProjectile1"));
        projectile.Add(Resources.Load<GameObject>("Prefab/Projectile/MasitionBaseProjectile2"));
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
    }
}

public class MasitionAirBasicAttackState : RangedAirBasicAttackState
{
    
    public override void start(Soul soul, InputManager input)
    {
        delay = 0.25f;
        audioClip = Resources.Load<AudioClip>("Sound/Soldier/Attack/Attack");
        base.start(soul, input);
        projectile = Resources.Load<GameObject>("Prefab/Projectile/MasitionBaseProjectile1");
        Debug.Log(projectile.name);
    }
}

