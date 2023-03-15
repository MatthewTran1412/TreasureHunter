using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ChooseVideotoPlay : MonoBehaviour
{
    public VideoClip[] videoclips;
    VideoPlayer VP;
    [SerializeField]MainMenu Menu;
    [SerializeField]AllwayShow allway;
    [SerializeField]public GameObject Temp;
    [SerializeField]Attack Atk;
    [SerializeField]GameObject SkipText;
    [SerializeField]GameObject UserMAnual;
    int index=0;
    public bool checkvideo;
    bool canSkip;
    void Start()
    {
        checkvideo=false;
        VP = GetComponent<VideoPlayer>();
        VP.clip = videoclips[index];
        VP.loopPointReached += CheckOver;
        Atk.cannotatk=true;
        Atk.CanMove=false;
        SkipText.SetActive(false);
        StartCoroutine(ShowSkip());
        canSkip=false;
    
    }
    // Update is called once per frame
    void Update()
    {
        //if(Menu.StartupIsDone==true && allway.LoadIsDone==true)
        //{
            VP.Play();
        //}
        if(Input.GetKeyDown(KeyCode.N))
        {
            if(canSkip)
                SkipVideo();
        }
    }
    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        int temp=videoclips.Length-1;
        if(index<temp)
            index++;
        VP.clip=videoclips[index];
        VP.loopPointReached += PauseAndClose;
        StartCoroutine(ShowSkip());
        if(index>temp)
            canSkip=false;
        SkipText.SetActive(false);
        if(checkvideo==false)
        {
            UserMAnual.SetActive(true);
            Time.timeScale=0f;
            Atk.islockmouse=false;
        }
        else if(checkvideo==true)
            Atk.islockmouse=true;
    }
    void PauseAndClose(VideoPlayer vp)
    {
        VP.Pause();
        allway.PlayVideoClip.SetActive(false);
        allway.VideoPlayer.SetActive(false);
        Temp.SetActive(false);
        Atk.cannotatk=false;
        Atk.CanMove=true;
    }
    public void PlayOnetimeVideo(VideoClip video)
    {
        Atk.cannotatk=true;
        Atk.CanMove=false;
        VP.clip=video;
        VP.Play();
        VP.loopPointReached += PauseAndClose;
    }
    IEnumerator ShowSkip()
    {
        yield return new WaitForSeconds(2);
        SkipText.SetActive(true);
        canSkip=true;
    }
    void SkipVideo()
    {
        int temp=videoclips.Length-1;
        if(index<temp)
        {
            index++;
            VP.clip=videoclips[index];
            VP.loopPointReached += PauseAndClose;
            StartCoroutine(ShowSkip());
        }
        else
        {
            PauseAndClose(VP);
            SkipText.SetActive(false);
            UserMAnual.SetActive(true);
            Atk.islockmouse=false;
            canSkip=false;
            Time.timeScale=0f;
        }
    }
}
