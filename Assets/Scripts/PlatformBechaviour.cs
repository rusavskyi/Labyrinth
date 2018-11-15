using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBechaviour : MonoBehaviour
{
    public float MoveSpeed = 1f;
    public bool CanMove = false;
    public bool MoveInCircle = false;
    public Vector3[] MovePoints;

    private bool MoveForward = true;
    private int i = 0;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(MoveCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            if (CanMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, MovePoints[i], MoveSpeed * Time.deltaTime);
                if (transform.position == MovePoints[i])
                {
                    i += MoveForward ? 1 : -1;
                }

                if (i >= MovePoints.Length)
                {
                    if (MoveInCircle)
                    {
                        i = 0;
                    }
                    else
                    {
                        i = MovePoints.Length - 1;
                        MoveForward = !MoveForward;
                    }
                }
                else if (i < 0)
                {
                    i = 0;
                    MoveForward = !MoveForward;
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
