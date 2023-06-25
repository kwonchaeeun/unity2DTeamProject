using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    private PlayerController playerController;

    float launchSpeed = 10f;
    public GameObject BulletR;
    public GameObject BulletL;
    public GameObject BulletUp;
    public GameObject BulletDown;
    
    void Start()
    {
        Invoke("DestroyBullet",10);
    }
    void Update()
    {
        BulletPos();
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    } 

    void BulletPos()
    {
        if(this.gameObject == BulletR)
        {
            transform.Translate(transform.right * launchSpeed * Time.deltaTime);
        }
        if(this.gameObject == BulletL)
        {
            transform.Translate(transform.right * -launchSpeed * Time.deltaTime);
        }
        if(this.gameObject == BulletDown)
        {
            transform.Translate(transform.up * -launchSpeed * Time.deltaTime);
        }
        if(this.gameObject == BulletUp)
        {
            transform.Translate(transform.up * launchSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("player");
            playerController.Hit(DamageType.INTELLECTUALITY, 1);
        }
    }
}
