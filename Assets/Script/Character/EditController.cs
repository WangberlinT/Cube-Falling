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
    public GameObject enermyModelPrefab;
    private GameObject enermyModel;

    //------------View Control Private-------------
    private Transform camTrans;
    private float rotationX = 0f;
    private float rotationY = 0f;

    //------------Build-------------------------
    private int selectIndex = 1;
    private World world;
    private Text promptText;
    private EditMode editMode = EditMode.CubeMode;
    public Vector3 enermyPosOffset = new Vector3(0.5f, 0, 0.5f);

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
        //设置Enermy默认模型
        enermyModelPrefab = PrefabManager.GetInstance().GetPrefab(PrefabType.Breaker);
        InitMonsterModel();
    }

    void FixedUpdate()
    {
        float moveY = 0;
        float moveX = Input.GetAxis("Horizontal") * speed;
        float moveZ = Input.GetAxis("Vertical") * speed;
        if (Input.GetKey(KeyCode.Space))
            moveY = speed;
        else if (Input.GetKey(KeyCode.LeftShift))
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

        if (Input.GetKeyDown(KeyCode.E))
            IncreaseEditMode(true);
        if (Input.GetKeyDown(KeyCode.Q))
            IncreaseEditMode(false);
        bool leftClick = Input.GetMouseButtonDown(0);
        bool rightClick = Input.GetMouseButtonDown(1);

        if (editMode == EditMode.CubeMode)
        {
            int length = (int)CubeType.Length;
            UpdateSelectIndex(length, scrool);

            if (breakBlock.gameObject.activeSelf)
            {
                if (leftClick)
                {
                    screen.Log("Click", "Left");
                    BreakBlock();
                }
                else if (rightClick)
                {
                    screen.Log("Click", "Right");
                    CreateBlock();
                }
            }
        }
        else if (editMode == EditMode.PlayerMode)
        {
            //right click place player
            if (rightClick)
            {
                SetPlayerPosition();
                screen.Log("Click", "Right");
            }

        }
        else if (editMode == EditMode.MonsterMode)
        {
            //Monster Mode
            int length = (int)MonsterType.LENGTH;
            UpdateSelectIndex(length, scrool);
            
            //TODO: setMonster
            if (rightClick)
                SetMonster((MonsterType)selectIndex);
        }




        UpdatePrompt();
    }
    private void SetPlayerPosition()
    {
        world.spawnPos = playerModel.transform.position;
        Instantiate(playerModel);
        editMode = EditMode.CubeMode;
    }
    //TODO: SetMonsterInfo
    private void SetMonster(MonsterType type)
    {
        Debug.Log("Set Monster");
        Instantiate(enermyModel);
        MonsterData data = new MonsterData(enermyModel.transform.position,type);
        world.AddMonsterData(data);
    }

    private void InitMonsterModel()
    {
        if (enermyModel == null)
            enermyModel = Instantiate(enermyModelPrefab);
        else
        {
            Destroy(enermyModel);
            enermyModel = Instantiate(enermyModelPrefab);
        }
        //只保留模型
        Destroy(enermyModel.GetComponent<Rigidbody>());
        Destroy(enermyModel.GetComponentInChildren<BoxCollider>());
    }
    

    private void UpdateSelectIndex(int length, float scrool)
    {
        if(scrool != 0)
        {
            if (scrool > 0)
            {
                selectIndex = (selectIndex + 1) % length;
            }
            else if (scrool < 0)
            {
                int complete = length - 1;
                selectIndex = (selectIndex + complete) % length;
            }

            if(editMode == EditMode.MonsterMode)
            {
                enermyModelPrefab = PrefabManager.GetInstance().GetPrefabByMonsterType((MonsterType)selectIndex);
                InitMonsterModel();
            }
        }
        
    }

    private void UpdatePrompt()
    {
        promptText.text = "Mode: " + editMode.ToString()+ " ";
        if (editMode == EditMode.CubeMode)
            promptText.text += ((CubeType)selectIndex).ToString() + " selected";
        
    }

    private void CreateBlock()
    {
        if (selectIndex != 0)
            world.SetCube(placeBlock.position, (CubeType)selectIndex);

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

            if(editMode == EditMode.CubeMode)
            {
                breakBlock.position = breakPos;
                placeBlock.position = placePos;

                breakBlock.gameObject.SetActive(true);
                placeBlock.gameObject.SetActive(true);
                
            }
            else if(editMode == EditMode.PlayerMode)
            {
                playerModel.transform.position = endPos;
                playerModel.gameObject.SetActive(true);
                breakBlock.gameObject.SetActive(false);
                placeBlock.gameObject.SetActive(false);
            }
            else if(editMode == EditMode.MonsterMode)
            {
                screen.Log("[MonsterEdit]", endPos.ToString());
                //TODO: 获取实际大小
                enermyModel.transform.position = placePos + enermyPosOffset;
                enermyModel.SetActive(true);
                breakBlock.gameObject.SetActive(false);
                placeBlock.gameObject.SetActive(false);
                
            }
            
        }
        else
        {
            playerModel.gameObject.SetActive(false);
            breakBlock.gameObject.SetActive(false);
            placeBlock.gameObject.SetActive(false);
            enermyModel.SetActive(false);
        }
        string debugMessage = string.Format("End point: {0}", endPos);
        screen.Log(TAG, debugMessage);
        Debug.DrawLine(ray.origin, endPos, Color.red);
    }

    private void IncreaseEditMode(bool increase)
    {
        int mode = (int)editMode;
        int len = (int)EditMode.LENGTH;
        if (increase)
            editMode = (EditMode)((mode + 1) % len);
        else
            editMode = (EditMode)((mode + len - 1) % len);
        //Reset selectIndex
        selectIndex = 0;
        screen.Log("Mode", editMode.ToString());
    }

    
}

public enum EditMode
{
    CubeMode,PlayerMode,MonsterMode,
    LENGTH
}
