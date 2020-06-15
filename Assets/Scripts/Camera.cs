using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public Vector3 Delay;
    private Vector3 veloc;
    public Transform Player;

    public Vector3 MinPos;
    public Vector3 MaxPos;

    public bool bounds;
	
	// Update is called once per frame
	void Update () {

        float PosX = Mathf.SmoothDamp(transform.position.x, Player.position.x, ref veloc.x, Delay.x);
        float PosY = Mathf.SmoothDamp(transform.position.y, Player.position.y, ref veloc.y, Delay.y);

        transform.position = new Vector3(PosX, PosY, transform.position.z);


        if (bounds)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, MinPos.x, MaxPos.x),
                Mathf.Clamp(transform.position.y, MinPos.y, MaxPos.y),
                Mathf.Clamp(transform.position.z, MinPos.z, MaxPos.z));
        }
    }
}
