using Emgu.CV;
using Emgu.CV.Structure;

namespace AgrobotV2.Camera.ObjectDetection;

public class CascadeClasifierObjectDetection : IObjectDetection
{
    public int ObjectCount
    {
        get;
        private set;
    }
    public string FilePath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    private CascadeClassifier cascadeClassifier;

    public CascadeClasifierObjectDetection()
    {
        this.cascadeClassifier = new();
    }

    public CascadeClasifierObjectDetection(string cascadeClassifierPath)
    {
        this.cascadeClassifier = new(cascadeClassifierPath);
    }

    public bool Detect(Mat imageFrame, out Mat resultImage)
    {
        if(string.IsNullOrEmpty(this.FilePath))
        {
            this.cascadeClassifier = new();
        }
        else
        {
            this.cascadeClassifier = new(this.FilePath);
        }

        var detectedObjects = this.cascadeClassifier.DetectMultiScale(imageFrame, 1.1, 10, Size.Empty);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}


