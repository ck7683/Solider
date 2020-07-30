using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    UILabel label;
    // Start is called before the first frame update
    void Start()
    {
        label = transform.GetComponent<UILabel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RenewScore(int score)
    {
        label.text = ""+score;
    }
}
