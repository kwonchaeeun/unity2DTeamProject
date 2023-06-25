using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masition : Soul
{
    public Masition(string name) : base(name)
    {
        skills.Add(KeyCode.X, new KnightSkill1(this));
        skills.Add(KeyCode.C, new KnightSkill1(this));
        state = new MasitionIdleState();
    }

    public override void Start(InputManager input)
    {
        if (Mathf.Abs(input.moveDir) > 0)
            state = new MasitionWalkState();
        else
            state = new MasitionIdleState();
        state.start(this, input);
    }

    public override void Update(InputManager input)
    {
        base.Update(input);
    }

    public override void FixedUpdate(InputManager input)
    {
        IsGround(this);
        state.fixedUpdate(this, input);
    }

    public override SoulState StateChanger(State innerState)
    {
        switch (innerState)
        {
            case State.IDLE:
                return new MasitionIdleState();
            case State.WALK:
                return new MasitionWalkState();
            case State.JUMP:
                return new MasitionJumpState();
            case State.FALL:
                return new MasitionFallState();
            case State.DASH:
                return new MasitionDashState();
            case State.BASEATTACK:
                return new MasitionGroundBasicAttackState();
            case State.AIRATTACK:
                return new MasitionAirBasicAttackState();
            case State.SKILL:
                return new SkillAdapterState();
            default:
                return null;
        }
    }

    public override void SwapingSoul(InputManager input)
    {
        state.end(this, input);
        state = new MasitionIdleState();
    }
}
