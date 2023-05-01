using Emgu.CV;

namespace Agrobot.Camera.ObjectDetection;

public class YoloObjectDetecion : IObjectDetection
{


    public int ObjectCount => throw new NotImplementedException();

    public string FilePath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public YoloObjectDetecion()
    {
    }
    
    public bool Detect(Mat imageFrame, out Mat resultImage)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public bool Detect(IInputOutputArray imageFrame)
    {
        throw new NotImplementedException();
    }
}