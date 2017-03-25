using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class kougeki : MonoBehaviour {
    //攻撃やｈｐの処理
    //ゲージなど
    //空の攻撃力の箱
    //0: fire ,1: reef ,2 :water, 3 :dark ,4: light, 5: kaihuku
    int[] kougekiryoku=new int[6];
    //自分のHP
    public float myhp;
    //自分の属性値それぞれ
    //ドロップ一個当たりの攻撃値
    //0: fire ,1: reef ,2 :water, 3 :dark ,4: light
    public int[] dropti;
    //回復一個あたりの回復値
    int kaihukuti;
    //敵スクリプト
    [SerializeField]
    enemys En;
    //ゲージ関係
    //ゲージイメージ
    public Image gageim;
    //ゲージ数値テキスト
    public Text gageT;
    //ゲージパーセント
    float gagepar;
    //最初のｈｐ
    float maxhp;
    //ターン
    [SerializeField]
    GlobalSp Gl;
    //音
    [SerializeField]
    audioSys oto;   
    //ゲージ回復
    float gagekai=0;
    //順々の攻撃
    float nowTime;
    float endTime;
    int suu;
    //攻撃パーティクル
    public GameObject[] partic = new GameObject[5];
    //パーティクルの生成ポイント
    public GameObject patisepos;

    //音声一度だけ
    private bool otosw;

    //回復ぬるぬる
    private bool kainul;
    private int kaiti;
    private float[] kaitime = new float[2];
    private float[] parsen=new float[6];
    //enemygageのamimation
    public Animator eneani;
    //enemy本体のanimation
    public Animator eneani2;
    //攻撃力を一時的に入れる箱
    private int[] kou = new int[5];
    //ダメージテキストprefab
    public GameObject dametec;
    //ダメージオブジェクト生成ポイント
    public GameObject seip;
    //ダメージ２倍か半減か通常かの判別
    //０が通常　１が２倍　２が半減
    private int[] dame = new int[5];

    //回復のテキスト
    public GameObject kaihukutob;
    private Text kaiT;
    private Animator kaianim;
	// Use this for initialization
	void Start () {
        otosw = false;
        suu = 0;
        nowTime = 0;
        endTime = 0.2f;
        gagekai = 0;
        myhp = 5000f;
        maxhp = myhp;
        //ここも多様化
        //ダンジョンはいるときに受け取る
   
        kaihukuti = 50;

        kainul = false;
        kaitime[0] = 0;
        kaitime[1] = 30;
        kaiT = kaihukutob.GetComponent<Text>();
        kaianim = kaihukutob.GetComponent<Animator>();
        kaiT.text = "";

	}
	
	// Update is called once per frame
	void Update () {
	//自分ゲージ
        gagepar = myhp / maxhp;
        gageim.fillAmount = gagepar;
        //hpテキスト
        gageT.text = (int)myhp+"/"+maxhp;
        //自分のHPが0になったらゲームオーバー
    
        switch (Gl.tarn)
        {
            case 1:
               
                
                    for(int i = 0; i < 5; i++)
                    {
                    if (otosw == false)
                    {
                        if (kougekiryoku[i] > 0)
                        {
                            //自分の音声
                            oto.SEo6();
                            otosw = true;
                        }
                    }
                    }
              
                
                //攻撃を発動
                hatudou();
              
                //攻撃を発動し終わったら敵のターン
            
            
                break;
            case 6:
              //コンティニュー
                    myhp += 10;
            if (myhp >= maxhp)
            {
                Gl.tarn = 0;
            }
                break;
                //ゲージの滑らかな増減
            case 10:
             
                    //何ループ目か
                    if (kaitime[0] <= kaitime[1])
                    {
                 
                    if (parsen[5] > 0)
                    {
               
                        //回復について
                        if (myhp + (int)parsen[5] < maxhp)
                        {
                            //回復がなければ増えないハズ
                            myhp += (int)parsen[5];
                        }
                        else
                        {
                            myhp = maxhp;
                        }
                    }
                    //攻撃について
                    for(int i = 0; i < 5; i++)
                    {
                        if (En.tekihp - (int)parsen[i] > 0)
                        {
                            En.tekihp = En.tekihp - (int)parsen[i];
                        }
                        else
                        {
                            En.tekihp = 0;
                            break;
                        }
                    }
                        kaitime[0]++;
                    }
                    else
                    {

                        root10();

                    }
              
         
                break;
        }
      
     
  
	}
    //ルート１０のsyokika処理
    void root10()
    {
        if (parsen[5] > 0)
        {
            
                int now = (int)(kaitime[1]-kaitime[0]);
                myhp += parsen[5] * now;
            //回復の音と回復値のテキスト表示
                kaiT.text = "";
                kaianim.SetBool("sw", false);
        }
        if (En.tekihp > 0)
        {
            //敵のターン
            Gl.tarn = 2;
        }
        else
        {
            Gl.tarn = 7;
        }
        eneani.SetBool("sw", false);
        eneani2.SetBool("enesw", false);
        for (int i = 0; i < kou.Length; i++)
        {
            if (kou[i] > 0)
            {
                oto.tekiO(En.zokusei, 1);
                break;
            }
      
        }
        for (int i = 0; i < kou.Length; i++)
        {
        
            kou[i] = 0;
        }
        for(int j = 0; j < parsen.Length; j++)
        {
            parsen[j] = 0;
        }
    
        kaiti = 0;
        kaitime[0] = 0;
        kainul = false;
    }
    //受け取ったオブジェクトのタグを調べて攻撃に加算
    public void tagcheckkou(GameObject tago)
    {
       
        dropti = new int[5];
        //ドロップ一個当たりの攻撃力
        dropti[0] = 100;
        dropti[1] = 100;
        dropti[2] = 100;
        dropti[3] = 100;
        dropti[4] = 100;
  
            switch (tago.tag)
            {
                //闇
                case "dark":
                    switch (En.zokusei)
                    {
                        //攻撃力２倍
                        case 4:
                            kougekiryoku[3] = kougekiryoku[3] + (dropti[3] * 2);
                            dame[3] = 1;
                            break;
                        //通常
                        default:
                            kougekiryoku[3] = kougekiryoku[3] + dropti[3];
                            dame[3] = 0;
                            break;
                    }

                    break;
                //光
                case "lights":
                    switch (En.zokusei)
                    {
                        //攻撃力２倍
                        case 3:
                            kougekiryoku[4] = kougekiryoku[4] + (dropti[4] * 2);
                            dame[4] = 1;
                            break;
                        //通常
                        default:
                            kougekiryoku[4] = kougekiryoku[4] + dropti[4];
                            dame[4] = 0;
                            break;
                    }

                    break;
                //回復
                case "kaihuku":
                    //パーティの回復力に応じて
                    kougekiryoku[5] = kougekiryoku[5] + kaihukuti;
                    break;
                //火
                case "fire":

                    switch (En.zokusei)
                    {
                        //攻撃２倍
                        case 1:
                            kougekiryoku[0] = kougekiryoku[0] + (dropti[0] * 2);
                            dame[0] = 1;
                            break;
                        //攻撃半分

                        case 2:
                            kougekiryoku[0] = kougekiryoku[0] + (int)(dropti[0] / 2);
                            dame[0] = 2;
                            break;
                        //通常
                        default:
                            kougekiryoku[0] = kougekiryoku[0] + dropti[0];
                            dame[0] = 0;
                            break;
                    }

                    break;
                //リーフ
                case "reef":
                    switch (En.zokusei)
                    {
                        //攻撃力２倍
                        case 2:
                            kougekiryoku[1] = kougekiryoku[1] + (dropti[1] * 2);
                            dame[1] = 1;
                            break;
                        //攻撃力半分
                        case 0:
                            kougekiryoku[1] = kougekiryoku[1] + (int)(dropti[1] / 2);
                            dame[1] = 2;
                            break;
                        //通常
                        default:
                            kougekiryoku[1] = kougekiryoku[1] + dropti[1];
                            dame[1] = 0;
                            break;
                    }

                    break;
                //水
                case "waters":
                    switch (En.zokusei)
                    {
                        //攻撃２倍
                        case 0:
                            kougekiryoku[2] = kougekiryoku[2] + (dropti[2] * 2);
                            dame[2] = 1;
                            break;
                        //攻撃半分
                        case 1:
                            kougekiryoku[2] = kougekiryoku[2] + (int)(dropti[2] / 2);
                            dame[2] = 2;
                            break;
                        //通常
                        default:
                            kougekiryoku[2] = kougekiryoku[2] + dropti[2];
                            dame[2] = 0;
                            break;
                    }

                    break;
            }
        
    
    

    }
    //蓄えた攻撃を実際に実行
    void hatudou()
    {
        nowTime += Time.deltaTime;
        if (nowTime > endTime)
        {

            if (kougekiryoku[suu] > 0&&suu<kougekiryoku.Length)
            {
                switch (suu)
                {
                        //回復ドロップだった場合
                    case 5:
                        kaiti = kougekiryoku[suu];

                        parsen[5] = kaiti / kaitime[1];
                        if (myhp < maxhp)
                        {
                            kainul = true;

                            //回復の音と演出
                            kaiT.text = "+" + kaiti;
                            kaianim.SetBool("sw", true);
                            oto.SEo9();
                        }
                        else
                        {
                            parsen[5] = 0;
                        }
                       
                        
                     
                    
                        break;
                    default:
                        //それ以外のドロップ
                        //敵の音声かゲージがゆれるか
                        eneani.SetBool("sw", true);
                        eneani2.SetBool("enesw", true);
                        //敵のｈｐが減る
                        
                        kou[suu] = kougekiryoku[suu];
                        parsen[suu] = kou[suu] / kaitime[1];
                            //En.tekihp = En.tekihp - kougekiryoku[suu];
                        kainul = true;
                //攻撃の音
                              oto.SEkou(suu);
                        //ダメージテキストの生成
                              GameObject[] g=new GameObject[2];
                              Text gt;
                             
                              Animator ee;

                        //生成
                              Vector2 s = seip.transform.position;
                              g[0] = (GameObject)Instantiate(dametec, s, dametec.transform.rotation);
                              g[1] = g[0].transform.FindChild("coT").gameObject;
                              gt = g[1].GetComponent<Text>();
                              ee = g[1].GetComponent<Animator>();
                        //弱点か半減かによってダメージテキストの大きさを変える
                              switch (dame[suu])
                              {
                                  case 0:
                                      break;
                                  case 1:
                                      ee.SetBool("sw0", true);
                                      break;
                                  case 2:
                                      ee.SetBool("sw1", true);
                                      break;
                              }
                              
                 
                     
                            
                   
                              gt.text = kougekiryoku[suu] + "";
                        //属性によってテキストのカラー変換
                              gt.color = tekich(suu);
                        //テキストに攻撃力を代入
                //パーティクル生成
                Instantiate(partic[suu], patisepos.transform.position, partic[suu].transform.rotation);
                        break;
                }

            }else
            {
                if (suu < 5)
                {
                    kou[suu] = 0;
                }
            }
            suu++;
            if (suu == kougekiryoku.Length)
            {
                for (int i = 0; i < kougekiryoku.Length; i++)
                {
                    kougekiryoku[i] = 0;
                }
                //たぶん入ることのない
                if (kainul == false)
                {
                    if (En.tekihp > 0)
                    {
                        Gl.tarn = 2;
                    }
                    else
                    {
                        Gl.tarn = 7;
                    }
                }
                else
                {
                    //緩やかなゲージの処理
                    Gl.tarn = 10;
                }
                otosw = false;
                suu = 0;
            }
            nowTime = 0;
        }
      

       
    }

    //テキストのカラー変換
    private Color tekich(int suu)
    {
        Color nn;
        switch (suu)
        {
            case 0:
                //赤
                nn = new Color(1, 0.274f, 0.274f);
                break;
            case 1:
                //緑
                nn = new Color(0.537f, 1, 0.537f);
                break;
            case 2:
                //青
                nn = new Color(0.498f, 0.749f, 1);
                break;

            case 3:
                //闇
                nn = new Color(0.654f, 0.313f, 1);
                break;
            case 4:
                //光
                nn = new Color(1, 1, 0.470f);
                break;
            default:
                nn = new Color(1, 1, 1);
                break;
            
        }
        return nn;
    }
  
}
