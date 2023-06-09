using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CErangeOfAttack : MonoBehaviour
{   
    private BoxCollider2D boxCollider;
    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        boxCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(ToggleBoxCollider());
    }

    IEnumerator ToggleBoxCollider()
    {
        while (true)
        {
            boxCollider.enabled = false;
            yield return new WaitForSeconds(2f);
            boxCollider.enabled = true;
            yield return new WaitForSeconds(2f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("player");
            playerController.Hit(DamageType.HP, 1);
        }
    }
}
