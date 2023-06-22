using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public enum DamageType
{
    HP,
    INTELLECTUALITY
}

public class InputManager
{
    public float moveDir;
    public bool isJumpKeyDown;
    public bool isDownJumpKeyDown;
    public bool isDashKeyDown;
    public bool isAttackKeyDown;
    public (bool, KeyCode) isSkillKeyDown;
    public InputManager()
    {
        moveDir = 0.0f;
        isJumpKeyDown = false;
        isDownJumpKeyDown = false;
        isDashKeyDown = false;
        isAttackKeyDown = false;
        isSkillKeyDown = (false, KeyCode.None);
    }
    public void reset()
    {
        moveDir = 0.0f;
        isJumpKeyDown = false;
        isDownJumpKeyDown = false;
        isDashKeyDown = false;
        isAttackKeyDown = false;
        isSkillKeyDown = (false, KeyCode.None);
    }
}

public class PlayerData
{
    public int hp;
    public int intellectuality;

    public PlayerData()
    {
        this.hp = 3;
        this.intellectuality = 100;
    }
}

public class PlayerController : MonoBehaviour
{

    public delegate void healthEventHandler();
    public healthEventHandler HealthEventHandler;

    public delegate void skillCooldownEventHandler();
    public skillCooldownEventHandler SkillCooldownEventHandler;

    public delegate void soulSwapEventHandler();
    public soulSwapEventHandler SoulSwapEventHandler;

    private PlayerData playerData = new PlayerData();
    public PlayerData PlayerData { get { return playerData; } }

    //Input���� ����
    private InputManager input = new InputManager();

    //soul
    private int currIndex = 0;
    public int MainIndex { get { return currIndex; } }
    private int subIndex = 1;
    public int SubIndex { get { return subIndex; } }
    private Soul currSoul;
    public Soul CurrSoul { get { return currSoul; } }
    private List<Soul> ownSouls;
    public List<Soul> OwnSouls { get { return ownSouls; } }
    // Start is called before the first frame update
    private void Awake()
    {
        ownSouls = new List<Soul>();
        //base ĳ���� �ʱ�ȭ
        InitializeSoul();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currSoul == null)
            return;
        input.moveDir = Input.GetAxisRaw("Horizontal");

        if (!(input.isJumpKeyDown || input.isDownJumpKeyDown) && Input.GetButtonDown("Jump") && currSoul.MoveData.jumpCount < currSoul.Data.availableJumpCount)
        {
            if (!Input.GetKey(KeyCode.DownArrow))
            {
                input.isJumpKeyDown = true;
            }
            else
            {
                input.isDownJumpKeyDown = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && currSoul.mCooldownTime.dashCoolingdown && currSoul.Data.isUseDash)
        {
            input.isDashKeyDown = true;
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            input.isAttackKeyDown = true;
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool result = SwapSoul();
            if (result)
                currSoul.Start(input);
        }
        if (!input.isSkillKeyDown.Item1)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (currSoul.Skills.ContainsKey(KeyCode.X) && currSoul.Skills[KeyCode.X].CanUseSkill())
                    input.isSkillKeyDown = (true, KeyCode.X);
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                if (currSoul.Skills.ContainsKey(KeyCode.C) && currSoul.Skills[KeyCode.C].CanUseSkill())
                    input.isSkillKeyDown = (true, KeyCode.C);
            }
        }
        SkillCooldownEventHandler();
        currSoul.HandleInput(input);
        currSoul.Update(input);
        if(ownSouls.Count == 2)
            ownSouls[subIndex].Update(input);
    }

    private void FixedUpdate()
    {
        currSoul.FixedUpdate(input);
    }

    public void InitializeSoul()
    {
        object[] args = new object[] { "Knight" };
        Type t = Type.GetType("Knight");
        ownSouls.Add((Soul)System.Activator.CreateInstance(t, args));
        ownSouls[currIndex].Initialize(this.GetComponent<Collider2D>(), this.GetComponent<Rigidbody2D>(), this.transform, this.GetComponent<SpriteRenderer>(), this.GetComponent<Animator>(), this.GetComponent<AudioSource>());
        currSoul = ownSouls[currIndex];
        this.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animator/SoulAnimator/" + currSoul.Data.name + "_Anime") as RuntimeAnimatorController;
    }

    public bool SwapSoul()
    {
        if (ownSouls.Count == 2)
        {
            switch (currIndex)
            {
                case 0:
                    currIndex = 1;
                    subIndex = 0;
                    break;
                case 1:
                    currIndex = 0;
                    subIndex = 1;
                    break;
                default:
                    break;
            }
            //currSoul.SwapingSoul(input);
            currSoul = ownSouls[currIndex];
            this.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animator/SoulAnimator/" + currSoul.Data.name + "_Anime") as RuntimeAnimatorController;
            Debug.Log("�ҿ� ����");
            SoulSwapEventHandler();
            input.reset();
            return true;
        }
        else
        {
            Debug.Log("������ �ҿ��� �������� �ʽ��ϴ�.");
            return false;
        }

    }

    public void ModifySoul(string name, int selectedNum)
    {
        if (ownSouls.Count == 1)
        {
            object[] args = new object[] { name };
            Type t = Type.GetType(name);
            ownSouls.Add((Soul)System.Activator.CreateInstance(t, args));
            ownSouls[ownSouls.Count - 1].Initialize(this.GetComponent<Collider2D>(), this.GetComponent<Rigidbody2D>(), this.transform, this.GetComponent<SpriteRenderer>(), this.GetComponent<Animator>(), this.GetComponent<AudioSource>());
        }
        else
        {
            object[] args = new object[] { name };
            Type t = Type.GetType(name);
            ownSouls[selectedNum] = (Soul)System.Activator.CreateInstance(t, args);
            ownSouls[selectedNum].Initialize(this.GetComponent<Collider2D>(), this.GetComponent<Rigidbody2D>(), this.transform, this.GetComponent<SpriteRenderer>(), this.GetComponent<Animator>(), this.GetComponent<AudioSource>());
        }
        SoulSwapEventHandler();
    }

    public List<string> GetPlayerSoulNameList()
    {
        List<string> nameList = new List<string>();
        foreach (Soul soul in ownSouls)
        {
            nameList.Add(soul.Data.name);
        }
        return nameList;
    }

    public void Hit(DamageType damageType, int damage)
    {
        if (currSoul.soulState.GetType() == typeof(DeadState))
            return;
            
        switch (damageType)
        {
            case DamageType.HP:
                playerData.hp -= damage;
                break;
            case DamageType.INTELLECTUALITY:
                playerData.intellectuality -= damage;
                break;
        }
        HealthEventHandler();
        if (!isDead())
        {
            currSoul.Hit(input); 
        }
        else
        {
            currSoul.Dead(input);
        }
            
    }
    private bool isDead()
    {
        if (playerData.hp <= 0 || playerData.intellectuality <= 0)
            return true;
        return false;
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(new Vector2(offsetX, offsetY), new Vector2(offsetX * 2, offsetY * 2));
    }
}
