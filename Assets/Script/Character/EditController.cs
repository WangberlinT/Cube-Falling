using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditController : MonoBehaviour
{
    //------------Move Control Public-------------
    public float speed = 5;

    //------------View Control Public-------------
    public float sensitive = 180f;
    public float up_max = 90f;
    public float down_max = -90f;

    //------------Selection Control Public----------------
    public float selectDistance = 5f;

    //------------Build Public------------
    public Transform breakBlock;
    public Transform placeBlock;
    public GameObject playerModel;

    //------------View Control Private-------------
    private Transform camTrans;
    private float rotationX = 0f;
    private float rotationY = 0f;

    //------------Build-------------------------
    private int selectedBlockIndex = 1;
    private World world;
    private Text promptText;
    private bool playerPositionMode = false;
    private bool enermyPositionMode = false;

    //Debug
    private DebugScreen screen;
    private const string TAG = "EditController";


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        camTrans = transform.Find("Main Camera");
        screen = DebugScreen.GetInstance();
        world = GameObject.Find("World").GetComponent<World>();
        promptText = GameObject.Find("Selection prompt").GetComponent<Text>();
    }

    void FixedUpdate()
    {
        float moveY = 0;
        float moveX = Input.GetAxis("Horizontal") * speed;
        float moveZ = Input.GetAxis("Vertical") * speed;
        if (Input.GetKey(KeyCode.Space))
            moveY = speed;
        else if (Input.GetKey(KeyCode.LeftAlt))
            moveY = -speed;

        Vector3 move = transform.right * moveX + transform.forward * moveZ + transform.up * moveY;
        move *= Time.fixedDeltaTime;
        transform.position += move;
    }

    void Update()
    {
            placeCursorBlocks();
            GetPlayerInput();
            ViewRotate();
    }

    private void GetPlayerInput()
    {

        float scrool = Input.GetAxis("Mouse ScrollWheel");
        int length = (int)CubeType.Length;

        if (Input.GetKeyDown(KeyCode.P))
            playerPositionMode = !playerPositionMode;

        if (playerPositionMode)
            screen.Log("Mode", "player position mode");
        else
            screen.Log("Mode", "Cube Mode");
            

        if(!playerPositionMode && !enermyPositionMode)
        {
            if (scrool > 0)
            {
                selectedBlockIndex = (selectedBlockIndex + 1) % length;
            }
            else if (scrool < 0)
            {
                int complete = length - 1;
                selectedBlockIndex = (selectedBlockIndex + complete) % length;
            }

            promptText.text = ((CubeType)selectedBlockIndex).ToString() + " selected";

            if (breakBlock.gameObject.activeSelf)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    screen.Log("Click", "Left");
                    BreakBlock();
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    screen.Log("Click", "Right");
                    CreateBlock();
                }
            }
        }
        else if(playerPositionMode)
        {
            //right click place player
            if (Input.GetMouseButtonDown(1))
            {
                SetPlayerPosition();
                screen.Log("Click", "Right");
            }
                
        }




    }
    private void SetPlayerPosition()
    {
        world.spawnPos = playerModel.transform.position;
        Instantiate(playerModel);
        playerPositionMode = false;
    }

    private void CreateBlock()
    {
        if (selectedBlockIndex != 0)
            world.SetCube(placeBlock.position, (CubeType)selectedBlockIndex);

    }
    private void BreakBlock()
    {
        world.BreakBlock(breakBlock.position);
    }

    private void ViewRotate()
    {
        rotationX -= Input.GetAxis("Mouse Y") * sensitive * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, down_max, up_max);
        rotationY += Input.GetAxis("Mouse X") * sensitive * Time.deltaTime;

        //rotate
        transform.localEulerAngles = new Vector3(0, rotationY, 0);
        camTrans.localEulerAngles = new Vector3(rotationX, 0, 0);
    }
    private void placeCursorBlocks()
    {
        float step = 0.5f;
        Ray ray = new Ray(camTrans.position, camTrans.forward);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * selectDistance, Color.red);



        RaycastHit hitt = new RaycastHit();
        Physics.Raycast(ray, out hitt, selectDistance);

        Vector3 endPos = ray.origin + ray.direction * selectDistance;

        if (hitt.transform != null)
        {
            endPos = hitt.point;

            Vector3 placePos = endPos - ray.direction * step;
            placePos.x = Mathf.FloorToInt(placePos.x);
            placePos.y = Mathf.FloorToInt(placePos.y);
            placePos.z = Mathf.FloorToInt(placePos.z);
            Vector3 breakPos = endPos + ray.direction * step;
            breakPos.x = Mathf.FloorToInt(breakPos.x);
            breakPos.y = Mathf.FloorToInt(breakPos.y);
            breakPos.z = Mathf.FloorToInt(breakPos.z);

            if(!playerPositionMode && !enermyPositionMode)
            {
                breakBlock.position = breakPos;
                placeBlock.position = placePos;

                breakBlock.gameObject.SetActive(true);
                placeBlock.gameObject.SetActive(true);
            }
            else if(playerPositionMode)
            {
                playerModel.transform.position = endPos;
                playerModel.gameObject.SetActive(true);
            }
            
        }
        else
        {
            playerModel.gameObject.SetActive(false);
            breakBlock.gameObject.SetActive(false);
            placeBlock.gameObject.SetActive(false);
        }
        string debugMessage = string.Format("End point: {0}", endPos);
        screen.Log(TAG, debugMessage);
        Debug.DrawLine(ray.origin, endPos, Color.red);
    }

    
}
