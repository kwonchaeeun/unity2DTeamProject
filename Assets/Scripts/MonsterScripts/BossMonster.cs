using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMonster : EnemySC
{
    public GameObject itemPrefab;
    public Slider healthBarSlider;
    private float delay = 2f;

    void Start()
    {
        enemyhp = 1;
        UpdateHealthBar();
    }

    protected override void DropItem()
    {
        GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        item.transform.position = transform.position;
        item.GetComponent<Money>().Initialize(100);
    }

    protected override void UpdateHealthBar()
    {
        float healthRatio = (float)enemyhp / 30f; 
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
