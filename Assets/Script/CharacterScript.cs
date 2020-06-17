using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    //calculates the vector needed to reset the position once a key is released
    Vector3 neededVector;

    //Both vectors are used to make sure that the character resets its position accurately
    Vector3 prevCharPos;
    Vector3 newCharPos;

    public float moveSpeed = 10f;

    //If the character twitches when reseting its position, decrease this value
    public float roundingInt = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        prevCharPos = transform.position;
        //the 4 following if statements make sure that the character doesn't go beyong the limits of the frame
        if(transform.position.y > 1f)
        {
            transform.position = new Vector3(0, 1f, 0);
        }
        else if(transform.position.y < -1f)
        {
            transform.position = new Vector3(0, -1f, 0);
        }
        else if (transform.position.x < -1f)
        {
            transform.position = new Vector3(-1f, 0, 0);
        }
        else if (transform.position.x > 1f)
        {
            transform.position = new Vector3(1f, 0, 0);
        }

        //Movement: 4 if statements
        if ((Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow)) && transform.position.y < 1f  && transform.position.x == 0)
        {
            transform.Translate(new Vector3(0, moveSpeed, 0) * Time.deltaTime);
        }
        else if((Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow)) && transform.position.y > -1f && transform.position.x == 0)
        {
            transform.Translate(new Vector3(0, -moveSpeed, 0) * Time.deltaTime);
        }
        else if ((Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow)) && transform.position.x < 1f && transform.position.y == 0)
        {
            transform.Translate(new Vector3(moveSpeed, 0, 0) * Time.deltaTime);
        }
        else if ((Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow)) && transform.position.x > -1f && transform.position.y == 0)
        {
            transform.Translate(new Vector3(-moveSpeed, 0, 0) * Time.deltaTime);
        }
        //Gradually resets position if no button is pressed
        else if ((transform.position.y != 0 && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)  && !Input.GetKey("w") && !Input.GetKey("s")) || (transform.position.x != 0 && !Input.GetKey("a") && !Input.GetKey("d") && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)))
        {
            neededVector = new Vector3(0, 0, 0) - transform.position;
            neededVector = neededVector / neededVector.magnitude;
            transform.Translate(neededVector * moveSpeed * Time.deltaTime);
        }
        newCharPos = transform.position;
        //If the character is negligibly close to the origin, just set its position to the origin
        if (Mathf.Round(transform.position.y * roundingInt) == 0 && Mathf.Round(transform.position.x * roundingInt) == 0)
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }
    
}
