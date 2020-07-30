using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectInformation : MonoBehaviour
{
    private SelectWeaponHUD parent;
    [SerializeField]
    private PlayerManager playerManager;
    public int slot;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.GetComponent<SelectWeaponHUD>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick()
    {
        if(playerManager.SelectWeapon(slot))
            parent.SelectedWeapon(slot);
    }
}
