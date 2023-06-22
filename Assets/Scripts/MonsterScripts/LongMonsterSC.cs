using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LongMonsterSC : EnemySC
{
    public Slider healthBarSlider;
    private float delay = 2f;
    void Start()
    {
        enemyhp = 10;
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
            healthBarSlider.value -= .1f;
            Debug.Log(enemyhp);
            healthBarSlider.gameObject.SetActive(true);
            delay +=1f;
            StartCoroutine(OffSlider());
        }
    }

    IEnumerator OffSlider()
    {
        yield return new WaitForSeconds(delay);
        healthBarSlider.gameObject.SetActive(false);
    }
}
