using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleoudes : MonoBehaviour {
    //自動的に消える
    private float nowTime;
    private float endTime;
	// Use this for initialization
	void Start () {
        nowTime = 0;
        endTime = 1;
	}
	
	// Update is called once per frame
	void Update () {
        nowTime += Time.deltaTime;
        if (nowTime > endTime)
        {
            Destroy(gameObject);
        }
	}
}
