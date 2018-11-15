using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public bool IsOpen = false;
    public GameObject OpenDoorsPrefab;
    public GameObject CloseDoorsPrefab;
    public GameObject Door;
    public GameController GameController;

    // Use this for initialization
    void Start()
    {
        Instantiate(CloseDoorsPrefab, Door.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && IsOpen)
        {
            SceneManager.LoadScene(GameController.NextScene);
        }
    }

    public void OpenDoor()
    {
        if (!IsOpen)
        {
            IsOpen = true;
            Destroy(Door.transform.GetChild(0).gameObject);
            Instantiate(OpenDoorsPrefab, Door.transform);
        }
    }
}
