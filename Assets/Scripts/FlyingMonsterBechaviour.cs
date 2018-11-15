using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonsterBechaviour : MonoBehaviour
{
    public float FlySpeed = 1f;
    public float Range = 10f;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SearchAndDestroy());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SearchAndDestroy()
    {
        bool lookRight = true;
        while (true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Range);
            foreach (Collider2D c in colliders)
            {
                if (c.gameObject.tag == "Player")
                {
                    transform.position = Vector3.MoveTowards(transform.position, c.transform.position, FlySpeed * Time.deltaTime);
                    if (c.transform.position.x < transform.position.x && lookRight ||
                        c.transform.position.x > transform.position.x && !lookRight)
                    {
                        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
                        lookRight = !lookRight;
                    }
                    break;
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
