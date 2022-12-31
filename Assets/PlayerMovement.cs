using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D Rigidbody;
    [SerializeField]
    private float Velocidad;
    private float moveinput;

    [Header("Ground Check")]
    private bool IsGrounded;
    public Transform Feet;
    public LayerMask WhatIsGround;
    public float Groundcheck;

    [Header("Jump")]
    public float Jumpheight;
    private float Jumptimecounter;
    public float Jumptime;
    private bool IsJumping;
    private int Extrajumps;
    public int Extrajumpsvalue;
    public Transform point;
    public float CheckRadius;
    public LayerMask GroundLayer;

    [Header("Dash")]
    public bool Candash = true;
    public float Dashspeed;
    public float Dashtime;
    public float Dashjump;
    public float Dashcooldown;


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {



        moveinput = Input.GetAxisRaw("Horizontal");
        Rigidbody.velocity = new Vector2(moveinput * Velocidad, Rigidbody.velocity.y);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            DashSkill();
        }
        if(Candash == false)
        {
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 0);
        }

        IsGrounded = Physics2D.OverlapCircle(Feet.position, Groundcheck, WhatIsGround);
        if(IsGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            IsJumping = true;
            Jumptimecounter = Jumptime;
            Rigidbody.velocity = Vector2.up * Jumpheight;
        }
        if(Input.GetKey(KeyCode.Space) && IsJumping == true)
        {
            if(Jumptimecounter > 0)
            {
                Rigidbody.velocity = Vector2.up * Jumpheight;
                Jumptimecounter -= Time.deltaTime;
            }
            else
            {
                IsJumping = false;
            }
            if(Input.GetKeyUp(KeyCode.Space))
            {
                IsJumping = false;
            }
        }

        void DashSkill()
        {
            if (Candash)
            {
                StartCoroutine(Dash());
            }
        }

        IEnumerator Dash()
        {
            Candash = false;
            Velocidad = Dashspeed;
            Jumpheight = Dashjump;
            yield return new WaitForSeconds(Dashtime);
            Velocidad = 11f;
            Jumpheight = 20f;
            yield return new WaitForSeconds(Dashcooldown);
            Candash = true;

        }
    }

}
