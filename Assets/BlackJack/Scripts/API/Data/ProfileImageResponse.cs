public class ProfileImageResponse : BaseResponse
{
    public ImageData data { get; set; }
}

public class ImageData
{
    public string profileImage { get; set; }
}