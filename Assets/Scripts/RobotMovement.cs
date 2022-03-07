using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script handles player input */
public class RobotMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    float speedMod;     //adds to the speed value. mods to speed comes from sprinting and any items.
    float baseSpeed {get;} = 6000;

    private Vector3 direction;
    private Vector3 lookDirection;
    Plane plane = new Plane(Vector3.up, Vector3.zero);
    float distance;
    public bool isSprinting;


    //[SerializeField]
    private Rigidbody rb;

    public static RobotMovement instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speedMod = 0; 
    }
    private void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
                
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }

        //check if player is sprinting TODO: should eventually check for input from a gamepad
        bool leftShiftPressed = Input.GetKey(KeyCode.LeftShift);
        if(leftShiftPressed)
        {
            //sprint!
            isSprinting = true;
            speedMod = baseSpeed;
            Debug.Log("holding shift");
        }
        else
        {
            isSprinting = false;
            speedMod = 0;
        }

        speed = baseSpeed + speedMod;


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.Acceleration);

    }
}
