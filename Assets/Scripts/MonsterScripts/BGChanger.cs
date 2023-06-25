using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGChanger : MonoBehaviour
{   
    public BossBG BG;  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Portal"))
        {
            if (BG != null)
            {
                BG.SwitchToBossStage();
            }
        }
    }


}
