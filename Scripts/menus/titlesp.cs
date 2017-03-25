using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class titlesp : MonoBehaviour {
    [SerializeField]
    ScnGlob SG;
    //何秒間でフェードか
    private float nowTime;
    private float endTime;
    //フェードスイッチ
    private bool fadsw;
    //透明度
    private float touka;
    //タイトルイメージ
    public GameObject tim;
    private GameObject[] timc = new GameObject[5];
    private Image[] tiIm=new Image[5];
    //色
    private Color[] Cc=new Color[5];
    //音
    [SerializeField]
    audioSys aud;
    //最初の点滅
    //時間
    private float[] ten=new float[2];
	// Use this for initialization
	void Start () {
        tim.SetActive(true);
        nowTime = 2;
        //O秒でフェード
        endTime = 2;
        fadsw = false;
        touka = nowTime / endTime;
        for(int i = 0; i < timc.Length; i++)
        {
            timc[i] = tim.transform.FindChild("titleIm"+i).gameObject;
            tiIm[i] = timc[i].GetComponent<Image>();
            Cc[i] = tiIm[i].color;
            Cc[i].a = touka;
        }
        tiIm[2].color = new Color(0.5f, 0.5f, 0.5f);
        ten[0] = 0;
        ten[1] = 1f;
	}
	
	// Update is called once per frame
	void Update () {
     
        if (fadsw == true)
        {
            fad();
        }
        else
        {         
            //最初の状態
            ten[0] += Time.deltaTime;
            float par=0.5f+ten[0]/2;
            tiIm[2].color = new Color(par, par, par);
            if (ten[0] > ten[1])
            {
                ten[0] = 0;
            }
            if (Input.GetMouseButtonDown(0))
            {
                //音
                aud.SEo0();
                //フェード開始
                fadsw = true;

            }
        }
	}
    //ふぇーど
    void fad()
    {
        nowTime -= Time.deltaTime;
        touka = nowTime / endTime;
        for(int i = 0; i < timc.Length; i++)
        {
            Cc[i] = tiIm[i].color;
            Cc[i].a = touka;
            tiIm[i].color = Cc[i];
        }
    
        if (nowTime < 0)
        {
            fadsw = false;
            //ゲーム状態をメニューに
            SG.gamestate = 1;
        }
    }
}
