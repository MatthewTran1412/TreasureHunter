using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    public int numberbuy=0;
    [SerializeField]public Text numberBuy;
    [SerializeField]public Text unitPrice;
    [SerializeField]public Text Total;
    [SerializeField]public Text GoldOwned;
    [SerializeField] public int unitprice;
    [SerializeField] HealthBar HB;
    [SerializeField] PlayerStatus PS;
    [SerializeField] public int total;
    [SerializeField] public GameObject Message;
    [SerializeField] public Text MessageText;

    // Update is called once per frame
    void Update()
    {
        numberBuy.text=(numberbuy).ToString();
        unitPrice.text=(unitprice).ToString();
        total=numberbuy*unitprice;
        Total.text="Total:\t"+total.ToString();
        GoldOwned.text="Owned\t"+PS.currentGold.ToString();
    }
    public void addnumber()
    {
        numberbuy++;
    }
    public void minusnumber()
    {
        numberbuy--;
    }
    public void buyMedkid()
    {
        HB.medkid+=numberbuy;
        if(PS.currentGold>=total)
        {
            PS.currentGold-=total;
            Message.SetActive(true);
            MessageText.text="Success";
        }
        else
        {
            Message.SetActive(true);
            MessageText.text="Not enough Gold";
        }
    }
}
