using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineHUD : MonoBehaviour
{
    UILabel label;
    string defaultContent;

    // Start is called before the first frame update
    void Start()
    {
        defaultContent = "-";
        label = transform.GetComponent<UILabel>();
        label.text = defaultContent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change(int cur, int max)
    {
        if (max == 0)
            label.text = defaultContent;
        else
            label.text = cur + " / " + max;
    }
}
