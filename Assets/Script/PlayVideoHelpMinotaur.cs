using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class PlayVideoHelpMinotaur : MonoBehaviour
{
    [SerializeField]ChooseVideotoPlay CP;
    [SerializeField]AllwayShow allway;
    [SerializeField]VideoClip Helpminotaur;
    [SerializeField]GameObject[] War;
    [SerializeField]QuestManager QM;
    bool check;
    void Start()
    {
      check=true;
        for(int i=0;i<War.Length;i++)
        {
          War[i].SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player" )
        {
          if(check==true&&QM.TalkWithSpecial==true)
          {
            allway.PlayVideoClip.SetActive(true);
            allway.VideoPlayer.SetActive(true);
            check=false;
            CP.checkvideo=true;
            CP.Temp.SetActive(true);
            CP.PlayOnetimeVideo(Helpminotaur);
            for(int i=0;i<War.Length;i++)
            {
              War[i].SetActive(true);
            }
          }
        }
    }
}
