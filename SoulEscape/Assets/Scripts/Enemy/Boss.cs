using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ボスを管理するクラス
/// </summary>
public class Boss : MonoBehaviour {

    //出現時の初期位置
    [SerializeField]
    protected Vector3 firstPos;
    public float hp;
    float maxHp;
    public int damage;
    [SerializeField]
    int scorePoint;

    //HPゲージ
    [SerializeField]
    Image hpImage;
    [SerializeField]
    Image hpGage;
    //透明度
    float alpha;

    /// <summary>
    /// 状態
    /// </summary>
    protected enum State
    {
        Start,
        Battle,
    }
    protected State state;

    protected virtual void OnEnable()
    {
        state = 0;
        maxHp = hp;
        hpGage.gameObject.SetActive(true);
    }

    protected virtual void Update() {
        //状態別更新
        switch (state) {
            case State.Start:
                //出現時の移動
                transform.position = Vector3.Lerp(transform.position, firstPos, Time.deltaTime);
                float distance = Vector3.Distance(transform.position, firstPos);
                //移動完了後バトルスタート
                if (distance < 0.1f)
                {
                    transform.position = firstPos;
                    state = State.Battle;
                }
                break;
            case State.Battle:
                break;
        }

        if (hp <= 0)
        {
            hpGage.gameObject.SetActive(false);
            GameScene.Instance.ScoreUp(scorePoint);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (hp > 0)
        {
            if (state != 0)
            {
                if (c.gameObject.tag == TagName.Player)
                {
                    GameScene.Instance.HpChange(damage);
                }
                else if (c.gameObject.tag == TagName.MyBullet)
                {
                    Bullet b = c.gameObject.GetComponent<Bullet>();
                    //HPの更新
                    hp -= b.damage;
                    alpha = hp / maxHp;
                    hpImage.fillAmount = alpha;
                }
            }
        }
    }
}
