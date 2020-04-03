using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public InventoryItem item;
    public int resourceCount = 0;
    public int maxCapacity = 5;
    public float creationTime = 10f;
    public float currentTime = 0f;
    public Transform groundTransform;
    public float power = 80;

    public UIMineController uiController;

    private void Start()
    {
        if (uiController)
        {
            uiController.setValueListener(updatePower);
            uiController.prodMaxCapacity = maxCapacity;
            uiController.img.sprite = item.sprite;
            updatePower(power);
        }
    }

    void Update()
    {
        if (currentTime >= creationTime / power * 100)
        {
            currentTime = 0;
            resourceCount++;

            if (uiController)
            {
                uiController.prodPercent = 0;
                uiController.prodCurrentCapacity = resourceCount;
            }
        }
        if (resourceCount < maxCapacity)
        {
            if (uiController) uiController.prodPercent = currentTime / (creationTime / power * 100);
            currentTime += Time.deltaTime;
        }

    }

    public InventoryItem GetResource()
    {
        if (resourceCount == 0) return null;
        resourceCount--;
        return item;
    }

    public void updatePower(float p)
    {
        power = p;
        uiController.prodCurrentSpeed = Mathf.Round((60 / creationTime) * (power / 100) * 100) / 100;
    }
}
