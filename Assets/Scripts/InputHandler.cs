using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
	Command leftButton;
	Command rightButton;

	public void Start()
	{
		SetupInput();
	}

	public void SetupInput()
	{
		leftButton = new GoLeftCommand();
		rightButton = new GoRightCommand();
	}

	public Command HandleInput()
	{
        //input for computer
    
        //if (Input.GetAxis("Horizontal") > 0)
        //        return rightButton;
        //    else if (Input.GetAxis("Horizontal") < 0)
        //        return leftButton;

        // input for mobile

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            int screenCenter = Screen.width / 2;

            if (touch.position.x > screenCenter)
                return rightButton;
            else
                return leftButton;
        }

        return null;
	}

}
