using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 8f;
    private Vector2 clickTarget;
    private Vector2 relativePos;
    private Vector2 movement;
    private float horizontal;
    private float vertical;
    bool moving;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        clickTarget = transform.position;
    }
    void Update()
    {
        //Ű input �ڵ�

        movement.x = Input.GetAxisRaw("Horizontal"); //Ű �Է�
        movement.y = Input.GetAxisRaw("Vertical"); 
        //animator.SetFloat("Horizontal", movement.x);
        //animator.SetFloat("Vertical", movement.y);
        //animator.SetFloat("Speed", movement.sqrMagnitude); //���� üũ��
        if (horizontal != 0||vertical !=0) animator.SetBool("Moving", true);
        else animator.SetBool("Moving", false);
        // ���콺 click �ڵ�
        if (Input.GetMouseButtonDown(0))
        {
            clickTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            animator.SetBool("Moving", true);
            moving = true;
        }
        
        relativePos = new Vector2(
             clickTarget.x - rb.position.x,
             clickTarget.y - rb.position.y);
        RotateAnimation();
    }
    private void FixedUpdate()//���� ��� �� �� ���
    {
        //movement �ڵ�
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);//���� �� ������ ���� �ð�

        //click �� movement �ڵ�
        if (moving && (Vector2)rb.position != clickTarget)
        {
            float step = moveSpeed * Time.fixedDeltaTime;
            rb.position = Vector2.MoveTowards(rb.position, clickTarget, step);
        }
        else
        {
            animator.SetBool("Moving", false);
            moving = false;
        }
        
       
    }
    private void RotateAnimation()
    {
        if (horizontal > 0.01f||relativePos.x>0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        else if (horizontal < -0.01f||relativePos.x<0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

    }
}

