using System.Collections;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// プレイヤーの残弾を管理
/// </summary>
public class RemainingMyBullet : MonoBehaviour {

    Image image;

	void Start () {
        image = GetComponent<Image>();
	}
	
	void Update () {
        if (image.fillAmount < 1.0f)
        {
            image.fillAmount += 0.005f;
        }
    }
}
