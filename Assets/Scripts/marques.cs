using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class marques : MonoBehaviour {
    public float temp=0;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        temp += Time.deltaTime;
        if(temp>=2.5f)
        {
            SceneManager.LoadScene("Iniciar");
        }
	}
}
