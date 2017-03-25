using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ScnGlob : MonoBehaviour {
    //スクリプトの存在有無
    //タイトル
    [SerializeField]
    titlesp Tsp;
 
    //メニュー
    [SerializeField]
    menuG Mmg;
    //ローディング画面
    //ローディングキャンバス
    public GameObject Loadob;
    //ゲーム状態
    public int gamestate;
    //タイトルキャンバス
    public GameObject titleC;
    //main1画面から来たかどうかのsw
    public static bool tomain;
	// Use this for initialization
	void Start () {
        //ローディングキャンバス最初は見えない
        Loadob.SetActive(false);
        gamestate = 0;  
	}
	
	// Update is called once per frame
	void Update () {
        if (tomain == true)
        {
            gamestate = 1;
            tomain = false;
        }
        switch (gamestate)
        {
            //タイトル
            case 0:
                titleC.SetActive(true);
                Tsp.enabled = true;          
                Mmg.enabled = false;
                break;
            //メニュー
            case 1:
                titleC.SetActive(false);
                Tsp.enabled = false;          
                Mmg.enabled = true;
                break;
            //ローディング
            case 2:
                //ローディングキャンバスの表示
                Loadob.SetActive(true);
                //待つ
                StartCoroutine("Loadwait");
                break;
        }
   

	}
    //ローディングの待つ時間
    IEnumerator Loadwait()
    {
        yield return new WaitForSeconds(2);
        switch (menuG.stagen)
        {
            case 0:
                SceneManager.LoadScene("main");
                break;
            case 1:
                break;
        }
       
    }
}
