using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームの状態
/// </summary>
public enum GameState
{
    Game,
    Boss,
    Clear,
    GameOver,
}

/// <summary>
/// ゲーム全体を管理するクラス
/// </summary>
public class GameScene : SingletonMonoBehaviour<GameScene> {

    [SerializeField]
    GameObject[] enemyBullet;
    [SerializeField]
    Player player;
    [SerializeField]
    GameObject boss;

    //HP
    public static int hp;
    //最大体力
    [SerializeField]
    int maxhp;
    //敵弾タイマー
    float bulletTimer = 0;

    //EnemyBulletの保管箱
    List<GameObject> enemyBulletBox = new List<GameObject>();
    //whiteBulletの生成時間
    float bulletTime;
    //whiteBulletの最小生成時間
    [SerializeField]
    float bulletTimeMin;
    //whiteBulletの最大生成時間
    [SerializeField]
    float bulletTimeMax;

    //背景
    float backFlowPosX;
    float backFlowFirstPosX;
    float backFlowSpeed = 0.01f;

    //スコア
    int score = 0;

    //クリアまでの距離
    [SerializeField]
    Text distanceText;
    const string distanceString1 = "まであと　";
    const string distanceString2 = " m";
    [SerializeField]
    float distance;

    //体力テキスト
    Text hpText;
    //スコアテキスト
    Text scoreText;
    const string scoreString = "Score ";

    //現在使えるプレイヤーの弾数
    public int ableBullet = 3;

    public GameState state;

    void Start () {
        state = GameState.Game;

        //最初に流れる敵弾の情報
        backFlowFirstPosX = transform.position.x;
        backFlowPosX = backFlowFirstPosX;
        bulletTime = Random.Range(bulletTimeMin, bulletTimeMax);

        //最大体力の設定
        hp = maxhp;
        hpText = GameObject.Find("HpText").GetComponent<Text>();
        hpText.text = hp.ToString();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

        FadeManager.Instance.FadeIn();
    }
	
	void Update () {

        //ゲームの状態別更新
        switch (state)
        {
            case GameState.Game:
                //背景が流れる
                backFlowPosX -= backFlowSpeed;
                transform.position = new Vector2(backFlowPosX, transform.position.y);
                if (transform.position.x < -10.0f)
                {
                    backFlowPosX = backFlowFirstPosX;
                }
                bulletTimer += Time.deltaTime;
                distance -= Time.deltaTime;
                //残り距離更新
                distanceText.text = distanceString1 + distance.ToString("F2") + distanceString2;
                break;
            case GameState.Boss:
                bulletTimer += Time.deltaTime;
                if(boss == null)
                {
                    Invoke(((System.Action)IsClear).Method.Name, 2.0f);
                }
                break;
            case GameState.Clear:
                //弾が出ないように
                for (int i = 0; i < enemyBulletBox.Count; i++)
                {
                    enemyBulletBox[i].SetActive(false);
                }
                //スコアの保存
                ScoreManager.Instance.Score = score;
                //クリアしたステージを更新
                if(ScoreManager.Instance.Stage == ScoreManager.Instance.ClearNum + 1)
                {
                    ScoreManager.Instance.ClearNum++;
                }
                break;
        }

        if (state != GameState.Clear)
        {
            //タイマーが生成時間を超えたら
            if (bulletTimer > bulletTime)
            {
                EnemyBulletFire();
            }

            //残り距離の更新
            if (distance < 0.1f)
            {
                FadeManager.Instance.BossAlert();
                distance = 0.1f;
                if (boss != null && !boss.activeSelf)
                {
                    distanceText.gameObject.SetActive(false);
                    Invoke(((System.Action)BossAppear).Method.Name, 2.5f);
                }
                state = GameState.Boss;
            }
        }

        if (state != GameState.GameOver)
        {
            if (hp <= 0)
            {
                Destroy(player.gameObject);
                state = GameState.GameOver;
                Invoke(((System.Action)LoadGameOver).Method.Name, 2.0f);
            }
        }
    }

    void LoadGameOver()
    {
        FadeManager.Instance.fadeColor = Color.black;
        FadeManager.Instance.FadeOut();
        SceneManager.Instance.LoadScene(SceneManager.GameOver_Scene);
    }

    /// <summary>
    /// ボスの出現
    /// </summary>
    void BossAppear()
    {
        boss.SetActive(true);
    }


    void IsClear()
    {
        state = GameState.Clear;
    }

    /// <summary>
    /// 敵弾発射
    /// </summary>
    void EnemyBulletFire()
    {
        //タイマーの初期化
        bulletTimer = 0;
        //生成時間の決定
        bulletTime = Random.Range(bulletTimeMin, bulletTimeMax);
        //生成位置の決定
        Vector2 spawnPos = new Vector2(9.0f, Random.Range(-4.75f, 4.75f));
        int random = Random.Range(0, enemyBullet.Length);
        int i = 0;
        //非表示になっている弾があれば再利用
        for (i = 0; i < enemyBulletBox.Count; i++)
        {
            GameObject b = enemyBulletBox[i];
            if(b.tag == enemyBullet[random].tag)
            if (!b.activeSelf)
            {
                b.SetActive(true);
                break;
            }
        }

        //弾がなければ
        if (enemyBulletBox.Count == 0 || i == enemyBulletBox.Count)
        {
            //whiteBullet生成
            GameObject bullet = Instantiate(enemyBullet[random], spawnPos, Quaternion.identity);
            enemyBulletBox.Add(bullet);
        }
    }

    /// <summary>
    /// プレイヤーHPの更新
    /// </summary>
    /// <param name="damage"></param>
    public void HpChange(int damage)
    {
        hp -= damage;
        hpText.text = hp.ToString();
    }

    /// <summary>
    /// スコアの更新
    /// </summary>
    /// <param name="point"></param>
    public void ScoreUp(int point)
    {
        score += point;
        scoreText.text = scoreString + score.ToString();
    }

    public Player GetPlayer
    {
        get
        {
            return player;
        }
    }
}
