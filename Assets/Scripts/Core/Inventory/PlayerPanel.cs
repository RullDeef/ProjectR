using UnityEngine;
using System.Collections.Generic;

namespace Core.Inventory
{
    public class PlayerPanel : MonoBehaviour
    {
        public delegate void OnPutOnItem(PartOfBody part, PlayerItem item);
        public OnPutOnItem OnPutOnItemCallback;

        public delegate void OnTakeOffItem(PartOfBody part);
        public OnTakeOffItem OnTakeOffItemCallback;

        static Dictionary<PartOfBody, PlayerItem> body;
        public static PlayerPanel instance;

        void Awake()
        {
            instance = this;
            body = new Dictionary<PartOfBody, PlayerItem>()
            {
                {PartOfBody.Head, null},
                {PartOfBody.Top, null},
                {PartOfBody.Bottom, null},
                {PartOfBody.Gloves, null},
                {PartOfBody.Boots, null},
                {PartOfBody.LeftHand, null},
                {PartOfBody.RightHand, null}
            };
        }

        // Одеть на игрока шмотку
        public void PutOn(PartOfBody part, PlayerItem item)
        {
            if (body[part] != null) // Если уже лежал итем, вернуть в инвентарь
            {
                PlayerInventory.instance.AddItem(body[part]);
                if (body[part].Equals(item)) // Лежал тот же итем, одевать снова не нужно
                {
                    body[part] = null;

                    if (OnTakeOffItemCallback != null)
                        OnTakeOffItemCallback(part);

                    return;
                }
            }

            body[part] = item; // Одеть в нужную часть тела
            PlayerInventory.instance.DeleteItem(item); // Удалить одетый итем из инветаря

            if (OnPutOnItemCallback != null)
                OnPutOnItemCallback(part, item);
        }
    }

    public enum PartOfBody
    {
        Head, Top, Bottom, Gloves, Boots,
        RightHand, // Weapon?
        LeftHand // Anything       
    }
}
