using System.Drawing;
using Agrobot.Camera.ObjectDetection;
public interface ICameraService
{
    /// <summary>
    /// Gets or sets the name of the instance.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the width of the image.
    /// </summary>
    /// <value>
    /// The width of the image.
    /// </value>
    public double ImageWidth { get; set; }

    /// <summary>
    /// Gets or sets the height of the image.
    /// </summary>
    /// <value>
    /// The height of the image.
    /// </value>
    public double ImageHeight { get; set; }

    /// <summary>
    /// Gets or sets the FPS.
    /// </summary>
    /// <value>
    /// The FPS.
    /// </value>
    public double Fps { get; set; }

    /// <summary>
    /// Gets a value indicating whether this instance is opened.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is opened; otherwise, <c>false</c>.
    /// </value>
    public bool IsOpened { get; }

    /// <summary>
    /// Gets or sets the object detection mode.
    /// </summary>
    /// <value>
    /// The object detection mode.
    /// </value>
    public ObjectDetectionMode ObjectDetectionMode { get; set; }

    /// <summary>
    /// Occurs when frame received.
    /// </summary>
    public event EventHandler<FrameReceivedEventArgs>? FrameReceived;

    /// <summary>
    /// Disposes of the resources
    /// </summary>
    public void Dispose();

    /// <summary>
    /// Grabs the frame.
    /// </summary>
    public void GrabFrame();

    /// <summary>
    /// Reads the frame.
    /// </summary>
    public void Read();

    /// <summary>
    /// Retrieves the frame.
    /// </summary>
    public void Retrieve();

    /// <summary>
    /// Starts the video stream.
    /// </summary>
    public void Start();

    /// <summary>
    /// Stops the video stream.
    /// </summary>
    public void Stop();
}