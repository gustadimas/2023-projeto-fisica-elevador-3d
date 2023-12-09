using UnityEngine;
using TMPro;

public class Elevador : MonoBehaviour
{
    [SerializeField] float alturaMax;
    [SerializeField] float aceleracao;
    [SerializeField] GameObject player;
    [SerializeField] TextMeshPro txtVisor;

    bool subindo = false;
    Rigidbody rb;

    float v0 = 0f;
    float t = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (subindo) 
            Subir();

        if (transform.position.y > alturaMax)
            Parar();

        txtVisor.text = Mathf.Round(transform.position.y).ToString();
    }

    public void SubidaIniciar()
    {
        rb.isKinematic = false;
        subindo = true;
    }

    void Subir()
    {
        ///Equação utilizada: s = v0.t + 1/2.a.t²
        float s = v0 * t + 0.5f * aceleracao * t * t;

        ///Testes de aceleracao:
        //rb.AddForce(aceleracao * Time.deltaTime * Vector3.up, ForceMode.Acceleration); //lentao
        rb.AddForce(aceleracao * Vector3.up, ForceMode.Acceleration); //mediano
        //rb.AddForce(Vector3.up * s, ForceMode.Acceleration); //virado na zorra
        //transform.position = new Vector3(transform.position.x, s, transform.position.z); //nao funfa

        v0 += aceleracao * t;
        t += Time.deltaTime;

        // Debug.LogError(rb.velocity.y); //teste
    }

    void Parar()
    {
        //float resistenciaInercia = 0.2f; 
        //rb.velocity *= 1 - resistenciaInercia * Time.deltaTime;

        player.GetComponent<Jogador>().RagdollAtivar();

        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        transform.position = new Vector3(transform.position.x, alturaMax, transform.position.z);
        subindo = false;
    }
}