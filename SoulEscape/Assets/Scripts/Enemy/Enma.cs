using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enma : Boss {
    //敵の放つ弾
    [SerializeField]
    GameObject bullet;
    float bulletTimer;
    float bulletTime;

    //移動するまでの時間
    float moveTime;
    float timer;
    float moveSpeed;
    //移動する位置
    Vector3 movePos;

    List<GameObject> bulletbox = new List<GameObject>();

    protected override void OnEnable()
    {
        base.OnEnable();
        bulletTime = Random.Range(3.0f, 5.0f);
        moveTime = Random.Range(5.0f, 10.0f);
        moveSpeed = Random.Range(0.5f, 2.0f);
        //生成時間の決定
        bulletTime = Random.Range(3.0f, 8.0f);
    }

    protected override void Update()
    {
        base.Update();

        //バトル中の動き
        if (state == State.Battle)
        {
            if(timer > moveTime)
            {
                MoveDateDecide();
            }

            if(bulletTimer > bulletTime)
            {
                BulletFire();
            }

            timer += Time.deltaTime;
            bulletTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, movePos, Time.deltaTime * moveSpeed);
        }
    }

    /// <summary>
    /// 移動に必要な情報の決定
    /// </summary>
    void MoveDateDecide()
    {
        moveTime = Random.Range(5.0f, 10.0f);
        moveSpeed = Random.Range(0.5f, 2.0f);
        timer = 0;
        float x = Random.Range(-3.0f, 5.0f);
        float y = Random.Range(-2.5f, 2.5f);
        movePos = new Vector3(x, y, 0);
    }

    /// <summary>
    /// 敵弾発射
    /// </summary>
    void BulletFire()
    {
        //タイマーの初期化
        bulletTimer = 0;
        //生成時間の決定
        bulletTime = Random.Range(3.0f, 8.0f);
        //生成位置の決定
        Vector2 pos = transform.position;
        int i = 0;
        //非表示になっている弾があれば再利用
        for (i = 0; i < bulletbox.Count; i++)
        {
            GameObject b = bulletbox[i];
            if (!b.activeSelf)
            {
                b.SetActive(true);
                b.transform.position = pos;
                break;
            }
        }

        //弾がなければ
        if (bulletbox.Count == 0 || i == bulletbox.Count)
        {
            //whiteBullet生成
            GameObject b = Instantiate(bullet, pos, Quaternion.identity);
            bulletbox.Add(b);
        }
    }
}
