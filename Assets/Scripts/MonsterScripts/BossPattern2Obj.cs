using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern2Obj : MonoBehaviour
{   private Transform target;
    private float riseSpeed = 2.0f;
    GameObject BossObj; 
    bool isMove = true;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("BossEnemy").GetComponent<Transform>();
        BossObj = GameObject.Find("BossEnemy");
    }
    void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        
        if (distance <= 6f)
        {
            if(isMove == true)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + (riseSpeed * Time.deltaTime));               
            }
        }
        else
        {     
            StartCoroutine(RiseAndRest(1.5f));
            transform.position = target.position;
        }
    } 

    private IEnumerator RiseAndRest(float duration)
    {
        isMove = false;
        yield return new WaitForSeconds(duration);
        isMove = true;
        BossSc bossObj = BossObj.GetComponent<BossSc>();
        bossObj.randomIndex = Random.Range(0, 4);
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("player");
        }
    }
}
