using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script manages all player animations. It will check for any player input, and then play the appropriate animation. */

public class AnimationController : MonoBehaviour
{
    Animator anim;
    Vector3 direction;  //used to get x and z values of player movement input.

    //animation states
    int robotIdle;
    int robotWalkL;
    int robotWalkR;
    int robotWalkF;
    int robotWalkB;
    int robotSprinting;

    RobotMovement rm;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        robotIdle = Animator.StringToHash("Idle");      //StringToHash makes it easier to handle changing animations.
        robotWalkF = Animator.StringToHash("WalkF");
        robotWalkB = Animator.StringToHash("WalkB");
        robotWalkL = Animator.StringToHash("WalkL");
        robotWalkR = Animator.StringToHash("WalkR");
        robotSprinting = Animator.StringToHash("Sprint");

        rm = RobotMovement.instance; 
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        //set animation
        
       
        if (direction.x < 0)
        {
            anim.SetBool(robotWalkL, true);
            anim.SetBool(robotIdle, false);
        }
        else
        {
            anim.SetBool(robotWalkL, false);
        }

        if (direction.x > 0)
        {
            anim.SetBool(robotWalkR, true);
            anim.SetBool(robotIdle, false);
        }
        else
        {
            anim.SetBool(robotWalkR, false);
        }

        //For forward and back movement, need to check which direction player is facing. For example, if player inputs up while looking in the
        //opposite direction, the walk forward animation will play when in reality it should be the walk backward animation.
        if (direction.z > 0)
        {
            //player can only sprint while moving forward
            if(rm.isSprinting)
            {
                anim.SetBool(robotSprinting, true);
                anim.SetBool(robotIdle, false);
            }
            else
            {
                Debug.Log("walking forward");
                anim.SetBool(robotWalkF, true);
                anim.SetBool(robotIdle, false);
                anim.SetBool(robotSprinting, false);
            }
        }
        else
        {
            anim.SetBool(robotWalkF, false);
        }

        if (direction.z < 0)
        {
            Debug.Log("Walking backwards");
            anim.SetBool(robotWalkB, true);
            anim.SetBool(robotIdle, false);
        }
        else
        {
            anim.SetBool(robotWalkB, false);
        }
          
        //check if player is idle
        if (direction.x == 0 && direction.z == 0)
            anim.SetBool(robotIdle, true);

    }
}
