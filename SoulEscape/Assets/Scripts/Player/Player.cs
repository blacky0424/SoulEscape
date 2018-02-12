using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーを管理するクラス
/// </summary>
public class Player : MonoBehaviour {
    const string Horizontal = "Horizontal";
    const string Vertical = "Vertical";

    [SerializeField]
    Boss boss;

    [SerializeField]
    GameObject myBullet;
    Rigidbody2D rd;

    //ダメージ判定
    [HideInInspector]
    public bool damage;
    bool invert = false;
    int num;
    SpriteRenderer playerSprite;
    float alpha = 1.0f;

    List<GameObject> playerBullets = new List<GameObject>();
    //残弾
    [SerializeField]
    List<Image> playerBulletsAlpha = new List<Image>();

    //移動速度
    [SerializeField]
    float speed;

    //クリア時のプレイヤーの移動位置（アニメーション）
    [SerializeField]
    Vector3 clearPos1;
    [SerializeField]
    Vector3 clearPos2;
    int c;

    void Start () {
        rd = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();

        c = 0;
	}
	
	void Update () {

        if (GameScene.Instance.state != GameState.Clear)
        {
            //キーボードのPかSpaceで弾発射
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Space))
            {
                BulletFire();
            }

            //上に行ける上限
            if (transform.position.y > 4.75f)
            {
                transform.position = new Vector2(transform.position.x, 4.75f);
            }
            //下に行ける上限
            else if (transform.position.y < -4.35f)
            {
                transform.position = new Vector2(transform.position.x, -4.35f);
            }
            //右に行ける上限
            if (transform.position.x > 6.0f)
            {
                transform.position = new Vector2(6.0f, transform.position.y);
            }
            //左に行ける上限
            else if (transform.position.x < -4.0f)
            {
                transform.position = new Vector2(-4.0f, transform.position.y);
            }

            //ダメージ表現
            if (damage)
            {
                damageExpression();
            }
        }
        //clear時の演出
        else
        {
            ClearDirection();
        }
    }

    private void FixedUpdate()
    {
        if (GameScene.Instance.state != GameState.Clear)
        {
            rd.AddForce(new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical)) * speed - rd.velocity);
        }else
        {
            rd.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// クリア時の演出
    /// </summary>
    void ClearDirection()
    {
        Vector3 pos = Vector3.zero;
        if (c == 0)
        {
            if (Vector3.Distance(transform.position, clearPos1) < 0.1f)
            {
                c = 1;
            }
            pos = clearPos1;
        }
        else if(c == 1)
        {
            if (Vector3.Distance(transform.position, clearPos2) < 0.1f)
            {
                c = 2;
                FadeManager.Instance.fadeColor = Color.white;
                SceneManager.Instance.LoadScene(SceneManager.Result_Scene);
            }
            pos = clearPos2;
        }
        else
        {
            pos = transform.position;
        }

        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 2.0f);
    }

    /// <summary>
    /// 弾発射
    /// </summary>
    void BulletFire()
    {
        //残弾があれば
        for (int i = 0; i < GameScene.Instance.ableBullet; i++)
        {
            if (playerBulletsAlpha[i].fillAmount >= 1.0f)
            {
                Vector2 pos = new Vector2(transform.position.x + 1.0f, transform.position.y);
                int j;
                //非表示になっている弾があれば再利用
                for (j = 0; j < playerBullets.Count; j++)
                {
                    GameObject b = playerBullets[j].gameObject;
                    if (!b.activeSelf)
                    {
                        b.transform.position = pos;
                        b.SetActive(true);
                        break;
                    }
                }

                //弾がなければ
                if (playerBullets.Count == 0 || j == playerBullets.Count)
                {
                    //MyBullet生成
                    GameObject b = Instantiate(myBullet, pos, Quaternion.identity);
                    playerBullets.Add(b);
                }
                playerBulletsAlpha[i].fillAmount = 0;
                break;
            }
        }
    }

    /// <summary>
    /// プレイヤーのダメージ表現
    /// </summary>
    void damageExpression()
    {
        if (!invert)
        {
            alpha -= 0.1f;
        }
        else
        {
            alpha += 0.1f;
        }
        if (num < 3)
        {
            if (alpha >= 1.0f)
            {
                invert = false;
                num++;
            }
            else if (alpha <= 0)
            {
                invert = true;
            }
        }
        else
        {
            damage = false;
            alpha = 1.0f;
            num = 0;
        }
        //画像の透明度の変化
        playerSprite.color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (boss.hp > 0)
        {
            damage = true;
        }
    }

    private void OnTriggerStay2D(Collider2D c)
    {
        if (boss.hp > 0)
        {
            if (c.gameObject.tag == TagName.Finish)
            {
                if (boss == null)
                {
                    boss = FindObjectOfType<Boss>();
                }

                if (!damage)
                {
                    GameScene.Instance.HpChange(boss.damage);
                    damage = true;
                }
            }
        }
    }
}
