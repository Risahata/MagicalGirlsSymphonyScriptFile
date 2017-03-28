using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class menuG : MonoBehaviour {
    //メニューのスクリプト
    [SerializeField]
    ScnGlob Globalsp;
    //音
    [SerializeField]
    audioSys aud;
    //押されたらそれぞれのエリアへ
    //クリアしていたら表示
    public static bool[] clcon=new bool[1];
    //ステージナンバー
    public static int stagen;
    //ボタンのゲームオブジェクト
    public GameObject[] gameobj = new GameObject[1];
    //テキスト
    public Text textc;
    //テキストが変わったよというsw
    bool tekihen;
    //テキストが変わったら数秒後もとに戻る
    float nowTime;
    float endTime;
	// Use this for initialization
	void Start () {
        gameobj[0].SetActive(clcon[0]);
        tekihen = false;
        nowTime = 0;
        endTime = 1;
        textc.text = "エリアを選択してください";
	}	
	// Update is called once per frame
	void Update () {
        if (tekihen == true)
        {
            nowTime += Time.deltaTime;
            if (nowTime > endTime)
            {
                textc.text="エリアを選択してください";
                nowTime = 0;
                tekihen = false;
            }
        }
	}
    //エリア１へ
    public void push0()
    {
        //音を鳴らして
        aud.SEo0();
        aud.SEo2();
        stagen = 0;
        //ローディング画面へ
        Globalsp.gamestate=2;       
    }
    //エリア２へ
    public void push1()
    {
        stagen = 1;
        //ブブという音を鳴らす
        aud.SEo1();
        textc.text = "準備中";
        tekihen = true;
    }
    //タイトルへのボタン
    public void fortitle()
    {
        aud.SEo0();
        //フェードして
    //シーンの読み直し
        StartCoroutine("waitti"); 
    }
    IEnumerator waitti()
    {
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel("menu");
    }

}
