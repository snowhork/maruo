using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hukidashi : MonoBehaviour {

    public enum HukidashiState
    {
        not_display,
        flowing,
        finish,
    }

    [SerializeField]
    Image image;
    [SerializeField]
    Text display_text;
    [SerializeField]
    Canvas canvas;

    [SerializeField]
    float close_time = 1.5f;

    [SerializeField]
    float flow_time = 0.2f;

    string[] contents;
    int content_index = -1;
    bool choose_flag;
    bool open_flag;
    HukidashiState state;

    string text = null;
    int text_index;
    float time = 0f;

    public Canvas Canvas { get { return canvas; } }
    public HukidashiState State { get { return state; } }

    Choose choose;

    // Use this for initialization
    void Start () {
        canvas.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        rotation_reset();
        if (state == HukidashiState.not_display || state == HukidashiState.finish)
        {
            return;
        }
        if (text_index == text.Length)
        {
            if (time <= close_time)
            {
                return;
            }
            content_index++;
            if (content_index < contents.Length)
            {
                StartText();
                if(content_index == contents.Length-1 && choose_flag)
                {
                    SetChoose();
                }
                return;
            }

            if(open_flag || choose_flag)
            {
                return;
            }

            if(!choose_flag)
            {
                EndText();
            }
            return;
        }

        if (time < flow_time)
        {
            return;
        }
        text_index++;
        display_text.text = text.Substring(0, text_index);
    }

    public void StartContents(string[] contents, bool choose_flag=false, bool open_flag = false)
    {
        EndText();
        this.contents = contents;
        this.choose_flag = choose_flag;
        this.open_flag = open_flag;
        content_index = 0;
        state = HukidashiState.flowing;
        StartText();
    }

    void StartText()
    {
        canvas.enabled = true;
        time = 0f;
        text_index = 0;
        display_text.text = "";
        text = contents[content_index];
    }
    public void EndText()
    {
        if (choose != null)
        {
            
            choose.EndChoose();
            choose = null;
        }
        canvas.enabled = false;
        state = HukidashiState.finish;
    }
    
    void rotation_reset()
    {
        if (GameManager.instance.camera == null)
        {
            return;
        }
        transform.LookAt(new Vector3(GameManager.instance.camera.transform.position.x, GameManager.instance.CameraRig.transform.position.y + 1.4f, GameManager.instance.camera.transform.position.z));
    }

    void SetChoose()
    {
        choose = new Choose(this);
        choose.SetChoose();
    }

}
