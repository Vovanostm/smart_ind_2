using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTruckController : MonoBehaviour
{
    public Inventory inventory;
    public InventoryItem item;
    public GameObject target;
    public float yeldSpeed = 10f;
    public float frontSpeed = 3f;
    public GameObject path;
    public Transform waypoints;
    public int targetIndex = 0;
    private Random rnd;
    public float resourceLoadTime = 2f;
    public float resourceCurrentLoadTime = 0f;
    public bool isResourceLoading = false;
    public bool isGrounded = false;
    public Transform groundTransform;
    public float upSpeed = 1f;
    public float flyHeigh = 2f;
    private float distanceToTarget;

    public InventoryItem resource;

    void Start()
    {
        inventory = ScriptableObject.CreateInstance<Inventory>();
        GameController.instance.SetInventory(inventory);
        path = GameObject.Find("Mines");
        waypoints = path.transform;
        NextTarget();
    }

    void Update()
    {
        if (isResourceLoading && !isGrounded)
        {
            SmoothRotate(groundTransform, true, true);
            distanceToTarget = Vector3.Distance(groundTransform.position, transform.position);
            float step = upSpeed * Time.deltaTime;
            if (distanceToTarget < 0.2) step = step / 4;
            transform.position = Vector3.MoveTowards(transform.position, groundTransform.position, step);
            if (distanceToTarget < 0.01)
            {
                isGrounded = true;
            }
        }
        else if (isResourceLoading)
        {
            if (resourceCurrentLoadTime > resourceLoadTime)
            {
                isResourceLoading = false;
                resourceCurrentLoadTime = 0;
                inventory.AddItem(resource);
            }
            resourceCurrentLoadTime += Time.deltaTime;
        }
        else if (isGrounded)
        {
            float step = upSpeed * Time.deltaTime;
            Vector3 flyPosition = transform.position;
            flyPosition.y = flyHeigh;
            transform.position = Vector3.MoveTowards(transform.position, flyPosition, step);
            if (Vector3.Distance(flyPosition, transform.position) < 0.05)
            {
                isGrounded = false;
            }
        }
        else if (target)
        {
            SmoothRotate(target.transform, true);
            transform.Translate(0, 0, frontSpeed * Time.deltaTime);
            Vector3 targetPosition = target.transform.position;
            targetPosition.y = transform.position.y;
            distanceToTarget = Vector3.Distance(targetPosition, transform.position);
            if (distanceToTarget < 0.1)
            {
                var mine = target.GetComponent<MineController>();
                var flyController = target.GetComponent<FlyPathController>();
                if (mine)
                {
                    resource = mine.GetResource();
                    if (resource)
                    {
                        isResourceLoading = true;
                        groundTransform = flyController.groundTransform;
                    }
                }
                NextTarget();
            }
        }

    }

    void SmoothRotate(Transform targetTransform, bool isOnlyY = false, bool isTargetTransform = false)
    {
        Vector3 position = targetTransform.position;
        if (isOnlyY) position.y = transform.position.y;
        var targetRotation = isTargetTransform ? targetTransform.rotation : Quaternion.LookRotation(position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, yeldSpeed * Time.deltaTime);
    }

    void NextTarget()
    {
        if (targetIndex >= waypoints.childCount)
        {
            targetIndex = 0;
        }
        target = waypoints.GetChild(targetIndex).gameObject;
        targetIndex++;
    }
}
