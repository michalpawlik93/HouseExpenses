namespace HouseExpenses.Api.Models
{
    public class ExpenseDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Jobs collection
        /// </summary>
        public List<JobDto> Jobs { get; set; }
    }
}
