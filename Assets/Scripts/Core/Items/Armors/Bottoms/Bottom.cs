using Core.Inventory;

namespace Core.Items.Armors.Bottoms
{
    public class Bottom : Armor
    {
        public Bottom(Item item, float durability) : base(item, durability)
        {

        }

        public override void Use(PlayerItem item)
        {
            PlayerPanel.instance.PutOn(PartOfBody.Bottom, item);
        }
    }
}