using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 弾の共通部分を管理するクラス
/// </summary>
public class Bullet : MonoBehaviour
{
    //耐久値
    [SerializeField]
    protected int originStrength;
    int strength;
    //速度
    [SerializeField]
    protected float speed;
    //x座標
    [HideInInspector]
    //ここをvector2 方向に
    public float dir;
    //初期x座標
    protected float firstPosX;
    //ダメージ量
    public int damage;
    //ポイント
    [SerializeField]
    protected int scorePoint;
    //移動方向
    [SerializeField]
    public Vector3 direction;

    enum BulletType
    {
        Player,
        Enemy,
    }
    [SerializeField]
    BulletType bulletType;

    void Awake()
    {
        strength = originStrength;
        firstPosX = transform.position.x;
    }

    /// <summary>
    /// 弾の耐久値更新
    /// </summary>
    /// <param name="decrease"></param>
    protected void decreaseStrength(int decrease)
    {
        strength -= decrease;
    }

    /// <summary>
    /// 初期化
    /// </summary>
    protected void initialize()
    {
        switch (bulletType)
        {
            case BulletType.Enemy:
                transform.position = new Vector2(firstPosX, Random.Range(-4.75f, 4.75f));
                break;
            case BulletType.Player:
                
                break;
        }
        strength = originStrength;
        gameObject.SetActive(false);
    }

    protected virtual void Update()
    {

        if (strength <= 0)
        {
            initialize();
            //タイプ別処理
            switch (bulletType)
            {
                case BulletType.Enemy:
                    GameScene.Instance.ScoreUp(scorePoint);
                    break;
                case BulletType.Player:
                    break;
                default:
                    break;
            }
            GameScene.Instance.PlaySound(GameScene.Instance.destroyBullet);
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == TagName.Player)
        {
            GameScene.Instance.HpChange(damage);
            initialize();
        }
        else if(c.gameObject.tag == TagName.Finish)
        {
            strength = 0;
        }
        else
        {
            decreaseStrength(c.GetComponent<Bullet>().damage);
        }
    }
}
