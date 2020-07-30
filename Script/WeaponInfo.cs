using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour {

    [Header("피해량")]

    [Min(0f)]
    public float dmgNormal;

    [Tooltip("약점 피해량")][Min(0f)]
    public float dmgCritical;

    [Header("명중률")]

    [Range(0f,30f)]
    public float moa;
    [Tooltip("탄퍼짐")] [Range(0f, 30f)]
    public float spread;
    [Range(0f, 1f)] [Tooltip("퍼센트")]
    public float recoil;
    [Range(0f, 10f)]
    public float recoveryTime;

    [Space(10)]

    [Min(0f)]
    public float bulletSpeed;

    [Min(0)][Tooltip("탄창수")][Range(0, 1000)]
    public int magazine;

    [Min(0.001f)][Tooltip("연사속도")]
    public float firerate;

    [Min(0f)][Tooltip("재장전 시간")]
    public float reload;

    [Min(0f)]
    public float deadTime;

    [HideInInspector]
    public int curMagazine;

    [HideInInspector]
    public Transform muzzle;



    [HideInInspector]
    public SpriteRenderer sprite;
    [HideInInspector]
    public Transform spriteTf;


    [HideInInspector]
    public AudioSource audio;
    public AudioClip audioFireClip;
    public AudioClip audioClipClip;
    public AudioClip audioReloadClip;

    private float clipReloadVolume;

    // Start is called before the first frame update
    void Awake()
    {
        clipReloadVolume = 0;
        spriteTf = transform.Find("Image");
        sprite = spriteTf.GetComponent<SpriteRenderer>();
        muzzle = spriteTf.Find("Muzzle");
        curMagazine = magazine;
        audio = transform.GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Team1"))
            PlayerSetting();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDisable()
    {
        isReloadingOn = false;
    }

    public void Reload()
    {
        if (curMagazine < magazine && !isReloadingOn)
        {
            curMagazine = 0;
            StartCoroutine(Reloading());
        }
    }
    public void ShootSound()
    {
        audio.PlayOneShot(audioFireClip);
    }

    public void ClipSound()
    {
        audio.PlayOneShot(audioClipClip, clipReloadVolume);
    }

    bool isReloadingOn = false;
    IEnumerator Reloading()
    {
        isReloadingOn = true;
        yield return new WaitForSeconds(reload);
        if (audio)
            audio.PlayOneShot(audioReloadClip, clipReloadVolume);
        curMagazine = magazine;
        isReloadingOn = false;
    }

    public void PlayerSetting()
    {
        clipReloadVolume = 1f;
        audio.spatialBlend = 0;
        if (gameObject.name == "M4A1(Clone)")
            audio.volume = 0.25f;
        else if (gameObject.name == "MP5(Clone)")
            audio.volume = 0.12f;
        else if (gameObject.name == "Kar98k(Clone)")
            audio.volume = 0.36f;
        else if (gameObject.name == "Glock(Clone)")
            audio.volume = 0.2f;
        else
            audio.volume = 0.25f;
    }
}
