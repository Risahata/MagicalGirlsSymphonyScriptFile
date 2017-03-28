using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mahoujinguru : MonoBehaviour {
    //魔法陣ぐるぐる
    public GameObject[] jin = new GameObject[3];	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < jin.Length; i++)
        {
            switch (i)
            {
                case 0:
                case 2:
                    jin[i].transform.Rotate(0, 0, 1);
                    break;
                case 1:
                    jin[i].transform.Rotate(0, 0, -1);
                    break;
            }
  
        }
	}
}
