using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern3Obj : MonoBehaviour
{
    private PlayerController playerController;
    private Transform target;
    public GameObject leftObj;
    public GameObject rightObj;

    private float moveSpeed = 5f; 
    private float moveDistance = 15f; 
    private bool isMovingLeft = true; 
    public float speed = 0.02f;
    private Vector3 initialPosition; 

    GameObject BossObj; 

    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        target = GameObject.FindGameObjectWithTag("BossEnemy").GetComponent<Transform>();
        initialPosition = transform.position;
        BossObj = GameObject.Find("BossEnemy");
    }

 void Update()
    {
        if(this.gameObject == leftObj)
        {
            LeftObj();
        }
        if(this.gameObject == rightObj)
        {
            RightObj();
        }

    }
    
    void LeftObj()
    {
        if (gameObject.activeSelf)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance <= 22f)
            {
                if (isMovingLeft)
                {
                    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

                    if (transform.position.x - initialPosition.x <= -moveDistance)
                    {
                        isMovingLeft = false;
                    }
                }
                else
                {
                    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

                    if (transform.position.x - initialPosition.x >= moveDistance/3)
                    {
                        transform.position = target.position;                        
                        moveSpeed = 0;
                        StartCoroutine(MoveAndRestL());
                    }
                }
            }
        }
    }
    private IEnumerator MoveAndRestL()
    {       
        yield return new WaitForSeconds(2f);
        moveSpeed = 5f;
        isMovingLeft = true;
        BossSc bossObj = BossObj.GetComponent<BossSc>();
        bossObj.randomIndex = Random.Range(0, 4);
        gameObject.SetActive(false);
    }

    void RightObj()
    {
        if (gameObject.activeSelf)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance <= 20f)
            {
                if (isMovingLeft)
                {
                    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

                    if (transform.position.x - initialPosition.x >= moveDistance)
                    {
                        isMovingLeft = false;
                    }
                }
                else
                {
                    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

                    if (transform.position.x - initialPosition.x <= -moveDistance / 3)
                    {
                        transform.position = target.position;
                        moveSpeed = 0;
                        StartCoroutine(MoveAndRestR());
                    }
                }
            }
        }
    }
    private IEnumerator MoveAndRestR()
    {
        yield return new WaitForSeconds(2f);
        moveSpeed = 5f;
        isMovingLeft = true;
        BossSc bossObj = BossObj.GetComponent<BossSc>();
        bossObj.randomIndex = Random.Range(0, 4);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("player");
            playerController.Hit(DamageType.INTELLECTUALITY, 10);
        }
    }
}
