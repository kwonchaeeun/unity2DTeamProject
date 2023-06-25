using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossSc : MonoBehaviour
{
    private PlayerController playerController;
    public int randomIndex;

    private float speed = 5f;
    private Transform player;
    private bool isChasing = false;
    private bool isMovingRight = true;
    bool pattern1 = false;
    public GameObject rightObject2;
    public GameObject leftObject2;

    public GameObject rightObject3;
    public GameObject leftObject3;
    public GameObject rightObject4;
    public GameObject leftObject4;

    private Animator animator;
    void Start()
    {
<<<<<<< Updated upstream
        randomIndex = Random.Range(0, 4);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = GameObject.FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (randomIndex == 0)
        {
            Pattern1();
        }
        else if (randomIndex == 1)
        {
            Pattern2();
        }
        else if (randomIndex == 2)
        {
            Pattern3();
        }
        else if (randomIndex == 3)
        {
            Pattern4();
        }
    }

    void Pattern1()
    {
        pattern1 = true;
        animator.SetTrigger("pattern2");
        if (player != null && !isChasing)
        {
            if (Vector2.Distance(transform.position, player.position) < 20f)
            {
                isChasing = true;

                isMovingRight = transform.position.x < player.position.x;
            }
        }

        if (isChasing)
        {
            if (isMovingRight)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (pattern1 == true)
        {
            if (collision.collider.CompareTag("Wall"))
            {
                isChasing = false;
                speed = 0f;
                StartCoroutine(ResumeChasingAfterDelay(3f));
                Debug.Log("wall");
            }

            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.Log("player");
                playerController.Hit(DamageType.HP, 1);
            }
        }
    }


    IEnumerator ResumeChasingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        speed = 5f;

        randomIndex = Random.Range(0, 4);
        pattern1 = false;
    }

    void Pattern2()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (!isChasing && 10 < distance && distance < 20)
            {
                isChasing = true;
                isMovingRight = transform.position.x < player.position.x;
            }

            if (isChasing)
            {
                if (isMovingRight)
                {
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                }
            }
            if (distance <= 10)
            {
                isChasing = false;

                Vector2 playerDirection = player.position - transform.position;

                if (playerDirection.x > 0)
                {
                    rightObject2.SetActive(true);
                    leftObject2.SetActive(false);
                }
                else
                {
                    leftObject2.SetActive(true);
                    rightObject2.SetActive(false);
                }
            }
        }
    }
    void Pattern3()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.position) < 20)
            {
                Vector2 playerDirection = player.position - transform.position;

                if (playerDirection.x > 0)
                {
                    rightObject3.SetActive(true);
                    leftObject3.SetActive(false);
                }
                else
                {
                    leftObject3.SetActive(true);
                    rightObject3.SetActive(false);

                }
            }
        }
    }
    void Pattern4()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.position) < 20)
            {
                Vector2 playerDirection = player.position - transform.position;

                if (playerDirection.x > 0)
                {
                    rightObject4.SetActive(true);
                    leftObject4.SetActive(false);
                }
                else
                {
                    leftObject4.SetActive(true);
                    rightObject4.SetActive(false);

                }
            }
        }
    }
}