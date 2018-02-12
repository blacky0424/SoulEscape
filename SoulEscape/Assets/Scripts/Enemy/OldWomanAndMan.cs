using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ２のボスの動きを管理するクラス
/// </summary>
public class OldWomanAndMan : Boss
{

    //突撃するまでの時間
    float attackTime;
    float attackTimer;
    //突撃速度
    float speed;
    //突撃する位置
    Vector3 attackPos;
    bool attack = false;

    //左右への移動を開始する時間
    float moveTime;
    float moveTimer;
    //移動する座標
    Vector3 movePos;

    //敵の放つ弾
    [SerializeField]
    GameObject bullet;
    float bulletTimer;
    float bulletTime;

    List<GameObject> bulletbox = new List<GameObject>();

    protected override void OnEnable()
    {
        base.OnEnable();
        bulletTime = Random.Range(1.0f, 3.0f);
    }

    protected override void Update()
    {
        base.Update();

        //バトル中の動き
        if (state == State.Battle)
        {
            //突撃中
            if (attack)
            {
                transform.position = Vector3.Lerp(transform.position, attackPos, Time.deltaTime * speed);
                float distance = Vector3.Distance(transform.position, attackPos);
                //移動完了
                if (distance < 0.01f)
                {
                    if (attackPos == firstPos)
                    {
                        attack = false;
                    }
                    else
                    {
                        transform.position = attackPos;
                        attackPos = firstPos;
                        AttackDateDecide();
                    }
                }
            }
            //初期位置へ戻る
            else
            {
                attackTimer += Time.deltaTime;
                //タイマー
                if (attackTimer > attackTime)
                {
                    float x = Random.Range(-4.0f, 0);
                    float y = Random.Range(-4.0f, 4.0f);
                    attackPos = new Vector3(x, y, 0);
                    AttackDateDecide();
                    attack = true;
                }

                bulletTimer += Time.deltaTime;
                //弾の生成
                if (bulletTimer > bulletTime)
                {
                    BulletFire();
                }

                if (transform.position != firstPos)
                {
                    float distance = Vector3.Distance(transform.position, firstPos);
                    //移動完了
                    if (distance < 0.01f)
                    {
                        transform.position = firstPos;
                    }
                }

                //初期位置から近い場合
                if (transform.position.x > firstPos.x - 2.0f || transform.position.x < firstPos.x + 2.0f)
                {
                    transform.position = Vector3.Lerp(transform.position, movePos, Time.deltaTime * speed);
                }
                //左右移動完了時
                if (Vector3.Distance(transform.position, movePos) < 0.11f)
                {
                    moveTimer += Time.deltaTime;
                }

                if (moveTimer > moveTime)
                {
                    MoveDatedecide();
                }
            }
        }
    }


    /// <summary>
    /// 突撃に必要な情報の決定
    /// </summary>
    void AttackDateDecide()
    {
        attackTime = Random.Range(6.0f, 12.0f);
        speed = Random.Range(1.0f, 3.0f);
        attackTimer = 0;
    }

    void MoveDatedecide()
    {
        moveTime  = Random.Range(1.0f, 5.0f);
        speed = Random.Range(1.0f, 3.0f);
        moveTimer = 0;
        float x = Random.Range(firstPos.x - 2.0f, firstPos.x);
        float y = Random.Range(-4.0f, 4.0f);
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
        bulletTime = Random.Range(1.0f, 2.0f);
        //生成位置の決定
        Vector2 pos = new Vector2(transform.position.x - 0.8f, transform.position.y);
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