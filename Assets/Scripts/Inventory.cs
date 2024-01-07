using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;

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
    public Transform dropLocation;

    [Header("Drag and Drop")]
    public Image dragIconImage;
    private Item currentDraggedItem;
    [SerializeField] Item shortsword;
    private int currentDragSlotIndex = -1;

    [SerializeField] CombatController combatController;
    private Weapon weapon;

    public void Start()
    {
        toggleInventory(false);
        foreach (Slot uiSlot in InventorySlots)
        {
            uiSlot.initialiseSlot();
        }
        addItemToInventory(shortsword);
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            dropItem();
        }

        // check if item is in inventory when active
        if (Input.GetKeyDown(KeyCode.F) && inventory.activeInHierarchy)
        {
            // Check if an item with the name "YourItemName" is in the inventory
            Item weaponItem = IsItemInInventory(combatController.GetActiveWeapon());
            Debug.Log("active weapon: " + combatController.GetActiveWeapon());

            if (weaponItem)
            {
                //check and do enhancement if possible
                EnhanceItem(weaponItem);
                
            }
        } //end of enhancement

        dragIconImage.transform.position = Input.mousePosition;

    }

    public void EnhanceItem(Item weaponItem)
    {
        int totalQuantity = 0;
        

        // Iterate through all inventory slots and accumulate the quantity of the specified item
        foreach (Slot slot in InventorySlots)
        {
            Item item = slot.getItem();
            if (item != null && item.name == weaponItem.name)
            {
                totalQuantity += item.currentQuantity;

            }
        }
        if (totalQuantity > 1)
        {
            // Increase enhancement level by 1
            
            Debug.Log("Enhancing item: " + weaponItem.name + ", new quantity: " + weaponItem.currentQuantity);

            foreach (Weapon weapon in combatController.Weapons)
            {
                if (weaponItem.name == weapon.name)
                {
                    weapon.enhancementLevel += 1;
                    weapon.base_attack = weapon.enhancementLevel * weapon.base_attack;
                    dropItem();
                    weaponItem.gameObject.SetActive(false);
                    Destroy(weaponItem.gameObject);
                }
            }
        }
        else
        {
            // There is only 1 item, enhancement is not allowed
            Debug.Log("Cannot enhance. Only 1 item available.");
        }
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
                        EnableWeapon(newItem);
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

    private void dropItem()
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            Slot curSlot = InventorySlots[i];
            if (curSlot.hovered && curSlot.hasItem())
            {
                curSlot.getItem().gameObject.SetActive(true);

                curSlot.getItem().transform.position = dropLocation.position;
                curSlot.setItem(null);
                break;
            }
        }
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
        dragIconImage.color = new Color(1, 1, 1, 0);
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


    //weapon usage/combat related functions
    public Item IsItemInInventory(GameObject itemName)
    {
        foreach (Slot slot in InventorySlots)
        {
            Item item = slot.getItem();
            // Debug.Log("inventory item "+item.name);

            if (item != null && item.name == itemName.name)
            {
                // Debug.Log("inventory item " + item.name);
                return item;
            }
        }

        return null;
    }

    public void EnableWeapon(Item item)
    {
        foreach (Weapon weapon in combatController.Weapons)
        {
            Debug.Log("code runs enableweapon");
            if (item.name == weapon.name)
            {
                weapon.is_enabled = true;
            }
        }
    }
}


