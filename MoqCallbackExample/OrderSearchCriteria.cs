namespace MoqCallbackExample
{
    public class OrderSearchCriteria
    {
        public int OrderId { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as OrderSearchCriteria;

            if (item == null)
            {
                return false;
            }

            return this.OrderId.Equals(item.OrderId);
        }

        public override int GetHashCode()
        {
            return OrderId.GetHashCode();
        }
    }
}