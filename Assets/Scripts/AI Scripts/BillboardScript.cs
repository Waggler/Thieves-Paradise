using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool isDebug;   
    [SerializeField] private GameObject self;

    #region Awake
    private void Awake()
    {
        //Checkin to see if there is already an object assigned to the target var
        ////self.SetActive(false);

        //Used to disable or enable the debug text for objects depending on if a bool is true or false
        if (isDebug == true)
        {
            self.SetActive(true);
        }
        else if (isDebug == false)
        {
            self.SetActive(false);
        }

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("PlayerVisionTarget").transform;
        }
    }

    #endregion

    #region Update
    void Update()
    {
        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = lookRotation;
    }
    #endregion
}
