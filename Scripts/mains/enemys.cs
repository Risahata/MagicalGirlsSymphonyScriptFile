using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class enemys : MonoBehaviour {
    //敵の属性や攻撃力などを格納するクラス
    //倒すごとに変化
    //総合

    //属性　0:fire,1:reef,2:water,3:dark,4:lights
    public int zokusei;
    //ターンごとの属性
    private int[] zokuseitarn = new int[5]{
    0,2,1,4,3
    };
    //敵の属性ごとの攻撃パーティクル
    public GameObject[] tekip = new GameObject[5];
    //パーティクル生成ポイント
    public GameObject patsei;
    //攻撃力技３種
    private int[,] kougekiy=new int[5,3]{
        {500,200,300},
        {500,300,400},
        {600,400,500},
        {500,700,600},
        {600,750,1000}
    };
    //攻撃ランダム数
    private int kouran;
    //防御力
    public int bougyo;
    //hp
    public float tekihp;
    //敵ｈｐイメージ
    public Image tekiIm;
    //敵maxhp
    private float[] maxhp=new float[5]{
    2500,2700,2900,3000,5000
    };
  
    //hpのパーセンテージ
    private float parsen;
    //何回待つか
    public int wait;
    private int[] maxwait=new int[5]{
    2,3,3,2,2
    };

    public Text tarnT;
    [SerializeField]
    GlobalSp Gl;
    //プレイヤーのほうの
    [SerializeField]
    kougeki Ko;
    //攻撃音
    [SerializeField]
    audioSys aud;
    //enemyのspriteレンダラーをせんたく
    public SpriteRenderer enemysp;
    //enemyのｈｐimage
    public Image[] eneIm=new Image[3];
    private Color[] enecolor = new Color[3];
    //変える絵
    public Sprite[] pic = new Sprite[5];

    //変えるときのフェード
    private float nowTime;
    private float endTime;
    private float touka;
    private Color Cc;
    //何バトルめかのtext
    public Text numberT;
    //nowbattleカウントアップ
    private bool nowbatc;
    //属性ごとの色
    private Color[] zokusyoku = new Color[5];
   
    //プレイヤーゲージ
    public Animator playani;
    //時間
    private float[] playergagetime = new float[2];
    //パーセンテージ
    private float parsene;
    //プレイヤーへのダメージを表示するtext
    public Text playerdamageT;
    public Animator damageani;
    
	// Use this for initialization
	void Start () {
        zokusei = zokuseitarn[0];
        enemysp.sprite = pic[0];
        tekihp = maxhp[0];
        wait = maxwait[0];
        //フェード
        nowTime = 1;
        endTime = 1;
        touka = nowTime / endTime;
        Cc = enemysp.color;
        for(int i = 0; i < eneIm.Length; i++)
        {
            enecolor[i] = eneIm[i].color;
            enecolor[i].a = touka;
        }
        Cc.a = touka;
        numberT.text = "";
        nowbatc = false;
        colorset();
        eneIm[1].color = zokusyoku[zokusei];
        //何フレームで行うか
        playergagetime[0] = 0;
        playergagetime[1] = 30;
        playerdamageT.text = "";
    }
	
	// Update is called once per frame
	void Update () {
        if (Gl.nowbattle <= Gl.battlesuu)
        {
            parsen = tekihp / maxhp[Gl.nowbattle];
            tarnT.text = "あと" + (wait-1);
            tekiIm.fillAmount = parsen;
        }
        else
        {
            tarnT.text = "";
        }
        switch (Gl.tarn)
        {
            case 7:
                playerdamageT.text = "";
                tekihp0();
                break;
            case 2:
                playerdamageT.text = "";
                tekitarn();
                break;
            //敵の攻撃でplayerのゲージがゆれるシーン
            case 12:
          
                if (playergagetime[0] <= playergagetime[1])
                {
                    Ko.myhp = Ko.myhp - (int)parsene;
                    playergagetime[0]++;
                }
                else
                {
                    end();
                }
                ////myhpがなくなったらゲームオーバー
                //if (Ko.myhp - kougekiy[Gl.nowbattle, kouran] <= 0)
                //{

                //    Ko.myhp = 0;
                //    //ゲームオーバー
                //    Gl.tarn = 4;

                //}
                //else
                //{
                //    //プレイヤーのhpを減らす

                //    //ゲージがゆれる
                //    Ko.myhp = Ko.myhp - kougekiy[Gl.nowbattle, kouran];


                //    //動作を終える
                //    Gl.tarn = 0;

                //}
                break;
            default:
                playerdamageT.text = "";
                break;
        }
        
     
	}
    //どうさを終える処理
    void end()
    {
        //myhpがなくなったらゲームオーバー
        if (Ko.myhp <= 0)
        {

            Ko.myhp = 0;
            //ゲームオーバー
            Gl.tarn = 4;

        }
        else
        {
            //プレイヤーのhpを減らす

       


            //動作を終える
            Gl.tarn = 0;

        }
        //味方の声
        aud.SEo8();
        parsene = 0;
        playani.SetBool("sw", false);
        damageani.SetBool("sw", false);
        playergagetime[0] = 0;
    }
    //敵のターンだたら
    void tekitarn()
    {
        //敵のターンだったら
  
            wait--;
            //何回ごとか？
            if (wait <= 0)
            {
                //プレイヤーに攻撃をして
                kouran = Random.Range(0, 2);
                //ターンをリセット
                wait = maxwait[Gl.nowbattle];
            //敵の攻撃パーティクル
            Instantiate(tekip[zokusei],patsei.transform.position,tekip[zokusei].transform.rotation);
            //敵の攻撃音
            aud.SEkou(zokusei);
            aud.SEo2();
            aud.tekiO(zokusei,0);

            playani.SetBool("sw", true);
            damageani.SetBool("sw", true);
            playerdamageT.text = "-"+kougekiy[Gl.nowbattle, kouran]  ;
            //パーセンテージ計算
            parsene = kougekiy[Gl.nowbattle, kouran] / playergagetime[1];

            Gl.tarn = 12;

            }
            else
            {
                Gl.tarn = 0;
            }


        
    }
    //敵のｈｐがゼロ
    void tekihp0()
    {
        //敵を倒したら
        if (tekihp <= 0)
        {
            //フェードで敵を消す
            nowTime -= Time.deltaTime;
            touka = nowTime / endTime;

            Cc = enemysp.color;

            Cc.a = touka;
            enemysp.color = Cc;
            for(int i = 0; i < eneIm.Length; i++)
            {
                enecolor[i] = eneIm[i].color;
                enecolor[i].a = touka;
                eneIm[i].color = enecolor[i];
            }
            if (nowTime < 0)
            {
                if (nowbatc == false)
                {
                    Gl.nowbattle++;
                    
                    nowbatc = true;
                }
             
                //この敵が最後以外の敵だったら次の敵へ
                if (Gl.battlesuu < Gl.nowbattle)
                {
                    //最後の敵だったらクリアへ
                    Gl.tarn = 3;
                }
                else
                {
                    numberT.text = (Gl.nowbattle + 1) + "/" + (Gl.battlesuu + 1);
                    //次の敵へ
                    StartCoroutine("feteki");
                }
          
          
            }
           

        }
    }
    //属性ごとの色の設定メソッド
    void colorset()
    {
        //炎
        zokusyoku[0] = new Color(1,0.274f,0.274f);
        //風
        zokusyoku[1] = new Color(0.498f, 1, 0.498f);
        //水
        zokusyoku[2] = new Color(0.498f, 0.749f, 1);
        //闇
        zokusyoku[3] = new Color(0.682f, 0.372f, 1);
        //光
        zokusyoku[4] = new Color(1,1,0.498f);
    }
    //次の敵へ
    IEnumerator feteki() {
       
 
   
        yield return new WaitForSeconds(1);
      
        numberT.text = "";
     
        nowTime = endTime;
        //敵のＨｐ切り替え
        tekihp = maxhp[Gl.nowbattle];
        //敵のイメージ切り替え
        enemysp.sprite = pic[Gl.nowbattle];
        //敵の属性切り替え
        zokusei = zokuseitarn[Gl.nowbattle];
        //表示
        touka = nowTime / endTime;
        Cc = enemysp.color;
        Cc.a = touka;
        enemysp.color = Cc;
        for(int i = 0; i < eneIm.Length; i++)
        {
            enecolor[i] = eneIm[i].color;
            enecolor[i].a = touka;
            eneIm[i].color = enecolor[i];
        }
        //属性によって色変える
        eneIm[1].color=zokusyoku[zokusei];
        wait = maxwait[Gl.nowbattle];
        nowbatc = false;
        //自分のターンへ
        Gl.tarn = 0;
    }

}
