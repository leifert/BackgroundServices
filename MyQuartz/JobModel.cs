namespace MyQuartz
{
    public class JobModel
    {
        public Guid JobId { get; set; }
        public Type JobType { get; set; }
        public string JobName { get; set; }
        public string CronExpression { get; set; }

        public JobModel(Guid Id, Type jobType, string jobName, string cronExpression)
        {
            JobId = Id;
            JobType = jobType;
            JobName = jobName;
            CronExpression = cronExpression;
        }
    }
}
