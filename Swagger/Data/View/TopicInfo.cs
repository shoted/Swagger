namespace Swagger.Data.View
{
    public class TopicInfo
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public TopicInfo(string title,string content)
        {
            Title = title;
            Content = content;
        }
    }
}
