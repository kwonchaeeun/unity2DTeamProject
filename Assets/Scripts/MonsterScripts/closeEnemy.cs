using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeEnemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float distance = 5f;
    [SerializeField] private GameObject enemyRangeAttack;
    [SerializeField] private GameObject lObject; 
    [SerializeField] private GameObject rObject;

    private Rigidbody2D rb;
    private Transform target;
    private int nextMove;
    private bool follow = false;
    private int randomIndex;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        Invoke("Think", 5);
        randomIndex = Random.Range(0, 2);
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < 15f)
        {
            follow = true;
        }
        else
            follow = false;

        if (follow == false)
            BasicMove();
        else
        {
            if (randomIndex == 0)
            {
                Pattern1();
            }
            else if (randomIndex == 1)
            {
                Pattern2();
            }
        }
    }

    void BasicMove()
    {
        rb.velocity = new Vector2(nextMove, rb.velocity.y);
    }

    void Pattern1()
    {
        if (Vector2.Distance(transform.position, target.position) > distance)
        {
            enemyRangeAttack.SetActive(true);

            if (transform.position.x < target.position.x)
            {
                transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
            }
            else
            {
                transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
            }
        }
        if (Vector2.Distance(transform.position, target.position) > 8)
        {
            enemyRangeAttack.SetActive(false);
        }
    }


    void Pattern2()
    {
        if (Vector2.Distance(transform.position, target.position) > distance)
        {
            if (target.position.x > transform.position.x)
            {
                rObject.SetActive(true);
                lObject.SetActive(false);
            }
            else if (target.position.x < transform.position.x)
            {
                lObject.SetActive(true);
                rObject.SetActive(false);
            }
            else
            {
                lObject.SetActive(false);
                rObject.SetActive(false);
            }

            if (transform.position.x < target.position.x)
            {
                transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
            }
            else
            {
                transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
            }
        }
        if (Vector2.Distance(transform.position, target.position) > 8)
        {
            lObject.SetActive(false);
            rObject.SetActive(false);
        }
    }

    // private void OnTriggerStay2D(Collider2D collision)
    // {
    //      if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
    //     {
    //         follow = true;
    //         Debug.Log("hh");
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
    //     {
    //         follow = false;
    //     }
        
    // }

    void Think()
    {
        nextMove = Random.Range(-1, 2);

        Invoke("Think", 2);
    }
}
