using Core.Inventory;

namespace Core.Items.Armors.Shields
{
    public class Shield : Armor
    {
        public Shield(Item item, float durability) : base(item, durability)
        {

        }

        public override void Use(PlayerItem item)
        {
            PlayerPanel.instance.PutOn(PartOfBody.LeftHand, item);
        }
    }
}