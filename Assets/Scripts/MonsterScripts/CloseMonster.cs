using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseMonster : EnemySC
{
    public GameObject itemPrefab;
    public Slider healthBarSlider;
    private float delay = 2f;
    private Animator animator;
    void Start()
    {
        enemyhp = 200;
        UpdateHealthBar();
        animator = GetComponent<Animator>();
    }

    protected override void DropItem()
    {
        GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        item.transform.position = transform.position;
        item.GetComponent<Money>().Initialize(100);
        //animator.Play("CEdead");
    }

    protected override void UpdateHealthBar()
    {
        //animator.Play("CEhurt");
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
