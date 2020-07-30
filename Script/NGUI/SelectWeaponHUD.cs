using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeaponHUD : MonoBehaviour
{
    int curWeapon;
    Transform[] child;
    // Start is called before the first frame update
    void Start()
    {
        child = new Transform[4];
        child[0] = transform.Find("SelectSlot1");
        child[1] = transform.Find("SelectSlot2");
        child[2] = transform.Find("SelectSlot3");
        child[3] = transform.Find("SelectSlot4");

        curWeapon = 0;

        for (int i = 1; i < 4; i++)
        {
            Color c = child[i].GetComponent<UISprite>().color;
            c.a = 0.01f;
            child[i].GetComponent<UISprite>().color = c;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectedWeapon(int slot)
    {
        if(curWeapon != slot)
        {
            Color c;
            for (int i = 0; i < 4; i++)
            {
                c = child[i].GetComponent<UISprite>().color;
                c.a = 0.01f;
                child[i].GetComponent<UISprite>().color = c;
            }
            c = child[slot].GetComponent<UISprite>().color;
            c.a = 1;
            child[slot].GetComponent<UISprite>().color = c;
            curWeapon = slot;
        }
    }
}
