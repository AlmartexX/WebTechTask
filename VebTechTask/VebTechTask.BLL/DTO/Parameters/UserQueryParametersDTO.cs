namespace VebTechTask.BLL.DTO.Parameters
{
    public class UserQueryParametersDTO
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortField { get; set; } = "Id";
        public bool Ascending { get; set; } = true;
        public string Filter { get; set; } = "";
    }
}