using UnityEngine;

public class Controlador : MonoBehaviour
{
    [SerializeField] GameObject elevador;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
            elevador.GetComponent<Elevador>().SubidaIniciar();
    }
}