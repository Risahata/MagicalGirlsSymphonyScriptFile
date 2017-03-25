using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class oversp : MonoBehaviour {
    //ゲームオーバーのスクリプト

    //ゲームオーバーまたはゲームクリアのキャンバス
    public GameObject overC;

    private GameObject overcc;
    private Text overcT;

    [SerializeField]
    GlobalSp Gl;
    [SerializeField]
    audioSys aud;
    //フェードsw
    public static bool fdsw;
    //フェードの時間
    private float nowTime;
    private float endtime;
    //透明度
    private float touka;
    private Color Cc;
    //フェードの白のイメージ
    public Image fadIm;
    public GameObject fadg;

    private bool otosw;
    //bgmの音を消すスイッチ
    private bool bgkesu;
    private float bgtime0;
    private float bgtime1;
    //bgmのオーディオソース
    public AudioSource bgm;
	// Use this for initialization
	void Start () {
        bgm.Play();
        overcc = overC.transform.FindChild("tnT").gameObject;
        overcT = overcc.GetComponent<Text>();
        overC.SetActive(false);

        fdsw = false;
        nowTime = 0;
        endtime = 3;
        touka = nowTime / endtime;
        Cc = fadIm.color;
        Cc.a = touka;
        fadg.SetActive(false);
        otosw = false;
        bgkesu = false;
	}
	
	// Update is called once per frame
	void Update () {
        switch (Gl.tarn)
        {
   
            //ゲームオーバー
            case 4:
                Debug.Log("表示された");
                //音一回だけ鳴らす
                if (otosw == false) {
                    aud.SEo4();
                    bgm.Pause();
                    aud.SEo5();
                    //bgmの音をけす
                    bgkesu = true;
                    otosw = true;
                }
                            overcT.text = @"
<Size=50>コンティニュー？</Size>

魔法石１個使用して
コンティニューしますか？
";
                        
            overC.SetActive(true);
                break;
            //あきらめるとき
            case 5:
                           overcT.text = @"
<Size=50>あきらめますか？</Size>

<color=#ffff00>取得した経験値は
入手できなくなります。</color>
";
            overC.SetActive(true);
                break;
            default:
                overC.SetActive(false);
                break;
        }



        //フェード
        if (fdsw == true)
        {
            fadg.SetActive(true);
            nowTime += Time.deltaTime;
            touka = nowTime / endtime;
            Cc = fadIm.color;
            Cc.a = touka;
            fadIm.color = Cc;
            if (nowTime > endtime)
            {
                SceneManager.LoadScene("menu");
            }
        }
        //bgmの音を消すスイッチがおん
        if (bgkesu == true)
        {
            //bgmの音を消す処理
        }
      
	}
    //ボタン

    //はい
    public void hai()
    {
        aud.SEo3();
        switch (Gl.tarn)
        {
            case 4:
                // コンティニューするとき
                bgm.Play();
                Gl.tarn = 6;
                otosw = false;
                break;
            case 5:
                aud.SEo4();
                bgm.Pause();
                aud.SEo5();
                ScnGlob.tomain = true;
                //あきらめる
                    fadg.SetActive(true);
        //画面がフェード
        fdsw = true;
                break;
        }
  
     
    }
    //いいえ
    public void iie()
    {
        aud.SEo3();
        switch (Gl.tarn)
        {
            case 4:
                ScnGlob.tomain = true;
                               fadg.SetActive(true);
        //画面がフェード
        fdsw = true;
                break;
            case 5:
                //元のターンへ
                Gl.tarn = Gl.kakuhako;
        
                break;
        }
    }
    
}
