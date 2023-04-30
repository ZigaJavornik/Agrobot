using System.Buffers;
using Emgu.CV;
using Emgu.CV.Structure;

public class FrameReceivedEventArgs
{
    public byte[] FrameBytes  { get; private set; }

    public FrameReceivedEventArgs(byte[] frameBytes)
    {
        this.FrameBytes = frameBytes;
    }
}