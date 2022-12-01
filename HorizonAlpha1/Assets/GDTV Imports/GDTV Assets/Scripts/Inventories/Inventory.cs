using System;
using UnityEngine;
using GameDevTV.Saving;

namespace GameDevTV.Inventories
{
    /// <summary>
    /// Provides storage for the player inventory. A configurable number of
    /// slots are available.
    ///
    /// This component should be placed on the GameObject tagged "Player".
    /// </summary>
    public class Inventory : MonoBehaviour
    {
        // CONFIG DATA
        [Tooltip("Allowed size")]
        [SerializeField] int inventorySize = 16;

        // STATE
        InventoryItem[] slots;

        // PUBLIC

        /// <summary>
        /// Broadcasts when the items in the slots are added/removed.
        /// </summary>
        public event Action inventoryUpdated;

        /// <summary>
        /// Convenience for getting the player's inventory.
        /// </summary>
        public static Inventory GetPlayerInventory()
        {
            var player = GameObject.FindWithTag("Player");
            return player.GetComponent<Inventory>();
        }

        /// <summary>
        /// Could this item fit anywhere in the inventory?
        /// </summary>
        public bool HasSpaceFor(InventoryItem item)
        {
            return FindSlot(item) >= 0;
        }

        /// <summary>
        /// How many slots are in the inventory?
        /// </summary>
        public int GetSize()
        {
            return slots.Length;
        }

        /// <summary>
        /// Attempt to add the items to the first available slot.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>Whether or not the item could be added.</returns>
        public bool AddToFirstEmptySlot(InventoryItem item)
        {
            int i = FindSlot(item);

            if (i < 0)
            {
                return false;
            }

            slots[i] = item;
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
            return true;
        }

        /// <summary>
        /// Is there an instance of the item in the inventory?
        /// </summary>
        public bool HasItem(InventoryItem item)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (object.ReferenceEquals(slots[i], item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Return the item type in the given slot.
        /// </summary>
        public InventoryItem GetItemInSlot(int slot)
        {
            return slots[slot];
        }

        /// <summary>
        /// Remove the item from the given slot.
        /// </summary>
        public void RemoveFromSlot(int slot)
        {
            print(slot + "Removing item " + slots[slot]);
            slots[slot] = null;
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
        }

        /// <summary>
        /// Will add an item to the given slot if possible. If there is already
        /// a stack of this type, it will add to the existing stack. Otherwise,
        /// it will be added to the first empty slot.
        /// </summary>
        /// <param name="slot">The slot to attempt to add to.</param>
        /// <param name="item">The item type to add.</param>
        /// <returns>True if the item was added anywhere in the inventory.</returns>
        public bool AddItemToSlot(int slot, InventoryItem item)
        {
            print(slot + "Adding item " + item);
            if (slots[slot] != null)
            {
                return AddToFirstEmptySlot(item); ;
            }

            slots[slot] = item;
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
            return true;
        }

        // PRIVATE

        private void Awake()
        {
            slots = new InventoryItem[inventorySize];
            slots[0] = InventoryItem.GetFromID("29366d4a-da4b-4f36-9440-0e1b887e02a9");
            slots[1] = InventoryItem.GetFromID("71e73607-4bac-4e42-b7d6-5e6f91e92dc4");
            slots[15] = InventoryItem.GetFromID("71e73607-4bac-4e42-b7d6-5e6f91e92dc4");
        }

        /// <summary>
        /// Find a slot that can accomodate the given item.
        /// </summary>
        /// <returns>-1 if no slot is found.</returns>
        private int FindSlot(InventoryItem item)
        {
            return FindEmptySlot();
        }

        /// <summary>
        /// Find an empty slot.
        /// </summary>
        /// <returns>-1 if all slots are full.</returns>
        private int FindEmptySlot()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
