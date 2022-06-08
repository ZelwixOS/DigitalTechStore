namespace Application.DTO.Response
{
    using System.Collections.Generic;

    public class WorkersSales
    {
        public WorkersSales()
        {
            Names = new List<string>();
            Data = new List<double>();
        }

        public List<string> Names { get; set; }

        public List<double> Data { get; set; }
    }
}
