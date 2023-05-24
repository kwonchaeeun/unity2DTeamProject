using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Skill
{
    private string name;
    protected Soul soul;
    protected float cooldown;
    private float time;
    protected bool isSkillAvailable;
    public Skill(Soul soul)
    {
        this.soul = soul;
        this.cooldown = 3.0f;
        this.isSkillAvailable = true;
        this.time = 0.0f;
    }

    abstract public void start(InputManager input);
    abstract public State handleInput(InputManager input);
    abstract public void update(InputManager input);
    abstract public void fixedUpdate(InputManager input);
    abstract public void end(InputManager input);

    public void ColldownUpdate()
    {
        if (isSkillAvailable) return;
        time += Time.deltaTime;
        if (time >= cooldown)
        {
            isSkillAvailable = true;
            time = 0.0f;
        }
    }

    public bool CanUseSkill()
    {
        if (this.isSkillAvailable)
            return true;
        else
            return false;
    }
}

public class KnightSkill : Skill
{
    GameObject prefab;
    private Vector3 startPos = new Vector3();
    private float time;
    private bool isAttacked;

    public KnightSkill(Soul soul) : base(soul)
    {
        prefab = Resources.Load<GameObject>("Prefab/fireBall");
    }

    public override void start(InputManager input)
    {
        isSkillAvailable = false;
        isAttacked = false;
        time = 0.0f;
        startPos = soul.mTransform.position;
        /*GameObject obj = Object.Instantiate(prefab, soul.mTransform.position + new Vector3(soul.MoveData.lookAt * -1 * 2.0f, 1.0f, 0.0f), soul.mTransform.rotation);
        obj.GetComponent<Projectile>().Initailize(soul.MoveData.lookAt * -1, 5.0f, 50.0f);*/
        soul.Anime.Play("SKILL1");
    }

    public override State handleInput(InputManager input)
    {
        if (time >= 0.4f)
            if (soul.IsOnGround)
                return State.IDLE;
            else
                return State.FALL;
        return State.NULL;
    }

    public override void update(InputManager input) { }

    public override void fixedUpdate(InputManager input)
    {
        time += Time.fixedDeltaTime;
        soul.Rigid.velocity = new Vector2(soul.Rigid.velocity.x, 0.0f);
        if (Vector3.Distance(startPos, soul.mTransform.position) <= 7f)
        {
            soul.mTransform.position = Vector2.MoveTowards(soul.mTransform.position, soul.mTransform.position + new Vector3(soul.MoveData.lookAt * 40.0f * Time.fixedDeltaTime, 0), 1.0f);
        }
        else
        {
            if (!isAttacked)
            {
                CreateHitbox();
                isAttacked = true;
            }
        }

    }

    public override void end(InputManager input)
    {
        if (!isAttacked)
            CreateHitbox();
    }

    private void CreateHitbox()
    {
        float offsetX = Mathf.Abs(startPos.x + soul.mTransform.position.x) * 0.5f;
        float offsetY = soul.Collider.offset.y;
        float sizeX = Mathf.Abs(startPos.x - soul.mTransform.position.x);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(new Vector2(offsetX, soul.mTransform.position.y + offsetY), new Vector2(sizeX, soul.Collider.bounds.size.y), 0, Vector2.up, 0, (int)Layer.Monster);
        foreach (RaycastHit2D hit in hits)
        {
            hit.collider.GetComponent<Monster>().Hit();
        }
    }
}

public class SoldierSkill : Skill
{
    GameObject prefab;
    private bool isAttack;
    private float degree;
    private Vector2 direction;
    public SoldierSkill(Soul soul) : base(soul)
    {
        prefab = Resources.Load<GameObject>("Prefab/fireBall");
        isAttack = false;
        degree = 0.0f;
        direction = Vector2.right;
    }

    public override void start(InputManager input)
    {
        isAttack = false;
        degree = 0.0f;
        direction = new Vector2(soul.MoveData.lookAt, 0.0f);
    }

    public override State handleInput(InputManager input)
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            isSkillAvailable = false;
            return State.IDLE;
        }
        return State.NULL;
    }
    public override void update(InputManager input)
    {
        switch (Input.GetAxisRaw("Vertical"))
        {
            case 1:
                if (degree < 70)
                    degree += 20.0f * Time.deltaTime;
                break;
            case -1:
                if (degree > -70)
                    degree -= 20.0f * Time.deltaTime;
                break;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isAttack = true;
        }
        Debug.Log(degree);
        direction.x = soul.MoveData.lookAt * Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * degree));
        direction.y = Mathf.Sin(Mathf.Deg2Rad * degree);
        direction = direction.normalized;
    }
    public override void fixedUpdate(InputManager input)
    {
        if (isAttack)
        {
            createProjectile();
            isAttack = false;
        }
    }

    public override void end(InputManager input)
    {

    }

    private void createProjectile()
    {
        GameObject obj = Object.Instantiate(prefab, soul.mTransform.position + new Vector3(direction.x * (soul.Collider.bounds.size.x + 1.0f), soul.Collider.offset.y, 0.0f), Quaternion.identity);
        obj.GetComponent<Projectile>().Initailize(soul.MoveData.lookAt, direction, 5.0f, soul.Data.damage);

    }
}
