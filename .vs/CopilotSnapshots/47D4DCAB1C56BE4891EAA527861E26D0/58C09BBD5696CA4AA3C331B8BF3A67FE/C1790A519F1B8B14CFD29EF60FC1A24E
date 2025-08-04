using CustomProjectRPG.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using CustomProjectRPG.Interfaces;

namespace CustomProjectRPG
{
    public class InventoryComponent: ISerializeJSON<InventoryComponent>
    {
        private List<Item> _items;
        private int _maxSlots;

        public InventoryComponent(int maxSlots = 5)
        {
            _items = new List<Item>();
            _maxSlots = maxSlots;
        }

        public IReadOnlyList<Item> GetContents()
        {
            return _items.AsReadOnly();
        }

        public bool Add(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (_items.Count >= _maxSlots)
            {
                return false;
            }

            _items.Add(item);
            return true;
        }

        public bool Remove(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return _items.Remove(item);
        }

        public bool HasItem(string itemId)
        {
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new ArgumentException("Item ID cannot be null or empty.");
            }

            foreach (Item item in _items)
            {
                if (item.Id == itemId)
                {
                    return true;
                }
            }

            return false;
        }


        public string Serialize()
        {
            return JsonSerializer.Serialize(_items);
        }

        public void Deserialize(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return;
            var items = JsonSerializer.Deserialize<List<Item>>(json);
            _items = items ?? new List<Item>();
        }

    }

}
