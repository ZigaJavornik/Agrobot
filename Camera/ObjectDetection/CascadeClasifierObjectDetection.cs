using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Agrobot.Camera.ObjectDetection;

public class CascadeClasifierObjectDetection : IObjectDetection
{
    public int ObjectCount
    {
        get;
        private set;
    }
    public string FilePath 
    { 
        get;
        set;
    } = string.Empty;

    private CascadeClassifier cascadeClassifier;

    public CascadeClasifierObjectDetection()
    {
        this.cascadeClassifier = new();
    }

    public CascadeClasifierObjectDetection(string cascadeClassifierPath)
    {
        this.cascadeClassifier = new(cascadeClassifierPath);
    }

    public bool Detect(IInputOutputArray imageFrame)
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

        if(detectedObjects.Length == 0 || 
           detectedObjects == null)
        {
            return false;
        }

        // Objects are deteced. Draw the rectangles.
        foreach(var obj in detectedObjects)
        {
            CvInvoke.Rectangle(imageFrame, obj, new Bgr(Color.LightGreen).MCvScalar, 2);
        }

        return true;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}


