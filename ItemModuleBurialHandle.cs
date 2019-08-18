using BS;

namespace BurialBlade
{
    // This create an item module that can be referenced in the item JSON
    public class ItemModuleBurialHandle : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item);
            item.gameObject.AddComponent<ItemBurialHandle>();
        }
    }
}
