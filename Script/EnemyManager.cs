using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    ObjectPooling op;
    [SerializeField]
    private PlayerManager playerManager;
    Queue<Transform> noneSetTarget;

    public Transform leftSpawn, rightSpawn;

    [HideInInspector]
    public int deathCount;

    [SerializeField]
    private Transform scoreLabel;
    private Score score;

    // Start is called before the first frame update
    void Start()
    {
        deathCount = 0;
        score = scoreLabel.GetComponent<Score>();
        noneSetTarget = new Queue<Transform>();
        op = ObjectPooling.GetInstance();
        op.Initialize(Resources.Load<GameObject>("Prefabs/Enemy"));
        StartCoroutine(CheckPlayer());
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemy()
    {
        GameObject go = op.Enable("Enemy");
        go.transform.position = 
            (Random.Range(0, 2) == 1 ? leftSpawn.position : rightSpawn.position);
        EnemyUnit enemy = go.GetComponent<EnemyUnit>();
        enemy.SetTarget(playerManager.playerTransform);
        go.layer = gameObject.layer;
        noneSetTarget.Enqueue(go.transform);

        int rand = Random.Range(0, 101);
        if(rand < 10)
        {
            enemy.approachDistance = 5;
            enemy.engageDistance = 9;
            enemy.UseWeapon(0);
        }
        else if(rand < 40)
        {
            enemy.approachDistance = 3;
            enemy.engageDistance = 7;
            enemy.UseWeapon(1);
        }
        else if(rand < 95)
        {
            enemy.approachDistance = 2;
            enemy.engageDistance = 6;
            enemy.UseWeapon(2);
        }
        else
        {
            enemy.approachDistance = 10;
            enemy.engageDistance = 12;
            enemy.UseWeapon(3);
        }
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            float t = 10f - (float)deathCount * 0.3f;
            if (t < 0.5f) t = 0.5f;
            yield return new WaitForSeconds(t);
            SpawnEnemy();
        }
    }

    IEnumerator CheckPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            if (playerManager.playerTransform)
                if (noneSetTarget.Count > 0)
                {
                    Transform tf = noneSetTarget.Dequeue();
                    tf.GetComponent<EnemyUnit>().SetTarget(playerManager.playerTransform);
                }
        }
    }

    public void RenewScore()
    {
        score.RenewScore(deathCount);
    }
}
