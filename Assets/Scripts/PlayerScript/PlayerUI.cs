using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public PlayerController subject;
    [SerializeField]
    private TMP_Text hpText;
    [SerializeField]
    private TMP_Text intellectualityText;
    [SerializeField]
    private TMP_Text skill1Text;
    [SerializeField]
    private TMP_Text skill2Text;

    [SerializeField]
    private GameObject mainSoulImage;
    [SerializeField]
    private GameObject subSoulImage;

    // Start is called before the first frame update
    void Start()
    { 
        subject = GameObject.Find("Player").GetComponent<PlayerController>();
        subject.HealthEventHandler += OnHealthNotify;
        subject.SkillCooldownEventHandler += OnSkillCooldownNotify;
        subject.SoulSwapEventHandler += OnSoulSwapNotify;
        Debug.Log(subject.CurrSoul.Data.name);
        mainSoulImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/PlayerUI/mainSoul/" + subject.CurrSoul.Data.name + "UISprite");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnHealthNotify()
    {
        hpText.text = subject.PlayerData.hp.ToString();
        intellectualityText.text = subject.PlayerData.intellectuality.ToString();
    }

    public void OnSkillCooldownNotify()
    {
        if (subject.CurrSoul.Skills[KeyCode.X].CurrTime != 0.0f)
        {
            skill1Text.text = (subject.CurrSoul.Skills[KeyCode.X].Cooldown - subject.CurrSoul.Skills[KeyCode.X].CurrTime).ToString("F1");
        }
        else
        {
            skill1Text.text = 0.0f.ToString("F1");
        }
        if (subject.CurrSoul.Skills[KeyCode.C].CurrTime != 0.0f)
        {
            skill2Text.text = (subject.CurrSoul.Skills[KeyCode.C].Cooldown - subject.CurrSoul.Skills[KeyCode.C].CurrTime).ToString("F1");
        }
        else
        {
            skill2Text.text = 0.0f.ToString("F1");
        }
    }

    public void OnSoulSwapNotify()
    {
        mainSoulImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/PlayerUI/mainSoul/" + subject.OwnSouls[subject.MainIndex].Data.name + "UISprite");
        subSoulImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/PlayerUI/subSoul/" + subject.OwnSouls[subject.SubIndex].Data.name + "UISprite");
        skill1Text.text = 0.0f.ToString("F1");
        skill2Text.text = 0.0f.ToString("F1");
    }
}
