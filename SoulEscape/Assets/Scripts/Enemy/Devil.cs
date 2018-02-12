using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ１ボスの動きを管理するクラス
/// </summary>
public class Devil : Boss
{

    //突撃するまでの時間
    float attackTime;
    float timer;
    //突撃速度
    float speed;
    //突撃する位置
    Vector3 attackPos;
    bool attack = false;

    void Start()
    {
        attackTime = 5.0f;
    }

    protected override void Update()
    {
        base.Update();
        timer += Time.deltaTime;

        //バトル中の動き
        if (state == State.Battle)
        {
            //タイマー
            if(timer > attackTime)
            {
                Debug.Log(transform.position);
                if (transform.position == firstPos)
                {
                    float x = Random.Range(-5.0f, 0);
                    float y = Random.Range(-4.0f, 4.0f);
                    attackPos = new Vector3(x, y, 0);
                    Debug.Log(attackPos);
                }
                AttackDateDecide();
                attack = true;
            }

            //突撃中
            if (attack)
            {
                transform.position = Vector3.Lerp(transform.position, attackPos, Time.deltaTime * speed);
                float distance = Vector3.Distance(transform.position, attackPos);
                //移動完了
                if (distance < 00.1f)
                {
                    transform.position = attackPos;
                    if (transform.position == firstPos)
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
            
        }
    }


    /// <summary>
    /// 突撃に必要な情報の決定
    /// </summary>
    void AttackDateDecide()
    {
        attackTime = Random.Range(3.0f, 10.0f);
        speed = Random.Range(1.0f, 3.0f);
        timer = 0;
    }
}

