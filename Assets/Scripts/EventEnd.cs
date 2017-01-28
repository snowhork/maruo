using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EventEnd : MonoBehaviour {

    [SerializeField]
    Hukidashi hukidashi;


    void Start()
    {
        hukidashi = GetComponentInChildren<Hukidashi>();
    }

    void Update()
    {
        if (hukidashi.State == Hukidashi.HukidashiState.flowing)
        {
            if (GameManager.instance.left_controller.warpflag)
            {
                hukidashi.EndText();
            }
        }
    }

    public void end()
    {

        hukidashi.enabled = true;
        hukidashi.StartContents(set_contents(), open_flag: true);
        StartCoroutine(sceneload());
    }

    string[] set_contents()
    {
        string[] contents = new string[1]
        {
            "この家は" + MoneyController.instance.money + "$で建てることができた家だよ。お疲れ様。"
        };
        return contents;
    }

    IEnumerator sceneload()
    {
        yield return new WaitForSeconds(60f);
        SceneManager.LoadScene("main");
    }
}
