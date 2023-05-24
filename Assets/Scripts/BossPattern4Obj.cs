using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern4Obj : MonoBehaviour
{
    // Start is called before the first frame update
    public BoxCollider2D boxCollider2D;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // if (gameObject.activeSelf)
       // {          
            StartCoroutine(Erase());
        //}
    }

    private IEnumerator Erase()
    {
        if(boxCollider2D.enabled == true)
        {
            yield return new WaitForSeconds(2f);
            boxCollider2D.enabled = false;
        }
        yield return new WaitForSeconds(2f);
        if(boxCollider2D.enabled == false)
        {
            yield return new WaitForSeconds(2f);
            boxCollider2D.enabled = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("player");
        }
    }
}
