using UnityEngine;

public class Jogador : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Transform t_cameraController;
    [SerializeField] Vector2 cameraRange;
    [SerializeField] float cameraSensibilidade;
    [SerializeField] Transform t_move;

    [SerializeField] Collider[] col;
    [SerializeField] Rigidbody[] rb_rag;

    Rigidbody rb;
    Animator animator;

    bool ragdoll = false;

    private void Awake()
    {
        ColisoresAtivacaoSet(0);
        RigidbodyAtivacaoSet(0);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        t_cameraController.transform.eulerAngles = Vector3.zero;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        t_cameraController.position = transform.position;
        //t_cameraController.position = transform.GetChild(0).position;

        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        t_cameraController.eulerAngles += new Vector3(-mouseY * cameraSensibilidade * Time.deltaTime, mouseX * cameraSensibilidade * Time.deltaTime, 0); //camera

        float x = t_cameraController.eulerAngles.x;

        if (t_cameraController.eulerAngles.x > 300) 
            x = t_cameraController.eulerAngles.x - 360;

        x = Mathf.Clamp(x, cameraRange.x, cameraRange.y);

        t_cameraController.eulerAngles = new Vector3(x, t_cameraController.eulerAngles.y, t_cameraController.eulerAngles.z);

        if (ragdoll) 
            return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        t_move.eulerAngles = new Vector3(0, t_cameraController.eulerAngles.y, 0);

        rb.velocity = h * moveSpeed * t_move.right + v * moveSpeed * t_move.forward + Vector3.up * rb.velocity.y;

        animator.SetBool("_andando", h != 0 || v != 0);

        if (animator.GetBool("_andando"))
            transform.LookAt(h * moveSpeed * t_move.right + v * moveSpeed * t_move.forward + transform.position);
    }

    public void RagdollAtivar()
    {
        ragdoll = true;

        rb.isKinematic = true;
        GetComponent<CapsuleCollider>().enabled = false;
        animator.enabled = false;

        ColisoresAtivacaoSet(1);
        RigidbodyAtivacaoSet(1);
    }

    void ColisoresAtivacaoSet(int valor)
    {
        foreach (Collider col in col) 
            col.isTrigger = valor == 0;
    }

    void RigidbodyAtivacaoSet(int valor)
    {
        foreach (Rigidbody rb_rag in rb_rag) 
            rb_rag.isKinematic = valor == 0;
    }
}