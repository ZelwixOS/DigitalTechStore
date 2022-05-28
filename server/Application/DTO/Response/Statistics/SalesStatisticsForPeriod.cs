namespace Application.DTO.Response.Statistics
{
    public class SalesStatisticsForPeriod
    {
        public double Finished { get; set; }

        public double NotCompleted { get; set; }

        public double Refused { get; set; }

        public double Canceled { get; set; }
    }
}
