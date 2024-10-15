namespace Sprint4.Configuration
{
    public class APPConfiguration
    {
        public SwaggerInfo Swagger { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
    }
    public class ConnectionStrings
    {
        public string RM99948 { get; set; }
    }
    public class SwaggerInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
