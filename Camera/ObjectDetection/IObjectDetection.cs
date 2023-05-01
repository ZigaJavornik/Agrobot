using System.Collections;
using Emgu.CV;


namespace Agrobot.Camera.ObjectDetection;
public interface IObjectDetection : IDisposable
{
    /// <summary>
    /// Gets number of detected objects in the image
    /// </summary>
    public int ObjectCount {get;}

    /// <summary>
    /// Gets or sets the file path of the algorithm .xml file
    /// </summary>
    public string FilePath {get; set;}

    /// <summary>
    /// Detects the image
    /// </summary>
    /// <param name="imageFrame"> Frame that should be checked for image detection </param>
    public bool Detect(IInputOutputArray imageFrame);

}