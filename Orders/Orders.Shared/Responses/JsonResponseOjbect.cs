namespace Orders.Shared.Responses
{
    public class JsonResponseOjbect<JsonOjbect>
    {
        public int code { get; set; }
        public bool ItsSuccessful { get; set; }
        public string? Message { get; set; }
        public JsonOjbect? ResultModel { get; set; }
    }
}
