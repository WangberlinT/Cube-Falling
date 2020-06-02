﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float rotSpeed = 15.0f;      //旋转速度
    public float moveSpeed = 3.0f;     //移动速度
    //-------Gravity and Jump---------
    public float jumpSpeed = 5.0f;      //跳跃初速度
    public float gravity = -9.8f;       //重力系数
    public float terminalVelocity = -20.0f;//最大下落系数
    public float minFall = -1.5f;       //最小下落系数
    public float ground_offset = 0.0f;    //地面偏移
    //------Private-------------------
    private CharacterController character;
    private float verticalSpeed;
    private float initMoveSpeed;
    private int isSpeedUp;
    private ControllerColliderHit contact;//Controller 碰撞检测
    private const string TAG = "[PlayerControl]";
    private Vector3 movement = Vector3.zero;
    //Debug
    private DebugScreen screen;

    //------UserInput---------
    private float horInput = 0;
    private float vertInput = 0;
    private float upSpeed = 0;//只有DebugMode才起作用
    private bool isJump = false;
    private bool isRunning = false;
    private PlayerAnimator animatorControl;
    //------组件---------
    public AudioSource audioSource;
    public GameObject world;
    private void Start()
    {
        animatorControl = GetComponent<PlayerAnimator>();
        verticalSpeed = minFall;
        character = GetComponent<CharacterController>();
        screen = DebugScreen.GetInstance();
        initMoveSpeed = moveSpeed;
    }
    void Update()
    {
        GetUserInput();
        Move();
    }

    private void Move()
    {
        movement = Vector3.zero;
        // Debug.Log(moveSpeed);
        FallDown();

        if (horInput != 0 || vertInput != 0)
        {
            // get vector direction(absolute)
            movement.x = horInput;
            movement.z = vertInput;
            movement = Vector3.ClampMagnitude(movement * (moveSpeed+(0.5f*initMoveSpeed) * isSpeedUp), (moveSpeed + (0.5f * initMoveSpeed) * isSpeedUp));


            //Debug.Log(string.Format("x,z position = ({0},{1})", movement.x, movement.z));
            Quaternion tmp = target.rotation;//save camera init rotate
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);//把相对摄像头朝向的movement向量变换为绝对向量
            target.rotation = tmp;
            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }
        
        if(IsGrounded())
        {
            //判断是否要播放脚步声
            if (!audioSource.isPlaying && (horInput != 0 || vertInput != 0))
                audioSource.Play();
            world.GetComponent<World>().GetCubeManager().TreadCube(transform.position);
        }
            
        else
            audioSource.Pause();
        VerticalMovement();

        movement.y = verticalSpeed;
        character.Move(movement * Time.deltaTime);
    }

    private void VerticalMovement()
    {
        if(GameManager.DebugMode)
        {
            verticalSpeed = upSpeed;
        }
        else
        {
            if (isJump)
                Jump();
            RunAnimation();
        }
    }

    private void RunAnimation()
    {
        if (horInput != 0 || vertInput != 0)
            animatorControl.Run();
        else if (!isJump)
            animatorControl.Idle();
    }

    private void GetUserInput()
    {
        
        horInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        isJump = Input.GetButtonDown("Jump");
        //当按下左shift时加速
        if (Input.GetKeyDown(KeyCode.LeftShift))
            isSpeedUp = 1;
        //当放开左shift时恢复原速
        if (Input.GetKeyUp(KeyCode.LeftShift))
            isSpeedUp = 0;

        if (Input.GetKey(KeyCode.Space))
            upSpeed = moveSpeed;
        else if (Input.GetKey(KeyCode.BackQuote))
            upSpeed = -moveSpeed;
        else
            upSpeed = 0;
        
    }

    private bool IsGrounded()
    {
        bool hitGround = false;
        float distance = 0;
        RaycastHit hit;//RaycastHit 结构体 用于存储射线碰撞信息
        //如果是下落状态（速度向下），就检测character向下的射线检测
        if (verticalSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            //Debug.DrawLine(transform.position, hit.point, Color.red);
            //由于character是胶囊型，所以check是人物和地面碰撞检测距离（应该是一个接近0的很小的值）
            float check = (character.height + character.radius) / 2f + ground_offset - 0.8f;
            distance = hit.distance;
            hitGround = distance <= check;//如果距离小于check，则检测为碰撞
        }
        if(hitGround)
            screen.Log(TAG, string.Format("Grounded,distance = {0}", distance));
        else
            screen.Log(TAG, string.Format("Not Grounded,distance = {0}", distance));
        //悬空状态
        if(!hitGround&&distance==0&&verticalSpeed<-18.0f)
        {
            animatorControl.Die();
            GameManager.GetInstance().Fail();
            verticalSpeed = 0f;
        }
        
        return hitGround;
    }

    // 更新垂直速度
    private void Jump()
    {
        //碰撞发生则表示在地面上
        if(IsGrounded())
        {
            animatorControl.Jump();
            verticalSpeed = jumpSpeed;
            // moveSpeed = 3.0f;//重置水平速度
        }
    }
    //更新下落垂直速度
    private void FallDown()
    {
        if (!IsGrounded())
        {
            verticalSpeed += gravity * Time.deltaTime;
            if (verticalSpeed < terminalVelocity)
            {
                verticalSpeed = terminalVelocity;
            }
        }
        else
            verticalSpeed = minFall;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contact = hit;//简单的绑定作用
        GameObject hitObject = hit.gameObject;
        //Debug.Log(hitObject.name);
        if(hitObject.name.Equals("IceBody"))//如果在冰面上
            moveSpeed = 6.0f;
        else if(hitObject.name.Equals("MudBody"))//如果在泥土上
            moveSpeed = 1.5f;
        else
            moveSpeed = 3.0f;//重置水平速度
        if(hitObject.name.Equals("Breaker(Clone)"))//如果碰撞到的物体为monster
        {
            Vector3 player_postion = transform.position;
            Vector3 monster_position = hitObject.transform.position;
            if(player_postion.y-monster_position.y>0.5f){
                animatorControl.Jump();
                verticalSpeed = jumpSpeed;
                Monster HitMonster= hitObject.GetComponent<MonsterUpdate>().GetMonster();
                HitMonster.SetHealth(HitMonster.GetHealth() - 1);
            }else{
                animatorControl.Die();
                GameManager.GetInstance().Fail();
            }
                
        }
    }
}
