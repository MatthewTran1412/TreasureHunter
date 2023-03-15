using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLookAtCam : MonoBehaviour
{
    public Camera maincamera;
    public Vector3 newRotation;

    // Update is called once per frame
    void Update()
    {
        newRotation = Quaternion.LookRotation(gameObject.transform.position-maincamera.transform.position).eulerAngles;
        transform.rotation=Quaternion.Euler(newRotation);
    }
}
