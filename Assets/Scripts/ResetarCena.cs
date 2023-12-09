using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetarCena : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
