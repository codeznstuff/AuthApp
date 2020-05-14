namespace Contracts
{
    public class ApplicationClaims
    {
        public string ApplicationName { get; set; }
        public ClaimData[] Claims { get; set; }
    }
}