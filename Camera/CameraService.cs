using System.Drawing;
using Agrobot.Camera.ObjectDetection;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Agrobot.Camera;



public class CameraService : ICameraService, IDisposable
{
    #region Fields
    private readonly ILogger logger;
    private VideoCapture videoCapture;
    private Mat imageFrame;
    private CameraExceptionHandler exceptionHandler;
    private IObjectDetection? objectDetection;
    private ObjectDetectionMode objectDetectionMode;

    #endregion


    #region Properties
    public string Name
    {
        get;
        set;
    } = string.Empty;

    public double ImageWidth
    {
        get
        {
            return this.videoCapture == null ? 0 : videoCapture.GetImageWidth();
        }
        set
        {
            this.videoCapture?.SetImageWidth(value);
        }
    }
    public double ImageHeight
    {
        get
        {
            return this.videoCapture == null ? 0 : videoCapture.GetImageHeight();
        }
        set
        {
            this.videoCapture?.SetImageHeight(value);
        }
    }
    public double Fps
    {
        get
        {
            return this.videoCapture == null ? 0 : videoCapture.GetFps();
        }
        set
        {
            this.videoCapture?.SetFps(value);
        }
    }
    public bool IsOpened
    {
        get
        {
            return this.videoCapture == null ? false : videoCapture.IsOpened;
        }
    }

    public ObjectDetectionMode ObjectDetectionMode 
    { 
        get
        {
            return this.objectDetectionMode;
        }
        set
        {
            ChangeObjectDetectionMode(value);
            objectDetectionMode = value;
        }
    }

    #endregion

    #region Events
    public event EventHandler<FrameReceivedEventArgs>? FrameReceived;

    #endregion


    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="CameraService"/> class.
    /// </summary>
    /// <param name="cameraIndex">The index of the camera to use.</param>
    /// <param name="api">The API to use.</param>
    /// <param name="width">The width of the image.</param>
    /// <param name="height">The height of the image.</param>
    /// <param name="fps">The fps of the image.</param>
    /// <param name="logger">The logger to use.</param>
    public CameraService(int cameraIndex, VideoCapture.API api, int width, int height, int fps, ILogger logger)
    {
        this.videoCapture = new(cameraIndex, api);

        // Set some basic properties
        this.videoCapture.SetImageWidth(width);
        this.videoCapture.SetImageHeight(height);
        this.videoCapture.SetFps(fps);
        this.videoCapture.ExceptionMode = true;
        this.videoCapture.ImageGrabbed += ImageGrabbed;
        this.imageFrame = new Mat();
        this.logger = logger;
        this.exceptionHandler = new CameraExceptionHandler();
        // No object detection by default
        ChangeObjectDetectionMode(ObjectDetectionMode.None);
    }
    #endregion

    #region Methods - public
    public void Start()
    {
        try
        {
            if (this.videoCapture != null &&
               this.videoCapture.IsOpened)
            {
                logger.LogInformation($"Starting camera {this}...");
                this.videoCapture.Start(exceptionHandler);
            }
            else
            {
                logger.LogError($"Camera not opened yet...!");
                return;
            }
        }
        catch (System.Exception e)
        {

            logger.LogError($"Camera could not be started!/n {e} /n {e.StackTrace}");
        }

        logger.LogInformation("Camera started");
    }

    public void Stop()
    {
        this.videoCapture?.Stop();
        logger.LogInformation("Camera stopped");
    }

    public void Read()
    {
        this.videoCapture?.Read(this.imageFrame);
    }

    public void Retrieve()
    {
        this.videoCapture?.Retrieve(this.imageFrame);
    }

    public void GrabFrame()
    {
        this.videoCapture?.Grab();
    }

    public void Dispose()
    {
        if (this.videoCapture != null)
        {
            this.videoCapture.ImageGrabbed -= ImageGrabbed;
            this.videoCapture.Dispose();
        }
        this.imageFrame.Dispose();
    }
    #endregion


    #region Methods - private

    private void ChangeObjectDetectionMode(ObjectDetectionMode mode)
    {
        if (mode == ObjectDetectionMode.None)
        {
            this.objectDetection = null;
        }
        else if (mode == ObjectDetectionMode.CascadeClassifier)
        {
            this.objectDetection = new CascadeClasifierObjectDetection();
        }
        else if (mode == ObjectDetectionMode.YOLO)
        {
            this.objectDetection = new YoloObjectDetecion();
        }
        else
        {
            throw new System.Exception($"Object detection mode {mode} not supported!");
        }
    }

    private void ImageGrabbed(object? sender, EventArgs e)
    {
        // video capture can't be null, since the event is invoked by the VideoCapture object
        this.videoCapture!.Retrieve(this.imageFrame);
        if (this.objectDetection != null)
        {
            this.objectDetection.Detect(this.imageFrame);
        }

        // Invoke the new frame received event
        this.FrameReceived?.Invoke(this, new FrameReceivedEventArgs(this.imageFrame.ToByteArray()));
    }
    #endregion
}

