
using Agrobot.Camera.ObjectDetection;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Agrobot.Camera;

///<summary>
/// Extension methods for <see cref="VideoCapture"/>
///</summary>
public static class CameraExtensions
{
    /// <summary>
    /// Set image width
    /// </summary>
    public static void SetImageWidth(this VideoCapture videoCapture, double width)
    {
        videoCapture.Set(CapProp.FrameWidth, width);
    }

    /// <summary>
    /// Set image height
    /// </summary>
    public static void SetImageHeight(this VideoCapture videoCapture, double height)
    {
        videoCapture.Set(CapProp.FrameHeight, height);
    }

    /// <summary>
    /// Set fps
    /// </summary>
    public static void SetFps(this VideoCapture videoCapture, double fps)
    {
        videoCapture.Set(CapProp.Fps, fps);
    }

    /// <summary>
    /// Get fps
    /// </summary>
    public static double GetFps(this VideoCapture videoCapture)
    {
        return videoCapture.Get(CapProp.Fps);
    }

    /// <summary>
    /// Get image width
    /// </summary>
    public static double GetImageWidth(this VideoCapture videoCapture)
    {
        return videoCapture.Get(CapProp.FrameWidth);
    }

    /// <summary>
    /// Get image height
    /// </summary>
    public static double GetImageHeight(this VideoCapture videoCapture)
    {
        return videoCapture.Get(CapProp.FrameHeight);
    }

    /// <summary>
    /// Convert <see cref="Mat"/> to <see cref="byte"/> array
    /// </summary>
    public static byte[] ToByteArray(this Mat mat)
    {
        return mat.ToImage<Bgr, byte>().ToJpegData();
    }

}