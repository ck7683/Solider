using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuKinds {
    Single = 0,
    etc = 1,
    etc2 = 2,
    etc3 = 3
}
public class MenuInfo : MonoBehaviour 
{
    public MenuKinds info;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void OnClick()
    {

        transform.parent.GetComponent<Menu>().SelectedMenu(info);
    }
}
