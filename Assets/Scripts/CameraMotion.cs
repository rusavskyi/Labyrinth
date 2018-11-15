using System.Collections;
using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    public Transform PlayerTransform;
    public Transform TopLeftBorder;
    public Transform BottomRightBorder;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(MoveToPlayer());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator MoveToPlayer()
    {
        while (true)
        {
            if (PlayerTransform.position.x - 25f > TopLeftBorder.position.x &&
                PlayerTransform.position.x + 25f < BottomRightBorder.position.x &&
                PlayerTransform.position.y + 14f < TopLeftBorder.position.y &&
                PlayerTransform.position.y - 14f > BottomRightBorder.position.y)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, transform.position.z),
                    Vector3.Distance(transform.position, PlayerTransform.position) * Time.deltaTime);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
