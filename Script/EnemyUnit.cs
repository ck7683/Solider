using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    [Min(1)]
    public int hp;
    private int curHp;
    [Min(0f)]
    public float speed;

    [HideInInspector]
    public Rigidbody2D rigid;
    private EnemyManager enemyManager;
    [HideInInspector]
    public Weapon weapon;
    private SpriteRenderer sprite;

    private ObjectPooling op;
    public Animator ani;

    private Transform target;

    public float engageDistance;
    public float approachDistance;

    public Transform hpBar;
    private EnemyHpBar enemyHpBar;

    private void Awake()
    {
        op = ObjectPooling.GetInstance();
        rigid = transform.GetComponent<Rigidbody2D>();
        weapon = transform.Find("Weapon").GetComponent<Weapon>();
        sprite = transform.GetComponent<SpriteRenderer>();
        ani = transform.GetComponent<Animator>();
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        enemyHpBar = hpBar.GetComponent<EnemyHpBar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        curHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        Attack(target);
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

        weapon.Shoot(1f, 0.25f, 0.2f, 5);
    }

    public void Reload()
    {
        weapon.Reload();
    }

    void Attack(Transform target)
    {
        if (target == null || !target.gameObject.activeInHierarchy)
            Move(new Vector2(0, 0));
        else
        {
            Aiming(target.transform.position);
            if (Vector2.Distance(transform.position, target.position) < engageDistance)
                Shoot();
            if (Vector2.Distance(transform.position, target.position) > approachDistance)
                Move(new Vector2(target.position.x - transform.position.x, 0));
            else
                Move(new Vector2(0, 0));
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
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
            enemyManager.deathCount++;
            enemyManager.RenewScore();
            enemyHpBar.SetHpBar((float)curHp / hp);
            op.Disable(gameObject);
        }
        enemyHpBar.SetHpBar((float)curHp / hp);
    }
}
