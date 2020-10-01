using Core.Inventory;

namespace Core.Items.Armors.Tops
{
    public class Top : Armor
    {
        public Top(Item item, float durability) : base(item, durability)
        {

        }

        public override void Use(PlayerItem item)
        {
            PlayerPanel.instance.PutOn(PartOfBody.Top, item);
        }
    }
}