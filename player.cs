using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class player : MonoBehaviour
{
    public bool swipe = true, moveTwo = true;
    public float Hassasiyet;
    public float speed, finalSpeed;

    public Rigidbody rb;
    [HideInInspector]
    public float r;
    public float yataySinir;

    public Vector3 dragDistance;
    [HideInInspector]
    public int horizontalMovement;


 

    float duzGitmeMiktari;

    public Camera touchCamera;
    float offset = 0;



    public static player instance;
    public Rigidbody m_Rigidbody;
    public float m_Speed;
    public float mainSpeed;
    public Animator anim;
    public Transform water;
    public Transform waterTwo;
    public Transform exits;
    public bool move = false;
    public bool start = false;
    public bool exit = false;


    public HealthBar healthBar;
    public int MaxHealth = 100;
    public int currentHealt;
    public GameObject gameOver;
    public GameObject LifeGuard;
    public GameObject playButton;
    public GameObject victory;
    //public GameObject puan;
    public GameObject green;
    public Transform swimmer;

    public Transform lifePoint;

    public GameObject debug;
    public TMP_Text debugText;

   
    public Camera m_MainCamera;
    public Camera m_CameraTwo;

    private void Awake()
    {
       
        m_MainCamera.enabled = true;
        m_CameraTwo.enabled = false;

    }
    void Start()
    {
        instance = this;
        anim = gameObject.GetComponent<Animator>();
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        move = true;
        anim.SetBool("jump", false);
        anim.SetBool("idle", false);

        currentHealt = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);


        gameOver.SetActive(false);
        LifeGuard.SetActive(false);
        playButton.SetActive(false);
        victory.SetActive(false);
        //puan.SetActive(true);
        green.SetActive(false);
        debug.SetActive(true);
        StartCoroutine(coroutineC());


        debugText.text = "Debug"+ Camera.current.transform.position.ToString() + ("\n");


    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 posStart = Vector2.zero, posEnd = Vector2.zero;


        //if (swipe)
        //{



        //    if (swipe)
        //    {

        //        if (InputWrapper.Input.touchCount == 1)
        //        {

        //            Touch touch = InputWrapper.Input.GetTouch(0);
        //            Vector3 curPosition = touchCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Hassasiyet));
        //            if (touch.phase == TouchPhase.Began)
        //            {
        //                offset = rb.position.x - curPosition.x;//Mathf.Abs()

        //            }

        //            if (touch.phase == TouchPhase.Ended)
        //            {
        //                posStart.x = 0;
        //                posEnd.x = 0;
        //                offset = 0;
        //            }
        //            if (touch.phase == TouchPhase.Moved)
        //            {
        //                posEnd = touch.position;
        //                if (Mathf.Abs(posStart.x - posEnd.x) > 10f)
        //                {
        //                    r = curPosition.x + offset;
        //                }

        //                if (r >= yataySinir) r = yataySinir;
        //                if (r <= -yataySinir) r = -yataySinir;
        //                if (r > 1f && (Mathf.Abs(touch.deltaPosition.x) > 1))
        //                {
        //                    horizontalMovement = -1;
        //                    duzGitmeMiktari = 0;
        //                }
        //                else if (r < -1f && (Mathf.Abs(touch.deltaPosition.x) > 1))
        //                {
        //                    horizontalMovement = 1;
        //                    duzGitmeMiktari = 0;
        //                }

        //                if (Mathf.Abs(touch.deltaPosition.x) < 1) { duzGitmeMiktari += Time.deltaTime; if (duzGitmeMiktari > 0.5f) horizontalMovement = 0; }

        //            }


        //        }

        //        else if (InputWrapper.Input.touchCount == 0)
        //        {
        //        }
        //        if (rb.transform.position.x >= yataySinir) rb.transform.position = new Vector3(yataySinir, rb.transform.position.y, rb.transform.position.z);
        //        if (rb.transform.position.x <= -yataySinir) rb.transform.position = new Vector3(-yataySinir, rb.transform.position.y, rb.transform.position.z);

        //        transform.position =
        //                    new Vector3(r, transform.position.y, transform.position.z);

        //    }
        //    print("r: " + r);
        //    if (moveTwo)
        //        transform.position += new Vector3(0, 0f, speed * Time.deltaTime);

        //}

        if (currentHealt <= 0)
        {
           
            green.SetActive(true);

        }

        if (move)
        {
            //Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, mainSpeed);
            //m_Rigidbody.MovePosition(transform.position + m_Input * Time.deltaTime * m_Speed);

            //Mobil
            Vector3 m_Input = new Vector3(CrossPlatformInputManager.GetAxisRaw("Horizontal"), 0, mainSpeed /*CrossPlatformInputManager.GetAxisRaw("Vertical")*/);
            m_Rigidbody.MovePosition(transform.position + m_Input * Time.deltaTime * m_Speed);
        }

        if(start)
        {
            move = false;
            mainSpeed = 25;
         
            transform.position = Vector3.MoveTowards(transform.position, water.position, mainSpeed * Time.deltaTime);
           
        }

        if(exit)
        {
            transform.position = Vector3.MoveTowards(transform.position, exits.position, mainSpeed * Time.deltaTime);
        }



        if (transform.localPosition.x > 41)
        {
            transform.localPosition = new Vector3(41f, transform.localPosition.y, transform.localPosition.z);
        }
        else if (transform.localPosition.x < 4f)
        {
            transform.localPosition = new Vector3(4f, transform.localPosition.y, transform.localPosition.z);
        }

    }
    private void TakeDamage(int damage)
    {
        currentHealt -= damage;
        healthBar.SetHealth(currentHealt);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "puan")
        {       
            TakeDamage(5);
        }

        if (other.gameObject.tag=="jump")
        {
            anim.SetBool("jump", true);
            m_Rigidbody.constraints = RigidbodyConstraints.FreezePosition;
            StartCoroutine(coroutineA());
            TakeDamage(5);
            Debug.Log(currentHealt);
            
        }
        if (other.gameObject.tag == "swimming")
        {
            transform.position = waterTwo.position;
            start = true;
            anim.SetBool("jump", false);
            anim.SetBool("swim", true);
            //m_Rigidbody.useGravity = false;
            //m_Rigidbody.constraints = RigidbodyConstraints.None;
            debugText.text = "Debug:" + "Rescue operation started" + ("\n");
            
            m_MainCamera.enabled = false;
            m_CameraTwo.enabled = true;
            
        }
        if (other.gameObject.tag == "swimexit")
        {
            mainSpeed = 1;
            anim.SetBool("swim", false);
            anim.SetBool("jump", true);
            m_Rigidbody.useGravity = true;
            StartCoroutine(coroutineB());
            exit = false;
            move = true;
            start = false;
            m_MainCamera.enabled = true;
            m_CameraTwo.enabled = false;
        }
        if (other.gameObject.tag == "swimmer")
        {
            exit = true;
            move = false;
            start = false;


            if (currentHealt > 0)
            {
                transform.position = water.position;
                debugText.text = "Debug:" + "Rescue operation failed" + ("\n"); 

            }
            else if (currentHealt <= 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z); 
                other.gameObject.transform.parent = this.transform;
                other.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
             
                //other.gameObject.transform.position = lifePoint.position;
                other.gameObject.GetComponent<Animator>().SetBool("carry", true);
                //other.GetComponent<BoxCollider>().enabled = false;
                Destroy(other.gameObject.GetComponent<BoxCollider>());
                //other.gameObject.transform.parent = transform.GetChild(1).GetChild(2);
                debugText.text = "Debug:" + "rescue operation was successful" + ("\n");


            }


        }
        if (other.gameObject.tag == "chair")
        {
            move = false;
            anim.SetBool("idle", true);
            anim.SetBool("jump", false);
            victory.SetActive(true);
            playButton.SetActive(true);
            healthBar.SetMaxHealth(MaxHealth);
            swimmer.position = lifePoint.position;
            if (currentHealt > 0)
            {
                LifeGuard.SetActive(true);
                gameOver.SetActive(true);
                playButton.SetActive(true);
                victory.SetActive(false);


            }
            else if (currentHealt <= 0)
            {
                LifeGuard.SetActive(true);
                gameOver.SetActive(false);
                playButton.SetActive(true);
                victory.SetActive(true);

            }


        }
    }

    IEnumerator coroutineC()
    {
        yield return new WaitForSeconds(1.0f);
        //puan.SetActive(false);
      
    }
    IEnumerator coroutineB()
    {
        yield return new WaitForSeconds(1.0f);    
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    IEnumerator coroutineA()
    {   
        yield return new WaitForSeconds(1.0f);
        m_Rigidbody.constraints = RigidbodyConstraints.None;
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("jump", false);
         
    }
    public void play()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene("");
        Time.timeScale = 1f;
    }

    public void win()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }









}
