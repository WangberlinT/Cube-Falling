using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float rotSpeed = 15.0f;      //旋转速度
    public float moveSpeed = 10.0f;     //移动速度
    //-------Gravity and Jump---------
    public float jumpSpeed = 5.0f;      //跳跃初速度
    public float gravity = -9.8f;       //重力系数
    public float terminalVelocity = -20.0f;//最大下落系数
    public float minFall = -1.5f;       //最小下落系数
    public float ground_offset = 0f;    //地面偏移
    //------Private-------------------
    private CharacterController character;
    private float verticalSpeed;
    private ControllerColliderHit contact;//Controller 碰撞检测
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
        //如果是下落状态（速度向下），就检测character向下的射线检测
        if(verticalSpeed < 0 && Physics.Raycast(transform.position,Vector3.down, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            //由于character是胶囊型，所以check是人物和地面碰撞检测距离（应该是一个接近0的很小的值）
            float check = (character.height + character.radius) / 2f + ground_offset;
            distance = hit.distance;
            hitGround = hit.distance <= check;//如果距离小于check，则检测为碰撞
        }
        //碰撞发生则表示在地面上
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
        else//如果在空中
        {
            Debug.Log("Not Grounded");
            verticalSpeed += gravity * Time.deltaTime;
            if(verticalSpeed < terminalVelocity)
            {
                verticalSpeed = terminalVelocity;
            }
            //这个也是检测着陆，看起来是斜坡的运算？但是应该不会运行到
            //实测注释掉也没问题
/*            if(character.isGrounded)//不知道为什么不起作用
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
*/
        }
        movement.y = verticalSpeed;
        return movement;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contact = hit;//简单的绑定作用
    }
}
