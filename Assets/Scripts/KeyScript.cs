﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameController GameController;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GameController.NumberOfKeysLeft--;
            GameController.KeysNumber.text = "" + GameController.NumberOfKeysLeft;
            Destroy(gameObject);
        }
    }
}