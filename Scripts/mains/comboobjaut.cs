using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class comboobjaut : MonoBehaviour {
    //自動で消えるオブジェクト
    private float[] nowTime=new float[2];
    private float[] endTime = new float[2];
    //フェードスイッチ
    private bool fadsw;
    //透明度
    private float touka;
    //comboイメージ
    private SpriteRenderer combol;
    //キャンバスのテキスト
    private GameObject canteg;
    //キャンバスのテキスト
    private Text canT;
    private Outline canout;
    //色
    private Color Cc;
    //点滅させる
    private float[] tent=new float[2];
    private int number;
	// Use this for initialization
	void Start () {
        combol = GetComponent<SpriteRenderer>(); 
        canteg = gameObject.transform.FindChild("coT").gameObject;
        //何コンボ目か
        canT = canteg.GetComponent<Text>();
        canout = canteg.GetComponent<Outline>();
        nowTime[0] = 0;
        endTime[0] = 0.8f;
        //フェードのタイム
        nowTime[1] = 0.3f;
        endTime[1] = 0.3f;
        touka = nowTime[1] / endTime[1];
        fadsw = false;
        Cc = combol.color;
        Cc = canT.color;
        Cc = canout.effectColor;
        Cc.a = touka;
        //点滅
        tent[0] = 0;
        tent[1] = 0.1f;
        number = 0;
	}	
	// Update is called once per frame
	void Update () {
        if (fadsw == false)
        {
            nowTime[0] += Time.deltaTime;
            //点滅させる
            ten();
            if (nowTime[0] > endTime[0])
            {
                //フェードがオン
                fadsw = true;        
            }
        }
        else
        {
            //フェード
            nowTime[1] -= Time.deltaTime;
            touka = nowTime[1] / endTime[1];
            Cc = combol.color;
            Cc = canT.color;
            Cc = canout.effectColor;
            Cc.a = touka;
            combol.color = Cc;
            canT.color = Cc;
            canout.effectColor = Cc;
            if (nowTime[1] <0)
            {
                Destroy(gameObject);
            }
        }
	}
    //点滅させる
    void ten()
    {
        tent[0] += Time.deltaTime ;
        float h = 1 - tent[0] * 2;
        //徐々に変化する
        switch (number)
        {
            case 0:
                canT.color = new Color(h, h, h);
                break;
            case 1:
                canT.color = new Color(h, 0, 0);
                break;
            case 2:
                canT.color = new Color(0, h, 0);
                break;
            case 3:
                canT.color = new Color(0, 0, h);
                break;
        }
        //終了地点まで来たら
        if (tent[0] > tent[1])
        {
            switch (number)
            {
                case 0:
                    //白
                    canT.color = new Color(1, 1, 1);
                    break;
                case 1:
                    //赤
                    canT.color = new Color(1, 0, 0);
                    break;
                case 2:
                    //青
                    canT.color = new Color(0, 1, 0);
                    break;
                case 3:
                    //緑
                    canT.color = new Color(0, 0, 1);
                    break;
            }
            number++;
            if (number == 4)
            {
                number = 0;
            }
            tent[0] = 0;
        }
    }
}
