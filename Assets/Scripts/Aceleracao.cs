using UnityEngine;

public class Aceleracao : MonoBehaviour
{
    /// <summary>
    /// Código Antigo
    /// </summary>

    // Equação utilizada: s = v0.t + 1/2.a.t²

    public float a = 50f;
    private float v0 = 0f;
    private float t = 0f;

    private bool espacoPressionado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            espacoPressionado = true;

        if (espacoPressionado)
        {
            float s = v0 * t + 0.5f * a * t * t;

            transform.position = new Vector3(transform.position.x, s, transform.position.z);
            v0 += a * t;
            t += Time.deltaTime;
        }
    }
}
