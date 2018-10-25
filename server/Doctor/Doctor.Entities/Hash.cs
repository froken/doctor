namespace Doctor.Entities
{
    public class PasswordHash
    {
        public byte[] Password { get; set; }

        public byte[] Salt { get; set; }
    }
}
