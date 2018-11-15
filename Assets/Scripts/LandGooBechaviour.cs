using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandGooBechaviour : MonoBehaviour
{
    public float MoveSpeed = 1f;
    public bool CanMove = true;
    public bool MoveForward = true;
    public Vector3[] MovePoints;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Move()
    {
        int i = 0;
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, MovePoints[i], MoveSpeed * Time.deltaTime);
            if (transform.position == MovePoints[i])
            {
                i += MoveForward ? 1 : -1;
            }

            if (i >= MovePoints.Length)
            {
                i = MovePoints.Length - 1;
                MoveForward = !MoveForward;
            }
            else if (i < 0)
            {
                i = 0;
                MoveForward = !MoveForward;
            }

            transform.localScale = MoveForward ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
            yield return new WaitForSeconds(0.01f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Do demage
        }
    }
}
