using System;
using System.Collections.Generic;
using System.Text;
using LZ4;
using MessagePack;

namespace Retro.Net.Api.RealTime.Models
{
    public interface IWebSocketMessage
    {
        IEnumerable<ArraySegment<byte>> Serialize();
    }

    public abstract class LazyWebSocketMessage : IWebSocketMessage
    {
        private readonly byte[] _header;
        private readonly Lazy<ArraySegment<byte>> _message;

        protected LazyWebSocketMessage(byte[] header, Func<ArraySegment<byte>> f)
        {
            _header = header;
            _message = new Lazy<ArraySegment<byte>>(f);
        }

        public IEnumerable<ArraySegment<byte>> Serialize()
        {
            yield return _header;
            yield return _message.Value;
        }
    }

    public class MessagePackWebSocketMessage<TMessage> : LazyWebSocketMessage
    {
        public MessagePackWebSocketMessage(byte[] header, TMessage message) : base(header, () => MessagePackSerializer.Serialize(message))
        {
        }
    }

    public class CompressedBinaryWebSocketMessage : LazyWebSocketMessage
    {
        public CompressedBinaryWebSocketMessage(byte[] header, byte[] bytes, byte[] buffer) : base(header, () => Compress(bytes, buffer))
        {
        }

        private static ArraySegment<byte> Compress(byte[] bytes, byte[] buffer)
        {
            var lz4Buffer = buffer ?? new byte[LZ4Codec.MaximumOutputLength(bytes.Length)];
            var length = LZ4Codec.Encode(bytes, 0, bytes.Length, lz4Buffer, 0, lz4Buffer.Length);
            return lz4Buffer.Segment(length);
        }
    }

    internal static class WebSocketMessageFactory
    {
        private static readonly byte[] FrameHeader = Encoding.UTF8.GetBytes("GPU");
        private static readonly byte[] MetricsHeader = Encoding.UTF8.GetBytes("MTC");
        private static readonly byte[] EventHeader = Encoding.UTF8.GetBytes("MSG");
        private static readonly byte[] StateUpdateHeader = Encoding.UTF8.GetBytes("STU");
        private static readonly byte[] ErrorHeader = Encoding.UTF8.GetBytes("ERR");

        public static IWebSocketMessage GpuFrame(byte[] frame, byte[] buffer = null) => new CompressedBinaryWebSocketMessage(FrameHeader, frame, buffer);

        public static IWebSocketMessage Metrics(GameBoyMetrics metrics) => new MessagePackWebSocketMessage<GameBoyMetrics>(MetricsHeader, metrics);

        public static IWebSocketMessage ClientMessage(GameBoyClientMessage message) => new MessagePackWebSocketMessage<GameBoyClientMessage>(EventHeader, message);

        public static IWebSocketMessage Error(ErrorMessage error) => new MessagePackWebSocketMessage<ErrorMessage>(ErrorHeader, error);
        
        public static IWebSocketMessage StateUpdate(GameBoySocketClientState state) => new MessagePackWebSocketMessage<GameBoySocketClientState>(StateUpdateHeader, state);
    }
}
