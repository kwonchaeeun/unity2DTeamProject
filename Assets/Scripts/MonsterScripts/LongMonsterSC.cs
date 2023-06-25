using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LongMonsterSC : EnemySC
{
    public GameObject itemPrefab;
    public Slider healthBarSlider;
    private float delay = 2f;

    void Start()
    {
        enemyhp = 8;
        UpdateHealthBar();
    }

    protected override void DropItem()
    {
        GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        item.transform.position = transform.position;
    }

    protected override void UpdateHealthBar()
    {
        float healthRatio = (float)enemyhp / 8f; 
        healthBarSlider.value = healthRatio; 
        healthBarSlider.gameObject.SetActive(true);
        delay += 1f;
        StartCoroutine(OffSlider());
    }

    IEnumerator OffSlider()
    {
        yield return new WaitForSeconds(delay);
        healthBarSlider.gameObject.SetActive(false);
    }
}
