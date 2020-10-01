using Core.Inventory;

namespace Core.Items.Armors.Glovess
{
    public class Gloves : Armor
    {
        public Gloves(Item item, float durability) : base(item, durability)
        {

        }

        public override void Use(PlayerItem item)
        {
            PlayerPanel.instance.PutOn(PartOfBody.Gloves, item);
        }
    }
}