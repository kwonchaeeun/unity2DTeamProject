using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySC : MonoBehaviour
{
    public int enemyhp;
    public void EnemyDie()
    {
        gameObject.SetActive(false);
        DropItem();
    }

    public void Hit(int damage)
    {
        enemyhp -= damage;
        
        if (enemyhp <= 0)
        {
            EnemyDie();
        }
        else
        {
            UpdateHealthBar();
        }
    }

    protected virtual void UpdateHealthBar()
    {

    }

    protected virtual void DropItem()
    {

    }
}
