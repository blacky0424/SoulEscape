using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 赤弾を管理するクラス
/// </summary>
public class RedBullet : Bullet {

    Transform bossPos;
    //円運動をする起点となる位置
    Vector3 basePos;
    //移動速度
    float speed;
    //周る大きさ
    float width;

    private void OnEnable()
    {
        bossPos = FindObjectOfType<Boss>().transform;
        basePos = transform.position;

        speed = Random.Range(1.0f, 5.0f);
        width = Random.Range(1.0f, 3.0f);
    }

    protected override void Update()
    {
        base.Update();
        float x = basePos.x + Mathf.Cos(Time.time * speed) * width;
        float y = basePos.y + Mathf.Sin(Time.time * speed) * width;

        transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, 0), 1.0f);

        if (GameScene.Instance.state == GameState.Clear)
        {
            this.gameObject.SetActive(false);
        }
    }
}
