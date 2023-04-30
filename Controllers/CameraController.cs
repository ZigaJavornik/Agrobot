using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;
using System.Text;
using AgrobotV2.Camera;

[ApiController]
[Route("[controller]")]
public class CameraController : ControllerBase
{
    #region Fields
    private readonly ILogger<CameraController> _logger;
    private readonly CameraService _camera;


    #endregion
    public CameraController(ILogger<CameraController> logger, CameraService camera)
    {
        _logger = logger;
        _camera = camera;
    }

    [HttpGet(Name = "GetVideo")]
    public void Get()
    {

        var bufferingFeature = HttpContext
            .Response
            .HttpContext
            .Features
            .Get<IHttpResponseBodyFeature>();

        bufferingFeature?.DisableBuffering();

        HttpContext.Response.StatusCode = 200;
        HttpContext.Response.ContentType = "multipart/x-mixed-replace; boundary=--frame";
        HttpContext.Response.Headers.Add("Connection", "Keep-Alive");
        HttpContext.Response.Headers.Add("CacheControl", "no-cache");
        _camera.FrameReceived += WriteFrame;

        try
        {
            _logger.LogInformation($"Start streaming video");
            _camera.Start();

            while (!HttpContext.RequestAborted.IsCancellationRequested) { }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception in streaming: {ex}");
        }
        finally
        {
            HttpContext.Response.Body.Close();
            _logger.LogInformation("Stop streaming video");
        }

        _camera.FrameReceived -= WriteFrame;
        _camera.Stop();
    }

    private async void WriteFrame(object? sender, FrameReceivedEventArgs? e)
    {
        try
        {
            if(e == null || e.FrameBytes == null)
            {
                return;
            }
            await HttpContext.Response.BodyWriter.WriteAsync(CreateHeader(e.FrameBytes.Length));
            await HttpContext.Response.BodyWriter.WriteAsync(
                e.FrameBytes.AsMemory()
                            .Slice(0, e.FrameBytes.Length)
            );
            await HttpContext.Response.BodyWriter.WriteAsync(CreateFooter());
        }
        catch (ObjectDisposedException)
        {
            // ignore this as its thrown when the stream is stopped
        }

        // vrže exception, idk... 
        //ArrayPool<byte>.Shared.Return(e.FrameBytes);
    }



    private byte[] CreateHeader(int length)
    {
        string header =
            $"--frame\r\nContent-Type:image/jpeg\r\nContent-Length:{length}\r\n\r\n";
        return Encoding.ASCII.GetBytes(header);
    }

    private byte[] CreateFooter()
    {
        return Encoding.ASCII.GetBytes("\r\n");
    }
}