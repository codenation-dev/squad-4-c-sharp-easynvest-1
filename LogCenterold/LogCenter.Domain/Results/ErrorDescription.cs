namespace LogCenter.Domain.Results
{
    public class ErrorDescription
    {
        public string Message { get; set; }
        public string Reason { get; set; }
        public string Domain { get; set; }

        public override string ToString()
        {
            string message = Message;

            if (message.EndsWith("."))
                message = message.Remove(Message.Length - 1, 1);

            return $"{message}: {Reason}";
        }
    }
}
