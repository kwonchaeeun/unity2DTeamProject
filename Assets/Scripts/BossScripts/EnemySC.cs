using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySC : MonoBehaviour
{
    public int enemyhp;

    public void EnemyDie()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        Debug.Log(enemyhp);
    }
}
