using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class long3Enemy : MonoBehaviour
{
    public GameObject bullet;
  
    void Start(){
        StartCoroutine(Bullet());
    }

    IEnumerator Bullet(){
        Instantiate(bullet, transform.position+Vector3.right*-4.0f+Vector3.up*-0.65f, transform.rotation);
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(Bullet());
    }

}   
