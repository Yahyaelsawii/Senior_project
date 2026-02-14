using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimatorToggle : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //ToggleDoorOpen();
        
    }

   public void ToggleDoorOpen()
    {
        anim.SetBool("DoorOpen",true);
        Invoke("ToggleDoorclose", 5f);
    }

     public void ToggleDoorclose()
    {
        anim.SetBool("DoorOpen",false);
    }
}
