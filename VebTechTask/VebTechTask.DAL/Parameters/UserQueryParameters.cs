
namespace VebTechTask.DAL.Parameters
{
    public class UserQueryParameters
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortField { get; set; } = "Id";
        public bool Ascending { get; set; } = true;
        public string Filter { get; set; } = "";
    }
}
