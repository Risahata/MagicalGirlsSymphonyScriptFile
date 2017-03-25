using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damegeteki : MonoBehaviour {
    //ダメージテキストオブジェクト
    //自動で消える
    private float nowTime;
    private float endTime;
    //飛ばす位置を決める一度だけ通るsw
    bool kime;
    Vector2 ranp;
	// Use this for initialization
	void Start () {
        nowTime = 0;
        endTime = 2;
        kime = false;
	}
	// Update is called once per frame
	void Update () {
        nowTime += Time.deltaTime;
        //ゲームオブジェクトを飛ばす
        if (kime == false)
        {
            Vector3 m=gameObject.transform.position;
            //最終地点
            ranp = new Vector3(Random.Range(m.x - 2, m.x + 1), Random.Range(m.y - 1, m.y + 1),m.z);

            kime = true;
        }
        //リーぷで飛ばす
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, ranp, 1f*Time.deltaTime);
        if (nowTime > endTime)
        {
            Destroy(gameObject);
        }
	}
}
