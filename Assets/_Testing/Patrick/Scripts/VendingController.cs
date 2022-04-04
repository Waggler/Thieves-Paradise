using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingController : MonoBehaviour
{
    [SerializeField] private GameObject dispensedItem;
    [SerializeField] private int numberStocked = 1;
    [SerializeField] private Transform outputLoc;
    [SerializeField] private Transform innerLoc;
    [SerializeField] private GameObject button;
    private bool isDispensing;

    public void DispenseItem()
    {
        if (numberStocked <= 0 || isDispensing)
        {
            //put empty sounds and stuff here
            button.SetActive(false); //deactivate button
            return;
        }

        GameObject newItem = Instantiate(dispensedItem, innerLoc.position, outputLoc.rotation);
        newItem.GetComponent<Rigidbody>().isKinematic = true;

        numberStocked--;

        StartCoroutine(MoveItem(newItem));
    }

    private IEnumerator MoveItem(GameObject item)
    {
        isDispensing = true;

        float i = 0;
        while (i < 1)
        {
            item.transform.position = Vector3.Lerp(innerLoc.position, outputLoc.position, i);
            i += Time.deltaTime / 2;
            yield return new WaitForSeconds(Time.deltaTime / 2);
        }

        item.GetComponent<Rigidbody>().isKinematic = false;

        

        isDispensing = false;
    }
}
