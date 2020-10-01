using Core.Inventory;

namespace Core.Items.Armors.Heads
{
    public class Head : Armor
    {
        public Head(Item item, float durability) : base(item, durability)
        {

        }
        public override void Use(PlayerItem item)
        {
            PlayerPanel.instance.PutOn(PartOfBody.Head, item);
        }
    }
}