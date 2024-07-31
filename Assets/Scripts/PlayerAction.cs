using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float speed;
    public GameManager manager; 
    float h;
    float v;
    bool isHorizonMove;
    Vector3 dirVec;

    Rigidbody2D rigid;
    Animator anim;
    GameObject scanObject;
     void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical"); 
        bool hUP = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");

        if(hDown)
        {
            isHorizonMove = true;
        }
        else if(vDown)
        {
            isHorizonMove = false;
        }
        else if (hUP || vUp)
        {
            isHorizonMove = h != 0;
        }

        if (anim.GetInteger("hAxisRaw") !=h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            anim.SetBool("isChange", false);
        }

        if(vDown && v == 1)
        {
            dirVec = Vector3.up;
        }
        else if (vDown && v == -1)
        {
            dirVec = Vector3.down;
        }
        else if(hDown && h == 1)
        {
            dirVec = Vector3.right;
        }
        else if (hDown && h == -1)
        {
            dirVec = Vector3.left;
        }

        //Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            manager.Action(scanObject);

        }

    }

     void FixedUpdate()
    {
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * speed;

        Debug.DrawRay(rigid.position, dirVec*0.7f, new Color(0,1,0));
        RaycastHit2D rayHIt = Physics2D.Raycast(rigid.position, dirVec , 0.7f, LayerMask.GetMask("Object"));

        if (rayHIt.collider != null) {
            scanObject = rayHIt.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }
}
