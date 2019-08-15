namespace Sitecore.Support.Data.Fields
{
    using Sitecore.Data;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Links;

    public class ReferenceField : Sitecore.Data.Fields.ReferenceField
    {
        public ReferenceField(Field innerField) : base(innerField)
        {
            Assert.ArgumentNotNull(innerField, "innerField");
        }

        public override void Relink(ItemLink itemLink, Item newLink)
        {
            Assert.ArgumentNotNull(itemLink, "itemLink");
            Assert.ArgumentNotNull(newLink, "newLink");

            // added check whether field point to deleted item. If no => skip relink
            if (this.Path == itemLink.TargetItemID.ToString())
            {
                this.Path = newLink.ID.ToString();
            }
        }

        public override void RemoveLink(ItemLink itemLink)
        {
            Assert.ArgumentNotNull(itemLink, "itemLink");
            ID tempTargetID = (base.TargetID == ID.Null && ID.IsID(base.Path)) ? new ID(base.Path) : base.TargetID;
            if (itemLink.TargetItemID == tempTargetID)
            {
                base.Clear();
            }
        }
    }
}