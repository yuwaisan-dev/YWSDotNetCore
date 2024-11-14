namespace YWSDotNetCore.miniKpayRestApi.Model
{
    public class DepositModel
    {
        public int DepositId { get; set; }
        public string MobileNumber { get; set; }
        public decimal Balance { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
