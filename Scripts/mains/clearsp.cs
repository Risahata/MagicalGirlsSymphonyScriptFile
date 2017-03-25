using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearsp : MonoBehaviour {
    //クリアー関係
    public GameObject clearC;
    [SerializeField]
    GlobalSp Gl;

    [SerializeField]
    audioSys aud;

    //一度だけ通る
    private bool itido;
	// Use this for initialization
	void Start () {
        clearC.SetActive(false);
        itido = false;
	}
	
	// Update is called once per frame
	void Update () {
        switch (Gl.tarn)
        {
            //クリア
            case 3:
                if (itido == false)
                {
                    //クリア時の音声
                    aud.SEo7();
                    itido = true;
                }

                clearC.SetActive(true);
                break;
        }


	}
    //クリアキャンバスのOKボタン
    public void okb()
    {
        //音
        aud.SEo3();
        switch (menuG.stagen)
        {
            case 0:

                menuG.clcon[0] = true;
                break;
        }
        oversp.fdsw = true;
        ScnGlob.tomain = true;
    }
}
