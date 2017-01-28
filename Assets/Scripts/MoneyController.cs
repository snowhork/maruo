using UnityEngine;
using System.Collections;

public class MoneyController : MonoBehaviour {
    public static MoneyController instance;
    public int money = 100;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public void get_money(int money)
    {
        this.money += money;
    }
    
}
