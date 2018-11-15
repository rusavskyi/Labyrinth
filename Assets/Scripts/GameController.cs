using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int CurrentScene = 0;
    public int NextScene = 0;
    public int NumberOfKeysLeft = 10;
    public DoorScript LevelDoors;
    public GameObject DeathPanel;
    public GameObject PausePanel;
    public Text KeysNumber;
    public Slider VolumeControl;

    private float oldVolume = 1;

    // Use this for initialization
    void Start()
    {
        KeysNumber.text = "" + NumberOfKeysLeft;
        DeathPanel.SetActive(false);
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (NumberOfKeysLeft <= 0)
        {
            LevelDoors.OpenDoor();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            PausePanel.SetActive(!PausePanel.activeInHierarchy);
        }

        if (oldVolume != VolumeControl.value)
        {
            AudioListener.volume = VolumeControl.value;
            oldVolume = VolumeControl.value;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(CurrentScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
