using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetButton("Run")) {
            if(Input.GetButton("Forward")) {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("walk_forward") &&
                    !anim.GetCurrentAnimatorStateInfo(0).IsName("jump")) {
                    anim.Play("walk_forward");      
                }
            }
            if(Input.GetButton("Left")) {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("walk_left") &&
                    !anim.GetCurrentAnimatorStateInfo(0).IsName("jump")) {
                    anim.Play("walk_left");      
                }
            }
            if(Input.GetButton("Right")) {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("walk_right") &&
                    !anim.GetCurrentAnimatorStateInfo(0).IsName("jump")) {
                    anim.Play("walk_left");      
                }
            }
            if(Input.GetButton("Back")) {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("walk_back") &&
                    !anim.GetCurrentAnimatorStateInfo(0).IsName("jump")) {
                    anim.Play("walk_back");      
                }
            }
        }
        if(Input.GetButton("Run")) {
            if(Input.GetButton("Forward") &&
                !anim.GetCurrentAnimatorStateInfo(0).IsName("jump")) {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("run")) {
                    anim.Play("run");
            }
        }

        }
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            anim.Play("shoot");
        }
        if(Input.GetButtonDown("Jump")) {
            anim.Play("jump");
        }
        if(Input.GetButtonUp("Forward")) {
            anim.Play("idle");
        }
    }
}
