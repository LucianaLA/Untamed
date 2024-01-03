using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [Header("UI")]
    public GameObject inventory; //reference to inventory object
    public List<Slot> InventorySlots = new List<Slot>();
    public Image crosshair;
    public TMP_Text itemHoverText;

    [Header("Raycase")]
    public float raycastDistance = 5f;
    public LayerMask itemLayer;

    [Header("Drag and Drop")]
    public Image dragIconImage;
    private Item currentDraggedItem;
    private int currentDragSlotIndex = -1;

    public void Start()
    {
        toggleInventory(false);
        foreach (Slot uiSlot in InventorySlots)
        {
            uiSlot.initialiseSlot();
        }
    }

    public void Update()
    {
        itemRaycast(Input.GetMouseButtonDown(0));
        if (Input.GetKeyDown(KeyCode.E))
        {
            toggleInventory(!inventory.activeInHierarchy);
        }
        if (inventory.activeInHierarchy && Input.GetMouseButtonDown(0))
        {
            dragInventoryIcon();
        }
        else if (currentDragSlotIndex != -1 && Input.GetMouseButtonUp(0) || currentDragSlotIndex != 1 && !inventory.activeInHierarchy)
        {
            dropInventoryIcon();
        }

        dragIconImage.transform.position = Input.mousePosition;

    }

    public void itemRaycast(bool hasClicked = false)
    {
        itemHoverText.text = "";
        Ray ray = Camera.main.ScreenPointToRay(crosshair.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, itemLayer))
        {
            if (hit.collider != null) //all items on the item layer should be items so shouldn;t need to look for a tag
            {
                if (hasClicked)
                {
                    Item newItem = hit.collider.GetComponent<Item>();
                    if (newItem)
                    {
                        addItemToInventory(newItem);
                    }
                }
                else
                {
                    Item newItem = hit.collider.GetComponent<Item>();
                    if (newItem)
                    {
                        itemHoverText.text = newItem.name;
                    }
                }
            }
        }
    }

    private void addItemToInventory(Item itemToAdd)
    {
        int leftoverQuantity = itemToAdd.currentQuantity;
        Slot openSlot = null;
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            Item heldItem = InventorySlots[i].getItem();

            if (heldItem != null && itemToAdd.name == heldItem.name)
            {
                int freeSpaceInSlot = heldItem.maxQuantity - heldItem.currentQuantity;

                if (freeSpaceInSlot >= leftoverQuantity)
                {
                    heldItem.currentQuantity += leftoverQuantity;
                    Destroy(itemToAdd.gameObject);
                    InventorySlots[i].updateData();
                    return;
                }
                else
                {
                    heldItem.currentQuantity = heldItem.maxQuantity;
                    leftoverQuantity -= freeSpaceInSlot;
                }
            }
            else if (heldItem == null)
            {
                if (!openSlot)
                {
                    openSlot = InventorySlots[i];
                }

            }
            InventorySlots[i].updateData();
        }
        if (leftoverQuantity > 0 && openSlot)
        {
            openSlot.setItem(itemToAdd);
            itemToAdd.currentQuantity = leftoverQuantity;
            itemToAdd.gameObject.SetActive(false);
        }
        else
        {
            itemToAdd.currentQuantity = leftoverQuantity;
        }
    }

    private void toggleInventory(bool enable)
    {
        inventory.SetActive(enable);

        Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = enable;

        ////diable the rotation of the camera
        //Camera.main.GetComponent<FPSController>().sensitivity = enable ? 0 : 2;
    }

    private void dragInventoryIcon()
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            Slot curSlot = InventorySlots[i];
            if (curSlot.hovered && curSlot.hasItem())
            {
                currentDragSlotIndex = i; //update current rag slot index variable

                currentDraggedItem = curSlot.getItem(); //get item from current slot
                dragIconImage.sprite = currentDraggedItem.icon;
                dragIconImage.color = new Color(1, 1, 1, 1);//make the follow mouse icon visible

                curSlot.setItem(null); //remove item from previous slot
            }
        }
    }

    private void dropInventoryIcon()
    {
        dragIconImage.sprite = null;
        dragIconImage.color = new Color(1, 1, 1, 1);
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            Slot curSlot = InventorySlots[i];
            if (curSlot.hovered)
            {
                if (curSlot.hasItem())//swap items
                {
                    Item itemToSwap = curSlot.getItem();

                    curSlot.setItem(currentDraggedItem);

                    InventorySlots[currentDragSlotIndex].setItem(itemToSwap);

                    resetDragVariables();
                    return;
                }
                else //place item with no swap
                {
                    curSlot.setItem(currentDraggedItem);

                    resetDragVariables();
                    return;
                }
            }
        }

        InventorySlots[currentDragSlotIndex].setItem(currentDraggedItem);
        resetDragVariables();
    }

    private void resetDragVariables()
    {
        currentDraggedItem = null;
        currentDragSlotIndex = -1;
    }
}
