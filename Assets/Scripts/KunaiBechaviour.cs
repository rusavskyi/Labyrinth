using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiBechaviour : MonoBehaviour
{
    public AudioClip hit;
    
    private Rigidbody2D _kunaiRigidbody2D;

    // Use this for initialization
    void Start()
    {
        _kunaiRigidbody2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        _kunaiRigidbody2D.transform.rotation.SetLookRotation(((Vector2)transform.position) + _kunaiRigidbody2D.velocity);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(hit, transform.position);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
    
}
