using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    ObjectPooling op;
    Rigidbody2D rigid;
    float dmg;
    float dmgCritical;
    float speed;
    // Start is called before the first frame update
    void Awake()
    {
        op = ObjectPooling.GetInstance();
        rigid = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(Vector2 loc, Vector2 target, float damage, float damageCritical, float bulletSpeed, float deadTime)
    {
        dmg = damage;
        dmgCritical = damageCritical;
        speed = bulletSpeed;
        transform.position = loc;
        rigid.velocity = (target - loc).normalized * speed;

        float angle = Mathf.Atan2(rigid.velocity.y, rigid.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        StartCoroutine(DeadBullet(deadTime));
    }


    public void Shoot(Vector2 loc, float angle, float damage, float damageCritical, float bulletSpeed, float deadTime)
    {
        dmg = damage;
        dmgCritical = damageCritical;
        speed = bulletSpeed;
        transform.position = loc;
        Vector2 target = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        rigid.velocity = target.normalized * speed;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        StartCoroutine(DeadBullet(deadTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.name == "Enemy")
        {
            Transform tf = collision.transform;
            EnemyUnit eu = tf.GetComponent<EnemyUnit>();
            if (Random.Range(0, 6) == 0)
                eu.Hit(dmgCritical);
            else
                eu.Hit(dmg);
        }
        else if(collision.transform.name == "Player")
        {
            Transform tf = collision.transform;
            PlayerUnit eu = tf.GetComponent<PlayerUnit>();
            if (Random.Range(0, 6) == 0)
                eu.Hit(dmgCritical);
            else
                eu.Hit(dmg);
        }
        op.Disable(gameObject);
    }
    
    IEnumerator DeadBullet(float deadTime)
    {
        yield return new WaitForSeconds(deadTime);
        op.Disable(gameObject);
    }
}
