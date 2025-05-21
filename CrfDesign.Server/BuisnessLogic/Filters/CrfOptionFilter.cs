namespace BuisnessLogic.Filters
{
    public class CrfOptionFilter : IFilter
    {
        public string PartialName { get; set; }
        public string PartialCategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
