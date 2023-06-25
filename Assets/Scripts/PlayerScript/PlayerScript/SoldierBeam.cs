using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBeam : MonoBehaviour
{
    private Collider2D collider;
    // Update is called once per frame
    private void Start()
    {
        this.collider = this.GetComponent<BoxCollider2D>();
        StartCoroutine("hitBox");
        StartCoroutine("objectDestroy");

    }
    IEnumerator objectDestroy()
    {
        yield return new WaitForSeconds(0.75f);
        Destroy(this.gameObject);
    }

    IEnumerator hitBox()
    {
        yield return new WaitForSeconds(0.3f);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(this.transform.position + new Vector3(this.transform.localScale.x * collider.offset.x, 0.0f, 0.0f), collider.bounds.size, 0.0f, Vector2.up, 0.0f, (int)Layer.Monster);
        foreach (RaycastHit2D hit in hits)
        {
            hit.collider.GetComponent<Monster>().Hit();
        }
        StopCoroutine("hitBox");
    }
}
