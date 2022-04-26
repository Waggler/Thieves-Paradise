using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertHudManager : MonoBehaviour
{
    [SerializeField] private EyeballScript eye;
    [SerializeField] private GameObject hudObject;
    private bool isObjectDisplayed;
    [SerializeField] private float delayTimeToHide = 5f;
    private float timer;

    private GameObject playerRef;
    private GameObject displayedObject;
    private RawImage arrow;
    private Color alpha = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement tempObj = (PlayerMovement)FindObjectOfType(typeof(PlayerMovement));
        playerRef = tempObj.transform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(eye.canCurrentlySeePlayer)
        {
            timer = delayTimeToHide; //reset decay timer

            //show alert
            if(!isObjectDisplayed)
            {
                ShowAlertObject();
            }
        }else if (timer > 0)
        {
            //decay timer
            timer -= Time.deltaTime;
            
        }else //player has not been visible for some time
        {
            //hide alert
            DestroyAlertObject();
        }

        if (isObjectDisplayed)
        {
            //rotate hud object towards this object
            RotateObject();
        }
    }

    private void ShowAlertObject()
    {
        isObjectDisplayed = true;
        displayedObject = Instantiate(hudObject, playerRef.transform);
        displayedObject.transform.parent = null;
        arrow = displayedObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<RawImage>();
    }

    private void DestroyAlertObject()
    {
        Destroy(displayedObject);
        isObjectDisplayed = false;
    }

    private void RotateObject()
    {
        //move to player
        displayedObject.transform.position = playerRef.transform.position;
        //look at the alerted object
        
        displayedObject.transform.LookAt(transform.position);
        //Vector3 lookAngle = playerRef.transform.position;
        //lookAngle.y = transform.position.y;
        //lookAngle = transform.position - lookAngle;
        
        //flatten rotation
        displayedObject.transform.eulerAngles = new Vector3 (0, displayedObject.transform.eulerAngles.y, 0);
        //displayedObject.transform.rotation = Quaternion.Euler(lookAngle);

        //lower alpha on the object to make it look like it's fading?
        alpha.a = timer/delayTimeToHide;
        if (arrow != null) 
        {
            arrow.color = alpha;
        }else
        {
            print("Arrow is Null");
        }
    }
}
