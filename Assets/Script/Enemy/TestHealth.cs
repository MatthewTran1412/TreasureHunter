using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHealth : MonoBehaviour
{
    [SerializeField] public GameObject damagetextPrefabs;
    [SerializeField] private int HP;


    // Update is called once per frame
    public void TakeDMG(int damageAmount)
    {
        HP -= damageAmount;
        if(damagetextPrefabs)
            ShowDamageText(damageAmount);
    }
    void ShowDamageText(int damageAmount)
    {
        var go = Instantiate(damagetextPrefabs,transform.position,Quaternion.identity,transform);
        go.GetComponent<TextMesh>().text=damageAmount.ToString();
    }
}
