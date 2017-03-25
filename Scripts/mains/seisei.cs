using UnityEngine;
using System.Collections;

public class seisei : MonoBehaviour {
    //ドロップ
    public GameObject[] sp=new GameObject[6];
    //ドロップのランダム
    private int dransu;
    //[縦、横]
    private GameObject[,] haiti=new GameObject[5,6];   
    //gameobjectname
    private string[,] obname = new string[5, 6]{
    {"a0","a1","a2","a3","a4","a5"},
    {"b0","b1","b2","b3","b4","b5"},
    {"c0","c1","c2","c3","c4","c5"},
    {"d0","d1","d2","d3","d4","d5"},
    {"e0","e1","e2","e3","e4","e5"},
    };
    //空のオブジェクト
    private GameObject kara;
    //個々のドロップ判別数
    private int suu;
    //生成ポジションのオブジェクト
    private GameObject[] seipos=new GameObject[6];
    string cn;
    //オブジェクト生成した後のコンボｓｗ
    bool combosw;
    //消す対象オブジェクト
    private GameObject[,] kesuob;
    int hakosu=0;
    int combosu;
    [SerializeField]
    idou MoveI;
    [SerializeField]
    GlobalSp Gl;
    //時間
    private float nowTime;
    private float endTime;
    string koname;
    //生成したと教えるsw
    bool seiseisw;
    public static bool kesu;
    //upし終わって判定入ると教えるsw
    public static bool seihan;
	// Use this for initialization
	void Start () {
        kesu = false;
        seiseisw = false;
        nowTime = 0;
        endTime = 0.5f;
        suu=0;
        //最初のランダム配置
        //スポーン
        //たて
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                //オブジェクトを見つける
                haiti[i, j] = GameObject.Find(obname[i,j]);
                //haitiの下に産む
                dransu = Random.Range(0, 6);           
                kara = (GameObject)Instantiate(sp[dransu], haiti[i, j].transform.position, haiti[i, j].transform.rotation);
                kara.name = "drop" + suu;
                kara.transform.parent = haiti[i, j].transform;
                suu++;
            }
        }
        //生成ポジションの取得
        for (int h = 0; h < seipos.Length; h++)
        {
            seipos[h] = GameObject.Find("seipos" + h);
        }
        combosw = false;
        seihan = false;
	}
	
	// Update is called once per frame
	void Update () {
        //upした後
        if (seihan == true)
        {
            //何度も
            downdrop();
        }
    
        if (combosw == true)
        {
            nowTime += Time.deltaTime;
            if (nowTime > endTime)
            {
                kesuob = new GameObject[10,60];
                //少し時間を置いて
                //コンボできるオブジェクトがあるか？
                //消す
                MoveI.hantei(haiti,hakosu,combosu,kesuob,koname);
                //消したというのがかえってこなければ
                if (kesu == false)
                {
                    seihan = false;    
                    Gl.tarn = 1;
                }
                else
                {
                    kesu = false;
                }
                nowTime = 0;
                combosw = false;
            }
        }
	}
    //下にダウン
    public void downdrop()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                //ドロップがはいっていない
                if (haiti[i, j].transform.childCount <= 0)
                {
                    //haiti[0,j]以外で
                    if (i != 0)
                    {
                        //上のオブジェクトを持ってくる
                        GameObject ko;
                        foreach (Transform child in haiti[i - 1, j].transform)
                        {
                            //child is your child transform
                            cn = child.name;
                        }
                        ko = haiti[i - 1, j].transform.FindChild(cn).gameObject;
                        ko.transform.parent = haiti[i, j].transform;
                    }
                    else
                    {
                        //haiti[0,j]のとき新たに挿入
                        //オブジェクト生成
                        obsei(i, j);
                        
                    }


                }
                else
                {
                    //childが入っていて
                    //生成したよというスイッチが起動したら
                    //ドロップが移動する作業
                    //childとparentのポジション比較
                    GameObject[,] ko;
                    //生成した後一回だけ通る
               
                        ko = new GameObject[5, 6];
                        foreach (Transform child in haiti[i, j].transform)
                        {
                            //child is your child transform
                            cn = child.name;


                        }
                        ko[i, j] = haiti[i, j].transform.FindChild(cn).gameObject;
                        seiseisw = false;

                    
                   
                    //haiti[i,j]に近づける
                    if (haiti[i, j].transform.position.y < ko[i,j].transform.position.y)
                    {
                        ko[i,j].transform.position = Vector3.Lerp(ko[i,j].transform.position, haiti[i, j].transform.position, 8f * Time.deltaTime);

                    }
                    else
                    {
                        ko[i,j].transform.position = haiti[i, j].transform.position;
                        //すべて移動しおわったら
                        combosw = true;
                    }
                
                }


            }
        }
     
    }
    //オブジェクトを生成
    void obsei(int i,int j)
    {

        dransu = Random.Range(0, 6);
        //どこか上のほうにとりあえずうむ
        kara = (GameObject)Instantiate(sp[dransu], seipos[j].transform.position, seipos[j].transform.rotation);
        kara.name = "drop" + suu;
        kara.transform.parent = haiti[i, j].transform;
        suu++;
        seiseisw = true;
    }
}
