using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    Transform[] weapons;
    WeaponInfo[] weaponInfos;
    [HideInInspector]
    public WeaponInfo wpInfo;
    int useSlot = 0;

    float fireTime;
    [HideInInspector]
    public float curMoa;

    ObjectPooling op;
    LayerMask bulletLayer;

    private void Awake()
    {
        op = ObjectPooling.GetInstance();
        op.Initialize(Resources.Load<GameObject>("Prefabs/Weapon/Bullet1"),1000);
        weapons = new Transform[4];
        weaponInfos = new WeaponInfo[4];
        useSlot = 0;
        ChangeWeapon(0, "M4A1");
        ChangeWeapon(1, "MP5");
        ChangeWeapon(2, "Glock");
        ChangeWeapon(3, "Kar98k");
    }

    // Start is called before the first frame update
    void Start()
    {

        bulletLayer = LayerMask.NameToLayer("Bullet" + LayerMask.LayerToName(gameObject.layer));
    }

    // Update is called once per frame
    void Update()
    {
        {
            curMoa = Vector2.Lerp(new Vector2(curMoa, 0), new Vector2(wpInfo.moa, 0), Time.deltaTime/wpInfo.recoveryTime).x;
        }
    }

    public void ChangeWeapon(int slot, string name)
    {
        if (slot < 0 || slot > 3) return;
        
        GameObject go = Resources.Load<GameObject>("Prefabs/Weapon/"+name);
        if (go)
        {
            if (weapons[slot])
                Destroy(weapons[slot]);
                
            weapons[slot] = Instantiate(go, transform).transform;
            weapons[slot].gameObject.layer = gameObject.layer;
             weaponInfos[slot] = go.GetComponent<WeaponInfo>();
            if (useSlot != slot)
                weapons[slot].gameObject.SetActive(false);
            else
                wpInfo = weapons[useSlot].GetComponent<WeaponInfo>();
        }
    }

    public bool UseWeapon(int slot)
    {
        if (slot < 0 || slot > 3 || slot == useSlot) return false;
        if (weapons[slot])
        {
            if (weapons[useSlot])
                weapons[useSlot].gameObject.SetActive(false);
            useSlot = slot;
            weapons[useSlot].gameObject.SetActive(true);
            wpInfo = weapons[useSlot].GetComponent<WeaponInfo>();
            curMoa = wpInfo.moa;
            if (wpInfo.curMagazine <= 0)
                Reload();
            return true;
        }
        return false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dmgBouns">피해량 변동치(n배 : n)</param>
    /// <param name="speedBouns">탄속 변동치(n배 : n) min: 0.01</param>
    /// <param name="fireRate">연사 변동치(n배 : n) min: 0.1</param>
    /// <param name="costMagazine">탄 소모 변동치(n배 : n) min: 1</param>
    public void Shoot(float dmgBouns = 1f, float speedBouns = 1f, float fireRate = 1f, int costMagazine = 1)
    {
        if (dmgBouns < 0) dmgBouns = 0;
        if (speedBouns < 0.01f) speedBouns = 0.01f;
        if (fireRate < 0.1f) fireRate = 0.1f;
        if (costMagazine < 1) costMagazine = 1;

        if (Time.realtimeSinceStartup - fireTime < wpInfo.firerate / fireRate) return;
        if (wpInfo.curMagazine < 1) return;
        if (!weapons[useSlot]) return;
        float z = transform.eulerAngles.z;
        GameObject go = op.Enable("Bullet1");
        Bullet b = go.GetComponent<Bullet>();
        go.layer = bulletLayer;
        b.Shoot(wpInfo.muzzle.position,
            transform.eulerAngles.z
            + Random.Range(-curMoa, curMoa),
            wpInfo.dmgNormal * dmgBouns,
            wpInfo.dmgCritical * dmgBouns,
            wpInfo.bulletSpeed * speedBouns,
            wpInfo.deadTime / speedBouns);
        fireTime = Time.realtimeSinceStartup;
        wpInfo.curMagazine -= costMagazine;
        wpInfo.ShootSound();
        if (wpInfo.curMagazine <= 0)
        {
            wpInfo.curMagazine = 0;
            wpInfo.ClipSound();
            Reload();
        }

        if(wpInfo.moa <= wpInfo.spread)
        {
            curMoa = Vector2.Lerp(new Vector2(curMoa, 0), new Vector2(wpInfo.spread, 0), wpInfo.recoil).x;
            if (curMoa > wpInfo.spread)
                curMoa = wpInfo.spread;
        }
        else
        {
            curMoa = Vector2.Lerp(new Vector2(curMoa, 0), new Vector2(wpInfo.spread, 0), wpInfo.recoil).x;
            if (curMoa < wpInfo.spread)
                curMoa = wpInfo.spread;
        }
    }


    public void Aiming(Vector3 target)
    {
        Vector3 v = target - transform.position;
        float z = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, z);
        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            wpInfo.spriteTf.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            wpInfo.spriteTf.localScale = new Vector3(1, +1, 1);
        }
    }

    public void Reload()
    {
        wpInfo.Reload();
    }
}
