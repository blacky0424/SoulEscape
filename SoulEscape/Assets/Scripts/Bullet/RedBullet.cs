using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 赤弾を管理するクラス
/// </summary>
public class RedBullet : Bullet {

    //円の大きさ
    float radius;

    private void OnEnable()
    {
        radius = Random.Range(1, 4);
    }

    protected override void Update()
    {
        base.Update();
        float x = Mathf.Cos(Time.time);
        float y = Mathf.Sin(Time.time);
        float z = 0f;

        transform.position = new Vector3(x, y, z) * radius;

        if(GameScene.Instance.state == GameState.Clear)
        {
            this.gameObject.SetActive(false);
        }
    }
}
