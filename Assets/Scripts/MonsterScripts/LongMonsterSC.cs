using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LongMonsterSC : EnemySC
{
    public Slider healthBarSlider;
    void Start()
    {
        enemyhp = 5;
    }

    void Update()
    {
        if(enemyhp == 0){
            EnemyDie();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Hit();
            healthBarSlider.value -= .2f;
            Debug.Log(enemyhp);
        }
    }
}
