using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anima;

    public float Veloc;
    private float horizontal;
    public float ForcaP = 690;

    public bool EstaVivo;
    private bool EstaAndando;
    private bool LadoDireito;
    public bool EstanoChao;
    public bool EstanaPared;
    public bool EstanaPlat;
    public bool VerifPlat;

    public Transform PontoColisaoChao;
    public Transform PontoColisaoPared;
    public Transform PontoColisaoPlatUni;
    public Transform PontoVerifPlat;

    public LayerMask Plataforma;
    public LayerMask PlataformaUni;


    public float raio1;
    public float raio2;
    public float raio3;
    public float raio4;

    public AudioSource Audio;
    public AudioClip jumpaudio;
    public AudioClip morrendo;


    // Use this for initialization
    void Start()
    {

        rig = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        LadoDireito = transform.localScale.x > 0f;

        EstaVivo = true;
    }

    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
        EstaAndando = Mathf.Abs(horizontal) > 0f;

        EstanoChao = Physics2D.OverlapCircle(PontoColisaoChao.position, raio1, Plataforma);
        EstanaPared = Physics2D.OverlapCircle(PontoColisaoPared.position, raio2, Plataforma);

        EstanaPlat = Physics2D.OverlapCircle(PontoColisaoPlatUni.position, raio3, PlataformaUni);
        VerifPlat = Physics2D.OverlapCircle(PontoVerifPlat.position, raio4, PlataformaUni);

        if (EstaVivo)
        {
            Movimento();
            MudarDirecao();
            Jump();
            animations();

            if (transform.position.y < (-10.96f))
            {
                SceneManager.LoadScene("Morreu");
            }
        }
    }

    private void Movimento()
    {
        if (EstaAndando && !EstanaPared)
        {
            rig.velocity = new Vector2(horizontal * Veloc, rig.velocity.y);
        }

    }

    private void MudarDirecao()
    {
        if (horizontal > 0 && !LadoDireito || horizontal < 0 && LadoDireito)
        {
            LadoDireito = !LadoDireito;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && (EstanoChao || EstanaPlat) && !VerifPlat)
        {
            Audio.volume = 1;
            Audio.PlayOneShot(jumpaudio);
            rig.AddForce(transform.up * ForcaP);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PontoColisaoChao.position, raio1);
        Gizmos.DrawWireSphere(PontoColisaoPared.position, raio2);
        Gizmos.DrawWireSphere(PontoColisaoPlatUni.position, raio3);
        Gizmos.DrawWireSphere(PontoVerifPlat.position, raio4);
    }

    private void animations()
    {
        if (VerifPlat)
        {
            EstanaPlat = false;
        }
        anima.SetBool("Pulando", !EstanoChao && !EstanaPlat);
        anima.SetFloat("VelVerti", rig.velocity.y);
        anima.SetBool("Andando", (EstaAndando && (EstanaPlat || EstanoChao)));
    }

    private void OnTriggerEnter2D(Collider2D outro)
    {

        if (outro.tag == "tesouro")
        {
            SceneManager.LoadScene("Parabens");
        }
    }

    private void DesativarPla()
    {
        gameObject.SetActive(false);
    }

    private void Morte()
    {
        SceneManager.LoadScene("Morreu");
    }

    public Vector2 PegarDirecaoPlay()
    {
        return Vector2.left;
    }

    public Vector2 PegarDirecaoPlay2()
    {
        return  Vector2.right;
    }

}