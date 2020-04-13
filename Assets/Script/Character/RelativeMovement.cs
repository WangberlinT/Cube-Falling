using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float rotSpeed = 15.0f;
    public float moveSpeed = 10.0f;
    //-------Gravity and Jump---------
    public float jumpSpeed = 5.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -20.0f;
    public float minFall = -1.5f;
    public float ground_offset = 0f;
    //------Private-------------------
    private CharacterController character;
    private float verticalSpeed;
    private ControllerColliderHit contact;//不明白这是啥
    private void Start()
    {
        verticalSpeed = minFall;
        character = GetComponent<CharacterController>();
    }
    void Update()
    {
        Vector3 movement = Vector3.zero;//set target movement to 0
        // get input
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        if (horInput != 0 || vertInput != 0)
        {
            // get vector direction(absolute)
            movement.x = horInput;
            movement.z = vertInput;
            movement = Vector3.ClampMagnitude(movement * moveSpeed, moveSpeed);
            

            //Debug.Log(string.Format("x,z position = ({0},{1})", movement.x, movement.z));
            Quaternion tmp = target.rotation;//save camera init rotate
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);//把相对摄像头朝向的movement向量变换为绝对向量
            Debug.Log(string.Format("x,z position(abs) = ({0},{1})", movement.x, movement.z));
            target.rotation = tmp;
            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }
        movement = Jump(movement);//顺序不能改
        character.Move(movement * Time.deltaTime);//巧妙
    }

    private Vector3 Jump(Vector3 movement)
    {
        bool hitGround = false;
        float distance = 0;
        RaycastHit hit;//RaycastHit 结构体 用于存储射线碰撞信息
        if(verticalSpeed < 0 && Physics.Raycast(transform.position,Vector3.down, out hit))//不明白
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            float check = (character.height + character.radius) / 2f + ground_offset;//不明白，怎么判断自己是否被射中的
            distance = hit.distance;
            hitGround = hit.distance <= check;
        }
        if(hitGround)
        {
            Debug.Log(string.Format("Grounded,distance = {0}",distance));
            if(Input.GetButtonDown("Jump"))
            {
                verticalSpeed = jumpSpeed;
            }
            else
            {
                verticalSpeed = minFall;
            }
        }
        else
        {
            Debug.Log("Not Grounded");
            verticalSpeed += gravity * Time.deltaTime;
            if(verticalSpeed < terminalVelocity)
            {
                verticalSpeed = terminalVelocity;
            }

            if(character.isGrounded)//不知道为什么不起作用
            {
                if (Vector3.Dot(movement, contact.normal) < 0)//计算点积
                {
                    movement = contact.normal * moveSpeed;//不明白
                }
                else
                {
                    movement += contact.normal * moveSpeed;//不明白
                }
            }
        }
        movement.y = verticalSpeed;
        return movement;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contact = hit;//不明白
    }
}
