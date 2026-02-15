namespace RMDProcessingApp.Services
{
    public class RmdService : IRmdService
    {
        public string DetermineRmdStatus(int age)
        {
            if (age < 73)
                return "Not Eligible";

            if (age >= 73 && age < 75)
                return "Pending Approval";

            return "Approved";
        }
    }
}
