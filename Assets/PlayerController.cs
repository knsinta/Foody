using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D cewe;
    public Collider2D coll; 
    public Animator anim;
    public LayerMask ground;
    public float jumpForce = 10f;
    public enum Status {diam, berlari, melompat, jatuh}
    Status status = Status.diam;
    // Start is called before the first frame update
    void Start()
    {
        cewe = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            cewe.velocity = new Vector2(-3, cewe.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            // anim.SetBool("Berlari", true);
        }

        else if(Input.GetKey(KeyCode.D))
        {
            cewe.velocity = new Vector2(3, cewe.velocity.y);
            transform.localScale = new Vector2(1, 1);
            // anim.SetBool("Berlari", true);
        }

        else
        {
            // anim.SetBool("Berlari", false);
        }

        if(Input.GetKeyDown(KeyCode.Space) && coll.IsTouchingLayers(ground))
        {
            // cewe.velocity = new Vector2 (cewe.velocity.x,10f);
            cewe.velocity = Vector2.up * jumpForce;
            status = Status.melompat;
        }

        KecepatanStatus();
        anim.SetInteger("status", (int)status);
    }

    private void KecepatanStatus()
    {
        if(status == Status.melompat)
        {
            if(cewe.velocity.y < .1f)
            {
                status = Status.jatuh;
            }
            // else if(Input.GetKeyDown(KeyCode.Space) && coll.IsTouchingLayers(ground))
            // {
            //     status = Status.jatuh;
            // }
        }

        else if(status == Status.jatuh)
        {
            if(coll.IsTouchingLayers(ground))
            {
                status = Status.diam;
            }
        }
        
        else if(Mathf.Abs(cewe.velocity.x) > 2f)
        {
            status = Status.berlari; 
        }

        else
        {
             status = Status.diam;
        }
    }
}
