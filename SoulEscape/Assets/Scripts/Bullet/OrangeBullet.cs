using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBullet : Bullet {


    private void OnEnable()
    {
        speed = Random.Range(0.01f, 0.05f);
        direction.y = Random.Range(1, 5);
    }

    protected override void Update()
    {
        base.Update();
        //移動
        transform.position += direction * speed;
        //端についたら初期化
        if (transform.position.x < -firstPosX)
        {
            initialize();
        }

        //上に行ける上限
        if (transform.position.y > 4.75f)
        {
            transform.position = new Vector2(transform.position.x, 4.75f);
            speed = Random.Range(0.01f, 0.05f);
            direction.y = Random.Range(-5, -1);
        }
        else if (transform.position.y < -4.75f)
        {
            transform.position = new Vector2(transform.position.x, -4.75f);
            speed = Random.Range(0.01f, 0.05f);
            direction.y = Random.Range(1, 5);
        }
    }
}
