using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianIdleState : IdleState { }

public class MagicianWalkState : WalkState { }

public class MagicianJumpState : JumpState { }

public class MagicianFallState : FallState { }

public class MagicianDashState : DashState { }

public class MagicianGroundBasicAttackState : RangedGroundBasicAttackState
{
    public override void start(Soul soul, InputManager input)
    {
        audioClip = Resources.Load<AudioClip>("Sound/Soldier/Attack/Attack");
        base.start(soul, input);
        attackDelay[0] = attackDelay[1] = attackDelay[2] = 0.417f;
        projectile.Add(Resources.Load<GameObject>("Prefab/Projectile/MagicianBaseProjectile1"));
        projectile.Add(Resources.Load<GameObject>("Prefab/Projectile/MagicianBaseProjectile2"));
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

public class MagicianAirBasicAttackState : RangedAirBasicAttackState
{
    
    public override void start(Soul soul, InputManager input)
    {
        delay = 0.25f;
        audioClip = Resources.Load<AudioClip>("Sound/Soldier/Attack/Attack");
        base.start(soul, input);
        projectile = Resources.Load<GameObject>("Prefab/Projectile/MagicianBaseProjectile1");
        Debug.Log(projectile.name);
    }
}


public class MagicianSkill1 : Skill
{
    private GameObject prefab;
    private Vector3 offset;
    private float time;
    public MagicianSkill1(Soul soul) : base(soul, 2.0f, 5)
    {
        prefab = Resources.Load<GameObject>("Prefab/Projectile/MagicianSkill1Projectile");
    }

    public override void start(InputManager input)
    {
        base.start(input);
        isSkillAvailable = false;
        time = 0.0f;   
        for(int i = 0; i < 3; i++)
        {
            int randomX = Random.Range(-10, 11);
            int randomY = Random.Range(-10, 11);
            offset = new Vector3(soul.MoveData.lookAt * 1.2f + randomX * 0.2f, 1.0f + randomY *0.2f, 0.0f);
            GameObject.Instantiate(prefab, soul.mTransform.position + offset, Quaternion.identity).GetComponent<GuidedMissile>().Initailize(soul.Data.damage * 3);
        }
        soul.Anime.Play("SKILL1");
    }

    public override State handleInput(InputManager input)
    {
        if (time >= 0.5f)
            return State.IDLE;
        return State.NULL;
    }

    public override void update(InputManager input)
    {
        time += Time.deltaTime;
    }

    public override void fixedUpdate(InputManager input) { }
    public override void end(InputManager input) { }
}
