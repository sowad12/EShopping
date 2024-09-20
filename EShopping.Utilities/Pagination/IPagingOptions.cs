namespace EShopping.Utilities.Pagination
{
    public interface IPagingOptions
    {
        int? Offset { get; set; }
        int? Limit { get; set; }
    }
}
