using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour {
    private Rigidbody2D rig;
    private Animator anima;

    private bool LadoDireito;

    public float veloc;

    public Player scriptp;

    public bool Vivo;

    public AudioSource Audio;

    public AudioClip colisao;

    // Use this for initialization
    void Awake () {

        rig = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        Vivo = true;

    }
	
	// Update is called once per frame
	void Update () {

        

        if (Vivo)
        {
            Movimento();
        }

       
    }
  
    private void OnTriggerEnter2D(Collider2D outro)
    {
        if(outro.tag=="Limite")
        {
            MudarDirecao();
        }
        
    }

     void OnCollisionEnter2D(Collision2D colli)
    {
       if( colli.gameObject.name=="Player")
        {
          if(colli.contacts[0].point.y > (transform.position.y + 0.5f))
            {
                if (scriptp.EstaVivo)
                {
                    anima.SetBool("Morreu", true);
                    Audio.volume = 1;
                    Audio.PlayOneShot(colisao);
                    colli.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * scriptp.ForcaP);
                    colli.gameObject.GetComponent<Player>().Audio.volume = 1;
                    colli.gameObject.GetComponent<Player>().Audio.PlayOneShot(scriptp.jumpaudio);
                }
            }
          else
            {          
                colli.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * scriptp.ForcaP);
                colli.gameObject.GetComponent<Player>().Audio.volume = 1;
                colli.gameObject.GetComponent<Player>().Audio.PlayOneShot(scriptp.morrendo);
                colli.gameObject.GetComponent<Animator>().SetBool("Morrer", true);
               
                scriptp.EstaVivo = false;   
                
                if(scriptp.transform.position.x < transform.position.x)
                colli.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * 3;
                else if(scriptp.transform.position.x > transform.position.x)
                colli.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * 3;
            }

        }

        if (colli.gameObject.tag == "Inimigo")
        {
            MudarDirecao();
        }
    }

    private Vector2 PegarDirecao()
    {
        return LadoDireito ? Vector2.left : Vector2.right;
    }

    private void MudarDirecao()
    {
        LadoDireito=!LadoDireito;
        this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
     
    private void Movimento()
    {
        transform.Translate((PegarDirecao()) *( veloc * Time.deltaTime));
    }

    private void Destruir()
    {
        Destroy(gameObject);
    }

   

}
