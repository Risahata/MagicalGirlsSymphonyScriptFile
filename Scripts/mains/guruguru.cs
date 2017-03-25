using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guruguru : MonoBehaviour {
    //メイン画面の魔法陣ぐるぐる
    public GameObject mahou;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (mahou != null)
        {
            mahou.transform.Rotate(0, 0, 1);
        }
	}
}
