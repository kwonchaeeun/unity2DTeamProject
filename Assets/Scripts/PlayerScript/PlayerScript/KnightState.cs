using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightIdleState : IdleState { }

public class KnightWalkState : WalkState { }

public class KnightJumpState : JumpState { }

public class KnightFallState : FallState { }

public class KnightDashState : DashState { }

public class KnightGroundBasicAttackState : MeleeGroundBasicAttackState 
{
    public override void start(Soul soul, InputManager input)
    {
        base.start(soul, input);
        attackDelay[0] = 0.42f;
        attackDelay[1] = 0.34f;
        attackDelay[2] = 0.34f;
        offset = new Vector2(1.1f, soul.Collider.offset.y);
        size = new Vector2(3.0f, soul.Collider.bounds.size.y);
    }
}

public class KnightAirBasicAttackState : MeleeAirBasicAttackState
{
    public override void start(Soul soul, InputManager input)
    {
        base.start(soul, input);
        delay = 0.42f;
        offset = new Vector2(1.1f, soul.Collider.offset.y);
        size = new Vector2(3.0f, soul.Collider.bounds.size.y);
    }
}