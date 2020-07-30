using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectedMenu(MenuKinds kind)
    {
        switch (kind)
        {
            case MenuKinds.Single:
                SceneManager.LoadScene("Ingame", LoadSceneMode.Single);
                break;
        }
    }
}
