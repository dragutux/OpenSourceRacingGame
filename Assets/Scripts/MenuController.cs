using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public UnityEvent MenuON;
    public UnityEvent MenuOFF;

    public UnityEvent EscapeMenuOn;
    public UnityEvent EscapeMenuOff;

    // first panel example, could be better
    public UnityEvent SingleRacePanelOn;
    public UnityEvent SingleRacePanelOff;

    public UnityEvent testPanelOn;
    public UnityEvent testPanelOff;
    // controls escape menu and/or ui elements
    public bool isInGame;

    // always start as "off"
    private bool escapeMenu;

    private MenuController instance;

    void Awake()
    {
        MenuController SceneInstance = FindObjectOfType<MenuController>();

        if(instance != null && instance != this || SceneInstance != null && SceneInstance != this)
        {
            Debug.Log("Duplicate instance, removing it");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(sceneName);
    }

    public void OnMenuEntryClick(string entry)
    {
        // use mask/layer or so ? or other methods ?
        if(entry == "0x0")
        {
            SingleRacePanelOn.Invoke();
        }
        // could be else if or a switch with calls
        else
        {
            SingleRacePanelOff.Invoke();
        }
    }

    public void OnPanelEntryClick(string sceneLoad)
    {
        if(sceneLoad == "LoadScene")
        {
            isInGame = true;
            escapeMenu = false;
            SingleRacePanelOff.Invoke();
            MenuOFF.Invoke();
            ChangeScene(sceneLoad);
        }
        else if(sceneLoad == "MenuScene")
        {
            isInGame = false;
            escapeMenu = false;
            EscapeMenuOff.Invoke();
            MenuON.Invoke();
            ChangeScene(sceneLoad);
        }
    }

    private void Update()
    {
        if(isInGame)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                escapeMenu = !escapeMenu;
                Debug.Log("Escape menu is " + escapeMenu);
            }

            switch(escapeMenu)
            {
                case true:
                    EscapeMenuOn.Invoke(); 
                    break;
                case false:
                    EscapeMenuOff.Invoke();
                    break;
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
