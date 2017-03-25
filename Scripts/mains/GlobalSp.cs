using UnityEngine;
using System.Collections;

public class GlobalSp : MonoBehaviour {
    //ターン　0:自分　1:敵
    public int tarn;
    //何バトルあるか
    public int battlesuu;
    //今何バトルメであるか
    public int nowbattle;
    //ターンを格納する箱
    public int kakuhako;
    [SerializeField]
    audioSys aud;
	// Use this for initialization
	void Start () {
        //まず自分から
        tarn = 0;
        battlesuu = 4;
        nowbattle = 0;
	}	
    //メニューボタン
    public void menyuB()
    {
        aud.SEo3();
        //ポーズへ
        kakuhako = tarn;
        tarn = 5;
    }
}
