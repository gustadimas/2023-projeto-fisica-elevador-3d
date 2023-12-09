using UnityEngine;

public enum Door1State { Door1, Door1Closed }
public enum Door2State { Door2, Door2Closed }

public class Door : MonoBehaviour
{
    [SerializeField] Animator anim_door1;
    [SerializeField] Animator anim_door2;

    Door1State door1State;
    Door2State door2State;

    bool entrou = false;

    private void Update()
    {
        UpdateAnimations();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            entrou = true;
            anim_door1.SetTrigger("default");
            anim_door2.SetTrigger("default");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            entrou = false;
        }
    }

    void UpdateAnimations()
    {
        if (entrou)
        {
            door1State = Door1State.Door1;
            door2State = Door2State.Door2;
        }
        else
        {
            door1State = Door1State.Door1Closed;
            door2State = Door2State.Door2Closed;
        }

        anim_door1.SetInteger("state", (int)door1State);
        anim_door2.SetInteger("state", (int)door2State);
    }
}
