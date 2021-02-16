using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int ArrowNumber;
    private int m_BaseballLeft;

    private Vector3 m_PlayerVCompFront = Vector3.zero;
    private Vector3 m_PlayerVCompRight = Vector3.zero;
    private Vector3 m_AimVComp = Vector3.zero;
    private GameObject m_AimObject = null;
    private Camera m_PlayerCam = null;

    private Queue<GameObject> m_ArrowPool;
    public GameObject m_Arrow;
    public int ArrowFireRate;
    private float m_delFire;

    private float deltaSpeed1D = 0.025f;
    public bool m_LevelCompleted = false;

    public SceneChanger m_sceneChanger;
    public bool m_scenePaused = false;

    private bool m_moveForward = false;
    private bool m_moveRight = false;

    //Rotation
    private float m_dx = 1.0f;
    private float m_dy = 1.0f;

    public bool m_PlayerSteppedOnStrigger;

    public ShowMouse m_mouse;

    // Start is called before the first frame update
    void Start()
    {
        //ArrowFireRate = GetComponent<int>(); //For now
        m_BaseballLeft = ArrowNumber;
        m_PlayerCam = GetComponentInChildren<Camera>();
        m_delFire = 0.0f;
        createAimObject();
        buildArrowPool();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_LevelCompleted)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (m_scenePaused)
                {
                    m_sceneChanger.openScene("Unpause");
                    m_scenePaused = false;
                }
                else
                {
                    m_sceneChanger.openScene("PauseScene");
                    m_scenePaused = true;
                }
            }
        }
        else
        {
            m_sceneChanger.openScene("WinScene");
            m_scenePaused = true;
        }

        if (m_scenePaused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            updatePlayerInput();
            updateAimObject();
            Time.timeScale = 1.0f;
            if (m_delFire > 0)
            {
                m_delFire -= Time.deltaTime;
            }
        }
    }

    private void createAimObject()
    {
        m_AimObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Vector3 tempPos = new Vector3(0.0f, transform.position.y, transform.position.z + 2.0f);
        m_AimObject.transform.position = tempPos;
        //m_AimObject.transform.position = Vector3.zero;
        m_AimObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        m_AimObject.GetComponent<Renderer>().material.color = Color.red;
        m_AimObject.GetComponent<Collider>().enabled = false;
    }

    private void updatePlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            m_PlayerVCompFront = deltaSpeed1D * transform.forward;
            m_moveForward = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            m_PlayerVCompFront = -deltaSpeed1D * transform.forward;
            m_moveForward = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            m_PlayerVCompRight = -deltaSpeed1D * transform.right;
            m_moveRight = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            m_PlayerVCompRight = deltaSpeed1D * transform.right;
            m_moveRight = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Firing arrow if delta fire rate to frame is not 0, prevents from spamming
            if (m_delFire <= 0)
            {
                if (m_BaseballLeft > 0)
                {
                    launchArrow();
                    m_BaseballLeft -= 1;
                    m_delFire = ArrowFireRate * Time.deltaTime;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            if (m_moveForward)
            {
                m_PlayerVCompFront = Vector3.zero;
                m_moveForward = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            if (m_moveRight)
            {
                m_PlayerVCompRight = Vector3.zero;
                m_moveRight = false;
            }
        }

        //if (!m_moveForward && !m_moveRight)
        //{
        //    m_PlayerVComp = Vector3.zero;
        //}

        Vector3 m_totalVComp = m_PlayerVCompFront + m_PlayerVCompRight;

        transform.position += new Vector3(m_totalVComp.x, 0.0f, m_totalVComp.z);// * Time.deltaTime;
        m_AimObject.transform.position += new Vector3(m_totalVComp.x, 0.0f, m_totalVComp.z);// * Time.deltaTime;
        //transform.LookAt(m_AimObject.transform.position - transform.position);

        float xDir = Input.GetAxis("Mouse X") / (4.0f * Mathf.PI);

        if (Mathf.Abs(xDir) > 0.0f)
        {
            if ((transform.rotation.y >= 180.0f) || (transform.rotation.y <= -180.0f))
            {
                transform.rotation = new Quaternion(transform.rotation.x, -(transform.rotation.y) + transform.rotation.y / 180.0f, transform.rotation.z, transform.rotation.w);
                m_dx *= -1.0f;
            }
            else
            {
                transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y + xDir * m_dx, transform.rotation.z, transform.rotation.w);
            }
        }

        Debug.Log(xDir);


        //float xDir = Input.GetAxis("Mouse X") * Mathf.Cos(transform.rotation.y);
        //float zDir = Input.GetAxis("Mouse X") * Mathf.Sin(transform.rotation.y);
        float yDir = Input.GetAxis("Mouse Y") / (4.0f * Mathf.PI);

        if (Mathf.Abs(yDir) > 0.0f)
        {
            if ((transform.rotation.x >= 180.0f) || (transform.rotation.x <= -180.0f))
            {
                transform.rotation = new Quaternion(-(transform.rotation.x) + transform.rotation.x / 180.0f, transform.rotation.y, transform.rotation.z, transform.rotation.w);
                m_dy *= -1.0f;
            }
            else
            {
                transform.rotation = new Quaternion(transform.rotation.x - yDir * m_dy, transform.rotation.y, transform.rotation.z, transform.rotation.w);
            }
        }

        Vector3 m_tempdxdydz = (transform.forward - (m_AimObject.transform.position - transform.position).normalized) * 2.0f;

        m_AimObject.transform.position += m_tempdxdydz;

        //m_AimVComp = new Vector3(xDir, yDir, zDir) * 2.0f * deltaSpeed1D;
        //m_AimVComp = new Vector3(0.0f, yDir, 0.0f) * 2.0f * deltaSpeed1D;

        //m_AimObject.transform.position += m_AimVComp;

        transform.LookAt(m_AimObject.transform.position);
    }

    private void updateAimObject()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_AimVComp.y = deltaSpeed1D;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_AimVComp.y = -deltaSpeed1D;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_AimVComp.x = -deltaSpeed1D;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_AimVComp.x = deltaSpeed1D;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            m_AimVComp.y = 0.0f;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            m_AimVComp.x = 0.0f;
        }

        if(Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.x -= Screen.width / 2.0f;
            mousePos.y -= Screen.height / 2.0f;
            m_mouse.lockMouse = true;
            Vector3 ScreenPos = m_PlayerCam.ScreenToWorldPoint(mousePos);
            //Vector3 ScreenPos = m_PlayerCam.ScreenToViewportPoint(mousePos);

            ScreenPos.z = transform.position.z + 2.0f;

            m_AimObject.transform.position = ScreenPos;

            Debug.Log("MousePos" + mousePos);
            Debug.Log("ScreenPos" + ScreenPos);
        }
    }

    private void launchArrow()
    {
        //Find unused arrow

        GameObject temp_UnUsed = getunUsedArrow();

        if (temp_UnUsed != null)
        {
            if (Time.frameCount % ArrowFireRate == 0)
            {
                var tempBullet = getunUsedArrow();
                //tempBullet.transform.SetParent(transform);
            }
        }
    }

    private void buildArrowPool()
    {
        m_ArrowPool = new Queue<GameObject>();

        for (int i = 0; i < ArrowNumber; i++)
        {
            var tempArrow = Instantiate(m_Arrow);
            //tempArrow.transform.SetParent(transform);
            tempArrow.SetActive(false);
            m_ArrowPool.Enqueue(tempArrow);
        }
    }

    private GameObject getunUsedArrow()
    {
        GameObject newArrow = null;
        newArrow = m_ArrowPool.Dequeue();
        newArrow.SetActive(true);
        newArrow.transform.position = transform.position;// + new Vector3(0.0f, 0.0f, 1.0f);
        newArrow.GetComponent<BaseballBehaviour>().setTarget(m_AimObject.transform.position);

        return newArrow;
    }

    public void returnedArrow(GameObject returnedArrow)
    {
        returnedArrow.SetActive(false);
        m_ArrowPool.Enqueue(returnedArrow);

        if  (m_BaseballLeft < ArrowNumber)
        {
            m_BaseballLeft += 1;
        }
    }

    public int getBaseballLeft()
    {
        return m_BaseballLeft;
    }

}
