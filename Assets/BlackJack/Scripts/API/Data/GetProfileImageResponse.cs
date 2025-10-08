public class GetProfileImageResponse : BaseResponse
{
    public bool success { get; set; }
    public string message { get; set; }
    public ImageData data { get; set; }
}