using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public static Timer instance;

    [SerializeField]
    float maxCount = 5f * 60f;

    float count;
    bool isrest = false;
    public bool IsRest { get { return isrest; } }
    float Count { get { return count; } }
    float RestTime { get { return maxCount - count; } }

    [SerializeField]
    GameObject[] houses;

    [SerializeField]
    Text text;
    [SerializeField]
    Transform endPosition;
    [SerializeField]
    Transform housePosition;
    [SerializeField]
    Image image;
    [SerializeField]
    EventEnd eventEnd;

    void Start () {
        count = 0f;

        if(instance == null)
        {
            instance = this;
        }
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        count += Time.deltaTime;
        setText();
        TimerEnd();

	}

    void TimerEnd()
    {
        if(RestTime > 0)
        {
            return;
        }
        if(isrest)
        {
            return;
        }
        isrest = true;
        StartCoroutine(EndMotion());
        
    }

    void setText()
    {
        text.text = displayTime();
    }

    string displayTime()
    {
        int minute = (int)RestTime / 60;
        int second = (int)(RestTime - minute * 60);
        if (RestTime < 0)
        {
            minute = 0;
            second = 0;
        }

        return minute + ":" + second.ToString("00");
    }

    IEnumerator EndMotion()
    {
        SoundController.instance.Play("AlermSound");
        for (float a = 0; a <= 1; a+= 0.01f)
        {
            image.color = new Color(0, 0, 0, a);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        GameManager.instance.CameraRig.transform.position = endPosition.position;
        
        seisan();
        set_house();
        eventEnd.end();

    
        image.enabled = false;

    }

    void seisan()
    {

        MoneyController.instance.money += GameManager.instance.inventory.SumPrice();
    }

    void set_house()
    {
        GameObject h;
        if (MoneyController.instance.money >= 8000)
        {
            h = Instantiate(houses[2]);
            h.transform.position = housePosition.position;
            return;
        }
        if (MoneyController.instance.money >= 3000)
        {
            h = Instantiate(houses[1]);
            h.transform.position = housePosition.position;
            return;
        }
        h = Instantiate(houses[0]);
        h.transform.position = housePosition.position;
        return;


    }
}
