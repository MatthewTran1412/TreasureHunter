using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    public Vector2 turn;
    private Transform parent;
    [SerializeField]Attack Atk;
    
    private void Start()
    {
        parent = transform.parent;
    }
    private void Update()
    {
        Rotate();
    }
    private void Rotate()
    {
        if(Atk.OPMenu.GameIsPaused==false && Atk.allway.Istransform==false&&Atk.Mainmenu.StartGame==false&&DialogManager.GetInstance().dialoguePlaying==false)
        {
            turn.x += Input.GetAxis("Mouse X");
            turn.y += Input.GetAxis("Mouse Y");

            //if(Atk.IsShootingMode==true)
            parent.localRotation = Quaternion.Euler(-turn.y,turn.x,0);
        }
    }
}
