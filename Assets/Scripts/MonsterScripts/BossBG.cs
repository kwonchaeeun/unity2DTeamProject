using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBG : MonoBehaviour
{
    public Image bossImage;        
    public Sprite bossSprite;     

    public void SwitchToBossStage()
    {
        if (bossImage != null && bossSprite != null)
        {
            bossImage.sprite = bossSprite;
        }
    }
}
