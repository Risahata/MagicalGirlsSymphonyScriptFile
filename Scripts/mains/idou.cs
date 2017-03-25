using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
public class idou : MonoBehaviour {
    //動かすドロップ
    GameObject poinm;
    SpriteRenderer poisl;
    private Vector3 screenPoint;
    private Vector3 offset;
    //[縦、横]
    private GameObject[,] haiti = new GameObject[5, 6];
    //範囲
    private float[,] haitiXbegin = new float[5, 6];
    private float[,] haitiXend = new float[5, 6];
    private float[,] haitiYbegin = new float[5, 6];
    private float[,] haitiYend = new float[5, 6];
    //動かす位置の親
    private GameObject oya;
    GameObject ko;
    //子供の名前を格納する箱
    private string koname;
    //gameobjectname
    private string[,] obname = new string[5, 6]{
    {"a0","a1","a2","a3","a4","a5"},
    {"b0","b1","b2","b3","b4","b5"},
    {"c0","c1","c2","c3","c4","c5"},
    {"d0","d1","d2","d3","d4","d5"},
    {"e0","e1","e2","e3","e4","e5"},
    };
    //消す対象オブジェクトを入れる箱
    //縦用
    private GameObject[,] deshako;
    //ドロップの数
    int hakosu;
    //最大コンボ数
    int combosu;

    public static int bsu = 0;
    //攻撃面のスクリプト
    [SerializeField]
    kougeki Kou;
    [SerializeField]
    GlobalSp Gl;
    [SerializeField]
    seisei Se;
    //音
    [SerializeField]
    audioSys aud;
    //ドロップカラー関係
    //ドロップカラー変更の時間 0:now 1:end
    private float[] dcTime = new float[2];
    //ドラッグしてましたよｓｗ
    private bool drsw;
    //生成するcomboオブジェクト
    public GameObject comboobj;

    public int botanstate;
    //デストロイしたところにパーティクルを生成
    public GameObject pat;
    //初期ポジ
    Vector3 syokipos;
    //スキル貯め
    [SerializeField]
    skill sk;
    //スムージングsw
    private bool smsw;
	// Use this for initialization
	void Start () {
        botanstate = 0;
        drsw = false;
        hakosu = 0;
        combosu=0;  
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                //オブジェクトを見つける
                haiti[i, j] = GameObject.Find(obname[i, j]);
                haitiXbegin[i,j] = haiti[i, j].transform.position.x + 0.3f;
                haitiXend[i, j] = haiti[i, j].transform.position.x + 0.7f;
                haitiYbegin[i, j] = haiti[i, j].transform.position.y - 0.3f;
                haitiYend[i, j] = haiti[i, j].transform.position.y - 0.7f;
            }
        }
        dcTime[0] = 0;
        dcTime[1] = 0.5f;
        smsw = false;
	}	
	// Update is called once per frame
	void Update () {
        //自分のターンだったら
        if (Gl.tarn == 0&&seisei.seihan==false)
        {
            switch (botanstate)
            {
                case 0:
                    //処理行っている途中は押されないように
                    //waitじゃなかったら
                    if (Input.GetMouseButtonDown(0))
                    {
                    
                        //コンボ数初期化
                        bsu = 0;
                        LeftClick();
                        //botanstate = 1;

                    }
                    break;
                case 1:
                    //ドラッグ
                    //動かす動作
                    //オブジェクトの位置交換
                    if (Input.GetMouseButton(0))
                    {
                        LeftDrag();
                    }
                    //スムーズな動き
                    if (smsw == true)
                    {
                  
                    }
                    smuziong();
                    //判定
                    if (Input.GetMouseButtonUp(0))
                    {

                        //ドロップカラーリセット
                        drres();
                        //ドロップした後で
                        if (drsw == true)
                        {

                            LeftUp();

                            seisei.seihan = true;
                            drsw = false;
                         
                            //スキルがたまる
                            for (int i = 0; i < sk.nowskill.Length; i++)
                            {
                                sk.nowskill[i]++;
                            }
                           
                        }
                        else
                        {                          
                            //入れ替えてなかったら
                            //もとの配置に戻す
                            if (poinm != null&&poinm.tag!="pat")
                            {
                                poinm.transform.position = syokipos;
                                poinm = null;
                            }
                            botanstate = 0;
                        }
                    }
               
                    break;
            }
        
         
        }
	}
    //rayを飛ばす
    public static GameObject GetCurrentHitCollider()
    {

        //メインカメラ上のマウスカーソルのある位置からRayを飛ばす
       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            GameObject s = hit.collider.gameObject;


            return s;
        }
        else
        {
            return null;
        }

    }
    //押されたとき（タップしたとき）
    void LeftClick()
    {
        //動かすゲームオブジェクトを取得
        poinm = GetCurrentHitCollider();
        //オブジェクトがあったら
        if (poinm != null&&poinm.tag!="pat")
        {    
            poisl = poinm.GetComponent<SpriteRenderer>();
            //押し始めのポイント取得
            //親オブジェクト取得
            oya = poinm.transform.parent.gameObject;
            //持ってるドロップのれいやーを一番上に
            poisl.sortingOrder = 1;
            this.screenPoint = Camera.main.WorldToScreenPoint(poinm.transform.position);
            this.offset = poinm.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            botanstate = 1;
        }
    }
    //ドロップを動かす
    void LeftDrag()
    {
    
        //オブジェクトがあったらなおかつ範囲内だったら
        if (poinm != null&&poinm.tag!="pat")
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
            if (currentPosition.y > 0.7f)
            {
                poinm.transform.position = new Vector2(currentPosition.x, 0.7f);
            }
            else if (currentPosition.x < -3)
            {
                poinm.transform.position = new Vector2(-3, currentPosition.y);
            }
            else if (currentPosition.x > 2)
            {
                poinm.transform.position = new Vector2(2, currentPosition.y);
            }
            else if (currentPosition.y < -3.5f)
            {
                poinm.transform.position = new Vector2(currentPosition.x, -3.5f);
            }
            else
            {
                //今のポジションが範囲内だったら
                poinm.transform.position = currentPosition;
            }
            //動かしているドロップの判定範囲
            //範囲X
            float poinmXbegin = poinm.transform.position.x + 0.4f;
            float poinmXend = poinm.transform.position.x + 0.8f;
            //範囲Y
            float poinmYbegin = poinm.transform.position.y - 0.4f;
            float poinmYend = poinm.transform.position.y - 0.8f;
            //poinmと配置場所の場所比較
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    //自分の初期ポジ以外で
                    if (haiti[i, j].transform.position!=oya.transform.position)
                    {
                        //範囲に入っていたとき
                        if ((poinmYbegin > haitiYbegin[i, j] && haitiYbegin[i, j] > poinmYend && (haitiXbegin[i, j] < poinmXend &&haitiXend[i, j] > poinmXbegin)) ||
                            (poinmYbegin > haitiYend[i, j] && haitiYend[i, j] > poinmYend && (haitiXbegin[i, j] < poinmXend && haitiXend[i, j] > poinmXbegin)))
                        {
                            //そこのポジションに入っている子オブジェクトを探す                          
                            foreach (Transform child in haiti[i, j].transform)
                            {
                                koname = child.name;                          
                            }
                            ko = haiti[i, j].transform.FindChild(koname).gameObject;
                            //入れ替えたよというｓｗがtrue
                            drsw = true;
                            //入れ替える音
                            aud.SEo0();
                            ko.transform.parent = oya.transform;
                            oya = haiti[i, j];  
                            poinm.transform.parent = oya.transform;
                        }
                    }
                    else
                    {
                        //自分の初期ポジを代入
                        syokipos = haiti[i, j].transform.position;
                    }
                }
            }
            //ドロップ中のドロップカラーの変更
            drcolor();
        }
    }
    //ドロップカラー変更
    void drcolor()
    {
        dcTime[0] += Time.deltaTime;
        //周期で変動
        switch (poinm.tag)
        {
            //とりあえず固定
            case "fire":
                poisl.color = new Color(0.5f, 0, 0);
                break;
            case "reef":
                poisl.color = new Color(0, 0.5f, 0);
                break;
            case "waters":
                poisl.color = new Color(0, 0.4f, 0.6f);
                break;
            case "dark":
                poisl.color = new Color(0.5f, 0, 0.5f);
                break;
            case "lights":
                poisl.color = new Color(0.5f, 0.5f, 0);
                break;
            case "kaihuku":
                poisl.color = new Color(1, 0.4f, 0.7f);
                break;
            default:
                break;

        }
        
        if (dcTime[0] > dcTime[1])
        {
            dcTime[0] = 0;
        }
    }
    //  スムーズな動きのスクリプト
    void smuziong()
    {
        for (int i = 0; i < 5;i++ )
        {
            for (int j = 0; j < 6; j++)
            {
                GameObject m;
                string nn="";
                foreach (Transform child in haiti[i, j].transform)
                {
                    //child is your child transform
                    nn = child.name;
                }
                if (nn != "")
                {
                    m = haiti[i, j].transform.FindChild(nn).gameObject;
                    m.transform.position = Vector2.Lerp(m.transform.position, haiti[i, j].transform.position, 10f * Time.deltaTime);
                }


            }
        }
    }
    //指が上がった時
   void LeftUp()
    {
        if (poinm != null&&poinm.tag!="pat")
        {
            //消す対象オブジェクトをnew
            deshako = new GameObject[10,60]; 
            poinm.transform.position = oya.transform.position;
            poisl.sortingOrder = 0;
            //縦か横に連続して同じ色があるか見る
            hantei(haiti,hakosu,combosu,deshako,koname);
        }
    }
    //ドロップカラーのリセット
   void drres()
   {
       if (poisl != null)
       {
           poisl.color = new Color(1, 1, 1);
       }
   }
    //判定
   public void hantei(GameObject[,] haiti, int　hakosu,int combosu, GameObject[,] deshako, string koname)
   {
        //hakosu
        int[] kanri = new int[30];
        GameObject[,] haitiko = new GameObject[5, 6];
       for (int i = 0; i < 5; i++)
       {
           for (int j = 0; j < 6; j++)
           {
               foreach (Transform child in haiti[i, j].transform)
               {
                   koname = child.name;
               }
               haitiko[i, j] = haiti[i, j].transform.FindChild(koname).gameObject;
           }
       }
       //haiti[i,j] 動かしたところ
       //deshakoの配列の長
       hakosu= 0;
       combosu = 0;
        //縦にそろってた時
        for (int j = 0; j < haitiko.GetLength(1); j++)
        {
            bool itido = false;
            for (int i = 0; i < haitiko.GetLength(0); i++)
            {

                if (itido == false)
                {
                    int suu = 0;
                    int c = i + 1;
                    bool sw = false;
                    //縦探索
                    if (i == 0 || i == 1 || i == 2)
                    {
                        while (sw == false && c < 5)
                        {
                            if (haitiko[i, j].tag == haitiko[c, j].tag)
                            {
                                suu++;
                                if (suu > 1)
                                {
                                    if (itido == false)
                                    {

                                        deshako[combosu, hakosu] = haitiko[i, j];
                                        hakosu++;
                                        deshako[combosu, hakosu] = haitiko[i + 1, j];
                                        hakosu++;
                                        //コンボが＋＋；
                                        //コンボが決まっているので列は探索しない
                                        itido = true;
                                    }
                                    deshako[combosu, hakosu] = haitiko[c, j];
                                    hakosu++;

                                }
                            }
                            else
                            {
                                //脱出
                                sw = true;
                            }
                            c++;
                        }

                    }

                }



            }
            if (itido == true)
            {
                //はこの長さを格納
                kanri[combosu] = hakosu;
                combosu++;
                //箱の要素数リセット
                hakosu = 0;
            }
        }
       //ただの横探索。
       for (int i = 0; i < 5; i++)
       {
           bool itido = false;
           for (int j = 0; j < 6; j++)
           {
               if (itido == false)
               {
                   int c = j + 1;
                   bool sw = false;
                   //３個以上並ぶとなので
                   int suu = 0;
                    switch (j)
                    {
                        case 0:
                            //とりあえず
                        case 1:
                        case 2:
                        case 3:
                            while (sw == false && c < 6)
                            {
                                if (haitiko[i, j].tag == haitiko[i, c].tag)
                                {
                                    suu++;
                                    if (suu > 1)
                                    {
                                        if (itido == false)
                                        {
                                          
                                            deshako[combosu, hakosu] = haitiko[i, j];
                                            hakosu++;
                                            deshako[combosu, hakosu] = haitiko[i, j+1];
                                            hakosu++;
                                            //コンボが＋＋；

                                            //コンボが決まっているので列は探索しない

                                            itido = true;
                                        }
                                        deshako[combosu, hakosu] = haitiko[i, c];
                                        hakosu++;
                                    }
                                }
                                else
                                {
                                    sw = true;
                                }
                                c++;
                            }
                            break;
                    }
           
                }
              
            }
            if (itido == true)
            {

                //はこの長さを格納
                kanri[combosu] = hakosu;
                combosu++;
                //箱の要素数リセット
                hakosu = 0;
            }
        }
        //オブジェクトを消す
       bool otosw=false;
        for (int a = 0; a < combosu; a++)
        {
            bool sw=false;
            for (int b = 0; b < kanri[a]; b++)
            {
            //一回だけ
                if (sw == false)
                {
                    //0のところがnullじゃないやつ
                    if (deshako[a, b] != null)
                    {
                        //コンボ数を数える
                        //これが実際に表示される奴
                        GameObject[] se=new GameObject[2];
                        Text te;
                        se[0]=(GameObject)Instantiate(comboobj, deshako[a, b + 1].transform.position, deshako[a, b + 1].transform.rotation);
                        //このゲームオブジェクトの中身を調べ、そこのテキストにbusを代入
                        se[1] = se[0].transform.FindChild("coT").gameObject;
                        te = se[1].GetComponent<Text>();
                      
                        te.text = (bsu+1)+"combo";
                        bsu++;
                        //消したよというのをお知らせ
                        seisei.kesu = true;
                        //コンボの表示
                        //初回だけ音
                        if (otosw == false)
                        {
                            aud.SEo1();
                            otosw = true;
                        }
                    }
                    sw = true;
                }
                if (deshako[a, b] != null)
                {
                    //それぞれタグごとに攻撃力に加算してから
                    Kou.tagcheckkou(deshako[a, b]);
                    Instantiate(pat, new Vector2((deshako[a, b].transform.position.x+0.5f),(deshako[a,b].transform.position.y-0.5f)), deshako[a, b].transform.rotation);
                    //消す
                    Destroy(deshako[a, b]);
                }
            }

        }
        botanstate = 0;


    }    
     
}
