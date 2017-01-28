using UnityEngine;
using System.Collections;

public class Choose
{
    public Choose(Hukidashi hukidashi)
    {
        this.hukidashi = hukidashi;
    }
    Hukidashi hukidashi;

    GameObject yes;
    GameObject no;

    public void SetChoose()
    {
        var yes_source = Resources.Load<GameObject>("UI/Yes");
        yes = GameObject.Instantiate(yes_source);
        yes.transform.rotation = hukidashi.Canvas.transform.rotation;
        yes.transform.parent = hukidashi.Canvas.transform;
        yes.transform.localPosition = yes_source.transform.position;

        var no_source = Resources.Load<GameObject>("UI/No");
        no = GameObject.Instantiate(no_source);
        no.transform.rotation = hukidashi.Canvas.transform.rotation;
        no.transform.parent = hukidashi.Canvas.transform;
        no.transform.localPosition = no_source.transform.position;

    }
    public void EndChoose()
    {
        GameObject.Destroy(yes);
        GameObject.Destroy(no);
    }
}
