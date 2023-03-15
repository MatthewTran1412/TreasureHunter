using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    public bool GameIsPaused = false;
    public GameObject PauseMenu;
    public GameObject MainOption;
    public GameObject VolumeOption;
    [SerializeField] Attack Atk;
    [SerializeField] MainMenu Mainmenu;
    // Update is called once per frame
    void Start()
    {
        Atk=GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) )
        {
            if(Atk.CloseAll==0)
            {
                if(GameIsPaused==true)
                {
                    BacktoGame();
                }
                else if(GameIsPaused==false)
                {
                    Pause();
                }
            }
        }
    }
    public void BacktoGame()
    {
        PauseMenu.SetActive(false);
        VolumeOption.SetActive(false);
        Time.timeScale=1f;
        GameIsPaused=false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible=false;
        Atk.ManualActive=false;
    }
    public void Pause()
    {
        PauseMenu.SetActive(true);
        MainOption.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused =true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible=true;
    }
    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying=false;
    #else
        Application.Quit();
    #endif
    }
    public void OpenVolumeManager()
    {
        MainOption.SetActive(false);
        VolumeOption.SetActive(true);
    }
    IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(3);
        Mainmenu.ImageMenuanim.SetBool("IsCompleted",false);
        Mainmenu.MenuImage.SetActive(true);
        Mainmenu.StartBtn.SetActive(true);
        Mainmenu.MenuTitle.SetActive(true);
    }
}
