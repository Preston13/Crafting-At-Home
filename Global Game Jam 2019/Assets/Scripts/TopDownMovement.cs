using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed = 3f;

    private Rigidbody2D rigid;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rigid = player.GetComponent<Rigidbody2D>();
        anim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        player.transform.position += movement * moveSpeed * Time.deltaTime;

        if(Input.GetAxis("Horizontal") > 0)
        {
            anim.speed = 1;
            anim.Play("Walk_1");
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            anim.speed = 1;
            anim.Play("Walk_2");
        }
        else if(Input.GetAxis("Vertical") > 0)
        {
            anim.speed = 1;
            anim.Play("Walk_3");
        }
        else if(Input.GetAxis("Vertical") < 0)
        {
            anim.speed = 1;
            anim.Play("Walk_0");
        }
        else
        {
            anim.speed = 0;
        }
    }
}
