using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo_dois : MonoBehaviour {
    private Rigidbody2D rig;
    private Animator anima;

    private bool ColidindoChao;
    public Transform PontoColisaoChao;
    public LayerMask plat;

    public float tempo;
    public float tempolimite;

    public Player scriptp;

    public float forcap;

	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        ColidindoChao = Physics2D.OverlapCircle(PontoColisaoChao.position, 0.02f, plat);
        ACAO();

	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PontoColisaoChao.position, 0.02f);
    }

    private void ACAO()
    {
        tempo += Time.deltaTime;
        if(tempo<=tempolimite)
        {
            anima.SetBool("Idle",ColidindoChao);
        }

        if ( tempo>tempolimite)
        {
            rig.AddForce(transform.up * forcap);
            tempo = 0;
        }
        anima.SetBool("Jump", !ColidindoChao);
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name=="Player")
        {
            if (scriptp.EstaVivo)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * scriptp.ForcaP);
                collision.gameObject.GetComponent<Animator>().SetBool("Morrer", true);
                collision.gameObject.GetComponent<Player>().Audio.volume = 1;
                collision.gameObject.GetComponent<Player>().Audio.PlayOneShot(scriptp.morrendo);

                scriptp.EstaVivo = false;

                if (scriptp.transform.position.x < transform.position.x)
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * 3;
                else if (scriptp.transform.position.x > transform.position.x)
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * 3;
            }
        }
    }


}
