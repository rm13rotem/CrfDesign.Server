namespace BuisnessLogic.Filters
{
    public class CrfOptionFilter : Filter
    {
       // string PartialName , int Page (=1) , int NLines = 10;
        public string PartialCategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
