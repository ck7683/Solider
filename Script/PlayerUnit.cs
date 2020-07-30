using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour {
    [Min(1)]
    public int hp;
    private int curHp;
    [Min(0f)]
    public float speed;

    [HideInInspector]
    public Rigidbody2D rigid;
    private PlayerManager playerManager;
    [HideInInspector]
    public Weapon weapon;
    private SpriteRenderer sprite;

    private ObjectPooling op;
    public Animator ani;
    

    // Start is called before the first frame update
    void Start()
    {
        curHp = hp;
        op = ObjectPooling.GetInstance();
        rigid = transform.GetComponent<Rigidbody2D>();
        playerManager = transform.parent.transform.GetComponent<PlayerManager>();
        weapon = transform.Find("Weapon").GetComponent<Weapon>();
        sprite = transform.GetComponent<SpriteRenderer>();
        ani = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Move(Vector2 direction)
    {
        float x = direction.x;
        if (Mathf.Abs(x) > 0.1)
            ani.SetBool("isMoving", true);
        else
            ani.SetBool("isMoving", false);
        if (x < -speed)
        {
            x = -speed;
        }
        if (x > speed)
        {
            x = speed;
        }
        if (Mathf.Abs(x) > 0.01f)
        {
            if (x > 0)
                sprite.flipX = false;
            else
                sprite.flipX = true;
        }
        Vector2 v = new Vector2(x, rigid.velocity.y);
        rigid.velocity = v;
    }

    public void Aiming(Vector3 target)
    {
        target.z = 0;
        weapon.Aiming(target);
    }

    public void Shoot()
    {
        
        weapon.Shoot();
    }

    public void Reload()
    {
        weapon.Reload();
    }

    public bool UseWeapon(int slot)
    {
        return weapon.UseWeapon(slot);
    }

    public void Hit(float dmg)
    {
        curHp -= (int)dmg;
        if (curHp <= 0)
        {
            curHp = 0;
            playerManager.Dead();
        }
        if(hp != 0)
            playerManager.RenewHpBar((float)curHp / hp);
    }
}
