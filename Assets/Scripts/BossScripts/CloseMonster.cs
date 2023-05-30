using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMonster : EnemySC
{
    // Start is called before the first frame update
    void Start()
    {
        enemyhp = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyhp == 0){
            EnemyDie();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            enemyhp -=1;
            Debug.Log(enemyhp);
        }
    }
}
