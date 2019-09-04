using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{

    public float speed = 10.0f;
    private float translation;
    private float straffe;
    private Rigidbody rb;
    private Camera camera;
    bool lost = false;
    Text loseText;
    string lt = "ЛОХ";
    public Transform boom;

    void Awake(){
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 300;
    }

    // Use this for initialization
    void Start()
    {
        // turn off the cursor
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        camera = transform.GetChild(0).GetComponent<Camera>();
        loseText = GameObject.Find("LoseText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input.GetAxis() is used to get the user's input
        // You can furthor set it on Unity. (Edit, Project Settings, Input)

        if (!lost)
        {
            translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            straffe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(straffe, 0, translation);
        }
        else
        {
            rb.velocity = Vector3.zero;
            loseText.text = lt;
        }
        if (Input.GetKeyDown("escape"))
        {
            // turn on the cursor
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Transform boomIns = Instantiate(boom) as Transform;
            boomIns.GetComponent<BoomController>().explode = true;
            boomIns.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
            // Debug.Log(camera.transform.rotation);
            // boomIns.rotation = new Quaternion(transform., camera.transform.rotation.y;
            boomIns.position = transform.GetChild(1).position;
            boomIns.forward = camera.transform.forward;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Transform boomIns = Instantiate(boom) as Transform;
            boomIns.GetComponent<BoomController>().explode = false;
            boomIns.GetComponent<Renderer>().material.color = new Color(1, 0, 1, 1);
            // boomIns.rotation = camera.transform.rotation;
            boomIns.position = transform.GetChild(1).position;
            boomIns.forward = camera.transform.forward;
        }
    }

    // void OnCollisionEnter(Collision col)
    // {
    //     if (col.gameObject.tag == "Finish")
    //     {
    //         lost = true;
    //         lt = "ЛОХ!";
    //     }
    // }
}
