using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [Header("Lenght of interaction ray")]
    [SerializeField] float distance;
    InventoryScript inventoryScript;
    CinemachineCamera cam;
    Vector3 origin;
    Vector3 direction;
    RaycastHit hitInfo;
    void Awake()
    {
        cam = GetComponentInChildren<CinemachineCamera>();
        inventoryScript = GetComponent<InventoryScript>();
    }
    void Update()
    {
        Debug.DrawRay(origin, direction  * distance, Color.yellow);
    }
    void OnInteract()
    {
        origin = cam.transform.position;
        direction = cam.transform.forward;
        Debug.Log(origin);
        Debug.Log(direction);
        Debug.Log(distance);
        if (Physics.Raycast(origin, direction, out hitInfo, distance))
        {
            if (hitInfo.transform.gameObject.tag == "Item")
            {
                inventoryScript.AddItem(hitInfo.transform.gameObject);
            }
        }
    }
}
