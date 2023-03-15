using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInMap : MonoBehaviour
{
    public CharacterController Cc;
    [SerializeField]public Attack atk;
    [SerializeField]MainMenu Mainmenu;
    [SerializeField]AllwayShow allway;
    [SerializeField]public float speed;
    [SerializeField]HealthBar HB;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(atk.CanMove&&allway.Istransform==false&&Mainmenu.StartGame==false&&HB.health>0&&DialogManager.GetInstance().dialoguePlaying==false)
            Movement();
    }
    void Movement(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal,vertical,0).normalized;
        Cc.Move(direction* speed * Time.deltaTime);
    }
}
