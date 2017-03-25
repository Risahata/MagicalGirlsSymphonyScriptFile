using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class skill : MonoBehaviour {
    public GameObject[] patiobj = new GameObject[6];
    private SpriteRenderer[] patisp=new SpriteRenderer[6];

    //スキルターン
    private int[] skilltarn = new int[6]{
    2,5,1,3,7,4
    };
    public GameObject skillC;
    //テキスト
    private GameObject sknameo;
    private Text skT;
    //private string[] skst = new string[6];
    //実際に今たまっているターン数
    public int[] nowskill = new int[6];
    //渡す数
    private int suu;
    //canbas表示sw
    bool cansw;
    [SerializeField]
    audioSys aud;
    //今が自分の出番かどうか
    [SerializeField]
    GlobalSp Gl;
    [SerializeField]
    idou id;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < nowskill.Length; i++)
        {
            nowskill[i] = 0;
            patisp[i] = patiobj[i].GetComponent<SpriteRenderer>();
       
        }
        sknameo = skillC.transform.FindChild("tnT").gameObject;
        skT = sknameo.GetComponent<Text>();

        skillC.SetActive(false);
        suu = 0;
        cansw = false;
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < skilltarn.Length; i++)
        {
            if (nowskill[i] >= skilltarn[i])
            {

                //spriteが輝く
                patisp[i].color = new Color(1, 1, 1);
            }
            else
            {
                //sprite灰色のまま
                patisp[i].color = new Color(0.5f, 0.5f, 0.5f);
            }
        }
        if (Gl.tarn == 0&&id.botanstate==0)
        {
            click();
        }
   
          
        //canvas
            skillC.SetActive(cansw);

	}
    void click()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //個々のスキルがたまっていたら
            if (cansw == false)
            {
                GameObject point = idou.GetCurrentHitCollider();
                if (point != null && point.tag == "pat")
                {
                    switch (point.name)
                    {
                        case "par0":
                            suu = 0;
                            skT.text = @"
<Size=50>炎の誓い</Size>

敵に1000のダメージ";
                            skillhan(suu);
                            break;
                        case "par1":
                            suu = 1;
                            skT.text = @"
<Size=50>風の誓い</Size>

敵に1000のダメージ";
                            skillhan(suu);
                            break;
                        case "par2":
                            suu = 2;
                            skT.text = @"
<Size=50>水の誓い</Size>

敵に1000のダメージ";
                            skillhan(suu);
                            break;
                        case "par3":
                            suu = 3;
                            skT.text = @"
<Size=50>光の誓い</Size>

敵に1000のダメージ";
                            skillhan(suu);
                            break;
                        case "par4":
                            suu = 4;
                            skT.text = @"
<Size=50>闇の誓い</Size>

敵に1000のダメージ";
                            skillhan(suu);
                            break;
                        case "par5":
                            skT.text = @"
<Size=50>炎の誓い</Size>

敵に1000のダメージ";
                            suu = 5;
                            skillhan(suu);
                            break;
                        default:
                            //cansw = false;
                            break;

                    }
                 
                }
            }

        }

    }
    void skillhan(int j)
    {
        if (nowskill[j] >= skilltarn[j])
        {
            aud.SEo3();
            cansw = true;
            id.botanstate = 2;
        }
    }
    //はい
   public void hai()
    {
        id.botanstate = 0;
        aud.SEo3();
        if (nowskill[suu] >= skilltarn[suu])
        {
            cansw = false;
            //スキルを発動
            Debug.Log("スキル" + suu);
            nowskill[suu] = 0;
        }
    }
    //いいえ
    public void iie()
    {
        id.botanstate = 0;
        aud.SEo3();
        cansw = false;
    }
}
