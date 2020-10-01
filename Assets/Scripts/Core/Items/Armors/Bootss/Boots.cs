using Core.Inventory;

namespace Core.Items.Armors.Bootss
{
    public class Boots : Armor
    {
        public Boots(Item item, float durability) : base(item, durability)
        {

        }

        public override void Use(PlayerItem item)
        {
            PlayerPanel.instance.PutOn(PartOfBody.Boots, item);
        }
    }
}