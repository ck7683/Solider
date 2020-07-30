using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    SpriteRenderer sprite;
    SpriteRenderer spriteBG;

    // Start is called before the first frame update
    void Start()
    {
        sprite = transform.GetComponent<SpriteRenderer>();
        spriteBG = transform.Find("BackGround").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHpBar(float value)
    {
        Vector2 v = sprite.size;
        v.x = value*1.5f;
        sprite.size = v;

        v = spriteBG.size;
        v.x = value * 1.5f + .1f;
        spriteBG.size = v;
    }
}
