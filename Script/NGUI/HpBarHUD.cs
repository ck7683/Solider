using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarHUD : MonoBehaviour
{
    private UISlider slider;
    [SerializeField]
    private UILabel label;
    // Start is called before the first frame update
    void Start()
    {
        slider = transform.GetComponent<UISlider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSlide(float value)
    {
        if (value > 1) value = 1;
        if (value < 0) value = 0;
        slider.value = value;
        label.text = ((int)(value * 100)) + " %";
    }
}
