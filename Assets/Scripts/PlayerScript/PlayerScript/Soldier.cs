using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Soul
{

    private float coolTime = 0.0f;
    public Soldier() { }

    public Soldier(string name) : base(name)
    {
        skills.Add(KeyCode.X, new SoldierSkill(this));
        skills.Add(KeyCode.C, new SoldierSkill(this));
        state = new SoldierIdleState();
    }

    public override void Start(InputManager input)
    {
        if (Mathf.Abs(input.moveDir) > 0)
            state = new SoldierWalkState();
        else
            state = new SoldierIdleState();
        state.start(this, input);
    }

    override public void Update(InputManager input)
    {
        foreach (KeyValuePair<KeyCode, Skill> skill in Skills)
        {
            skill.Value.ColldownUpdate();
        }
        moveData.lookAt = (sprite.flipX) ? -1 : 1;
        state.update(this, input);
        if (attackCount >= 1)
            combatAttackTerm -= Time.deltaTime;
        if (attackCount == 3)
            attackCount = 0;
        if (combatAttackTerm <= 0)
        {
            combatAttackTerm = 1.5f;
            attackCount = 0;
        }
        if (!cooldownTime.dashCoolingdown)
        {
            coolTime += Time.deltaTime;
            if (cooldownTime.dashCooldownTime <= coolTime)
            {
                coolTime = 0.0f;
                cooldownTime.dashCoolingdown = true;
            }
        }
    }

    override public void FixedUpdate(InputManager input)
    {
        IsGround(this);
        state.fixedUpdate(this, input);
    }

    public override void SwapingSoul(InputManager input)
    {
        this.state.end(this, input);
        this.state = new SoldierIdleState();
    }

    public override SoulState StateChanger(State innerState)
    {
        switch (innerState)
        {
            case State.IDLE:
                return new SoldierIdleState();
            case State.WALK:
                return new SoldierWalkState();
            case State.JUMP:
                return new SoldierJumpState();
            case State.FALL:
                return new SoldierFallState();
            case State.DASH:
                return new SoldierDashState();
            case State.BASEATTACK:
                return new SoldierGroundBasicAttackState();
            case State.AIRATTACK:
                return new SoldierAirBasicAttackState();
            case State.SKILL:
                return new SkillAdapterState();
            default:
                return null;
        }
    }
}
