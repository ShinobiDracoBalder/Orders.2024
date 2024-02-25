namespace Orders.Shared.Responses
{
    public class JsonResponse
    {
        public int code { get; set; }
        public bool ItsSuccessful { get; set; }
        public string? Message { get; set; }
        public object? ResultModel { get; set; } = null;
    }
}
