namespace BuisnessLogic.Filters
{
    public interface IFilter
    {
        string PartialName { get; set; }
        public int Page { get; set; }
        public int NLines { get; set; }
    }
}