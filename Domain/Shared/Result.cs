namespace Domain.Shared
{
    public class Result
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public object? Data { get; set; }

        public Result(string error)
        {
            this.Success = false;
            this.Errors = new List<string>
            {
                error
            };
        }

        public Result(object data)
        {
            this.Success = true;
            this.Data = data;
        }

    }

}
