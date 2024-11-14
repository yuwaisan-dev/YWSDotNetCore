namespace YWSDotNetCore.miniKpayRestApi.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public decimal Balance { get; set; }
        public string PinNumber { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
