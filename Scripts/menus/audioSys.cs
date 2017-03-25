using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioSys : MonoBehaviour {
    //音関係
    public GameObject[] SEo;
    private AudioSource[] Auo;
    //攻撃魔法系統
    public GameObject[] SEk;
    private AudioSource[] auok;
    //敵のボイス
    public GameObject[] tekiv;
    private AudioSource[] tekio;

    public AudioClip[] mode1;
    public AudioClip[] mode2;
	// Use this for initialization
	void Start () {
        Auo = new AudioSource[SEo.Length];

        for (int i = 0; i < SEo.Length; i++)
        {
            Auo[i] = SEo[i].GetComponent<AudioSource>();
        }
        if (SEk != null)
        {
            auok = new AudioSource[SEk.Length];
            for (int i = 0; i < SEk.Length; i++)
            {
                auok[i] = SEk[i].GetComponent<AudioSource>();
            }
        }
        if (tekiv != null)
        {
            tekio = new AudioSource[tekiv.Length];
            for(int i = 0; i < tekiv.Length; i++)
            {
                tekio[i] = tekiv[i].GetComponent<AudioSource>();
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //SE音１
    public void SEo0()
    {
        if (SEo[0] != null)
        {
            Auo[0].PlayOneShot(Auo[0].clip);
        }
    }
    //SE音２
    public void SEo1()
    {
        if (SEo[1] != null)
        {
            Auo[1].PlayOneShot(Auo[1].clip);
        }
    }
    //SE音３
    public void SEo2()
    {
        if (SEo[2] != null)
        {
            Auo[2].PlayOneShot(Auo[2].clip);
        }
    }
    //SE音4
    public void SEo3()
    {
        if (SEo[3] != null)
        {
            Auo[3].PlayOneShot(Auo[3].clip);
        }
    }
    //SE音5
    public void SEo4()
    {
        if (SEo[4] != null)
        {
            Auo[4].PlayOneShot(Auo[4].clip);
        }
    }
    //SE音5
    public void SEo5()
    {
        if (SEo[5] != null)
        {
            Auo[5].PlayOneShot(Auo[5].clip);
        }
    }
    //味方の攻撃音声
    public void SEo6()
    {
        if (SEo[6] != null)
        {
            Auo[6].PlayOneShot(Auo[6].clip);
        }
    }
    //クリア時の音声
    public void SEo7()
    {
        if (SEo[7] != null)
        {
            Auo[7].PlayOneShot(Auo[7].clip);
        }
    }
    //プレイヤーのダメージ音
    public void SEo8()
    {
        if (SEo[8] != null)
        {
            Auo[8].PlayOneShot(Auo[8].clip);
        }
    }
    //回復
    public void SEo9()
    {
        if (SEo[9] != null)
        {
            Auo[9].PlayOneShot(Auo[9].clip);
        }
    }
    public void SEkou(int suu)
    {
        if (SEk[suu] != null)
        {
            auok[suu].PlayOneShot(auok[suu].clip);
        }
    }
    //敵の声
    public void tekiO(int v,int suu)
    {
        //suuは０か１

        if (tekiv[v] != null)
        {
            //オーディオクリップのセット
            switch (suu)
            {
                case 0:
                    tekio[v].clip = mode1[v];
                    break;
                case 1:
                    tekio[v].clip = mode2[v];
                    break;
           }

            tekio[v].PlayOneShot(tekio[v].clip);
        }
    }

}
