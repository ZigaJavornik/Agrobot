using System.Text.Json.Serialization;

namespace Agrobot.Camera.ObjectDetection;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ObjectDetectionMode
{
    /// <summary>
    /// No object detection processing
    /// </summary>
    None,

    /// <summary>
    /// Haar cascade classifier
    /// </summary>
    CascadeClassifier,

    /// <summary>
    /// You Only Look Once (YOLO)
    /// </summary>
    YOLO
}