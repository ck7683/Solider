using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector]
    public Transform playerTransform;
    PlayerUnit playerUnit;

    [SerializeField]
    private Camera camera;
    
    [SerializeField]
    private Transform selectWeapon;
    private SelectWeaponHUD selectWeaponHUD;

    [SerializeField]
    private Transform magazineGui;
    private MagazineHUD magazineHUD;

    [SerializeField]
    private Transform hpBar;
    private HpBarHUD hpBarHUD;

    [SerializeField]
    Transform defeatGO;
    Defeat defeat;


    // Start is called before the first frame update
    void Start()
    {
        defeat = defeatGO.GetComponent<Defeat>();
        playerTransform = transform.Find("Player");
        playerUnit = playerTransform.GetComponent<PlayerUnit>();
        if (selectWeapon != null)
        {
            selectWeaponHUD = selectWeapon.GetComponent<SelectWeaponHUD>();
        }
        if (magazineGui != null)
        {
            magazineHUD = magazineGui.Find("BackGround").Find("Label").GetComponent<MagazineHUD>();
        }
        hpBarHUD = hpBar.GetComponent<HpBarHUD>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerUnit) return;
        if (camera != null)
        {
            Vector3 v = camera.transform.position;
            v.x = playerUnit.transform.position.x;
            camera.transform.position = v;
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            SelectWeapon(0);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            SelectWeapon(1);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            SelectWeapon(2);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            SelectWeapon(3);
        }

        if (Input.GetKey(KeyCode.A))
        {
            playerUnit.Move(new Vector2(-2, 0));
           // playerUnit.ani.SetBool("isMoving", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerUnit.Move(new Vector2(+2, 0));
            //playerUnit.ani.SetBool("isMoving", true);
        }
        else
        {
            playerUnit.Move(new Vector2(0, 0));
            //playerUnit.ani.SetBool("isMoving", false);
        }

        if (Input.GetMouseButton(0))
        {
            playerUnit.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            playerUnit.Reload();
        }

        MagazineHUD_Change();
        playerUnit.Aiming(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    void MagazineHUD_Change()
    {
        if (playerUnit.weapon.wpInfo && magazineHUD)
            magazineHUD.Change(
                playerUnit.weapon.wpInfo.curMagazine,
                playerUnit.weapon.wpInfo.magazine
                );
    }

    public bool SelectWeapon(int slot)
    {
        if (selectWeaponHUD && playerUnit.UseWeapon(slot))
        {
            selectWeaponHUD.SelectedWeapon(slot);
            return true;
        }
        return false;
    }

    public void RenewHpBar(float value)
    {
        hpBarHUD.SetSlide(value);
    }

    public void Dead()
    {
        defeat.RunDefeat();
        playerUnit = null;
    }
}
