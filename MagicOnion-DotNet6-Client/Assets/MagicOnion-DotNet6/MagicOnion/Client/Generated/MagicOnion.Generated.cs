// <auto-generated />
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace MagicOnion
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::MagicOnion;
    using global::MagicOnion.Client;

    public static partial class MagicOnionInitializer
    {
        static bool isRegistered = false;

        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Register()
        {
            if(isRegistered) return;
            isRegistered = true;

            MagicOnionClientRegistry<MagicOnionDotNet6.Shared.IMyFirstService>.Register((x, y, z) => new MagicOnionDotNet6.Shared.MyFirstServiceClient(x, y, z));

            StreamingHubClientRegistry<MagicOnionDotNet6.Shared.IGamingHub, MagicOnionDotNet6.Shared.IGamingHubReceiver>.Register((a, _, b, c, d, e) => new MagicOnionDotNet6.Shared.GamingHubClient(a, b, c, d, e));
        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 612
#pragma warning restore 618
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace MagicOnion.Resolvers
{
    using System;
    using MessagePack;

    public class MagicOnionResolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new MagicOnionResolver();

        MagicOnionResolver()
        {

        }

        public global::MessagePack.Formatters.IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly global::MessagePack.Formatters.IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                var f = MagicOnionResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }

    internal static class MagicOnionResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static MagicOnionResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(4)
            {
                {typeof(global::MagicOnion.DynamicArgumentTuple<global::UnityEngine.Vector2, global::UnityEngine.Vector2>), 0 },
                {typeof(global::MagicOnion.DynamicArgumentTuple<int, int>), 1 },
                {typeof(global::MagicOnion.DynamicArgumentTuple<string, string>), 2 },
                {typeof(global::MagicOnionDotNet6.Shared.Player[]), 3 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key))
            {
                return null;
            }

            switch (key)
            {
                case 0: return new global::MagicOnion.DynamicArgumentTupleFormatter<global::UnityEngine.Vector2, global::UnityEngine.Vector2>(default(global::UnityEngine.Vector2), default(global::UnityEngine.Vector2));
                case 1: return new global::MagicOnion.DynamicArgumentTupleFormatter<int, int>(default(int), default(int));
                case 2: return new global::MagicOnion.DynamicArgumentTupleFormatter<string, string>(default(string), default(string));
                case 3: return new global::MessagePack.Formatters.ArrayFormatter<global::MagicOnionDotNet6.Shared.Player>();
                default: return null;
            }
        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 612
#pragma warning restore 618
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace MagicOnionDotNet6.Shared {
    using System;
    using MagicOnion;
    using MagicOnion.Client;
    using Grpc.Core;
    using MessagePack;

    [Ignore]
    public class MyFirstServiceClient : MagicOnionClientBase<global::MagicOnionDotNet6.Shared.IMyFirstService>, global::MagicOnionDotNet6.Shared.IMyFirstService
    {
        static readonly Method<byte[], byte[]> SumAsyncMethod;
        static readonly Func<RequestContext, ResponseContext> SumAsyncDelegate;

        static MyFirstServiceClient()
        {
            SumAsyncMethod = new Method<byte[], byte[]>(MethodType.Unary, "IMyFirstService", "SumAsync", MagicOnionMarshallers.ThroughMarshaller, MagicOnionMarshallers.ThroughMarshaller);
            SumAsyncDelegate = _SumAsync;
        }

        MyFirstServiceClient()
        {
        }

        public MyFirstServiceClient(CallInvoker callInvoker, MessagePackSerializerOptions serializerOptions, IClientFilter[] filters)
            : base(callInvoker, serializerOptions, filters)
        {
        }

        protected override MagicOnionClientBase<IMyFirstService> Clone()
        {
            var clone = new MyFirstServiceClient();
            clone.host = this.host;
            clone.option = this.option;
            clone.callInvoker = this.callInvoker;
            clone.serializerOptions = this.serializerOptions;
            clone.filters = filters;
            return clone;
        }

        public new IMyFirstService WithHeaders(Metadata headers)
        {
            return base.WithHeaders(headers);
        }

        public new IMyFirstService WithCancellationToken(System.Threading.CancellationToken cancellationToken)
        {
            return base.WithCancellationToken(cancellationToken);
        }

        public new IMyFirstService WithDeadline(System.DateTime deadline)
        {
            return base.WithDeadline(deadline);
        }

        public new IMyFirstService WithHost(string host)
        {
            return base.WithHost(host);
        }

        public new IMyFirstService WithOptions(CallOptions option)
        {
            return base.WithOptions(option);
        }
   
        static ResponseContext _SumAsync(RequestContext __context)
        {
            return CreateResponseContext<DynamicArgumentTuple<int, int>, int>(__context, SumAsyncMethod);
        }

        public global::MagicOnion.UnaryResult<int> SumAsync(int x, int y)
        {
            return InvokeAsync<DynamicArgumentTuple<int, int>, int>("IMyFirstService/SumAsync", new DynamicArgumentTuple<int, int>(x, y), SumAsyncDelegate);
        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 612
#pragma warning restore 618
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace MagicOnionDotNet6.Shared {
    using Grpc.Core;
    using MagicOnion;
    using MagicOnion.Client;
    using MessagePack;
    using System;
    using System.Threading.Tasks;

    [Ignore]
    public class GamingHubClient : StreamingHubClientBase<global::MagicOnionDotNet6.Shared.IGamingHub, global::MagicOnionDotNet6.Shared.IGamingHubReceiver>, global::MagicOnionDotNet6.Shared.IGamingHub
    {
        static readonly Method<byte[], byte[]> method = new Method<byte[], byte[]>(MethodType.DuplexStreaming, "IGamingHub", "Connect", MagicOnionMarshallers.ThroughMarshaller, MagicOnionMarshallers.ThroughMarshaller);

        protected override Method<byte[], byte[]> DuplexStreamingAsyncMethod { get { return method; } }

        readonly global::MagicOnionDotNet6.Shared.IGamingHub __fireAndForgetClient;

        public GamingHubClient(CallInvoker callInvoker, string host, CallOptions option, MessagePackSerializerOptions serializerOptions, IMagicOnionClientLogger logger)
            : base(callInvoker, host, option, serializerOptions, logger)
        {
            this.__fireAndForgetClient = new FireAndForgetClient(this);
        }
        
        public global::MagicOnionDotNet6.Shared.IGamingHub FireAndForget()
        {
            return __fireAndForgetClient;
        }

        protected override void OnBroadcastEvent(int methodId, ArraySegment<byte> data)
        {
            switch (methodId)
            {
                case -1297457280: // OnJoin
                {
                    var result = MessagePackSerializer.Deserialize<global::MagicOnionDotNet6.Shared.Player>(data, serializerOptions);
                    receiver.OnJoin(result); break;
                }
                case 1651921817: // OnRestartGame
                {
                    var result = MessagePackSerializer.Deserialize<string>(data, serializerOptions);
                    receiver.OnRestartGame(result); break;
                }
                case -664130984: // OnMovePlayer
                {
                    var result = MessagePackSerializer.Deserialize<float>(data, serializerOptions);
                    receiver.OnMovePlayer(result); break;
                }
                case 224575324: // OnMoveBall
                {
                    var result = MessagePackSerializer.Deserialize<DynamicArgumentTuple<global::UnityEngine.Vector2, global::UnityEngine.Vector2>>(data, serializerOptions);
                    receiver.OnMoveBall(result.Item1, result.Item2); break;
                }
                case 1049770867: // OnTakeBallOwnership
                {
                    var result = MessagePackSerializer.Deserialize<string>(data, serializerOptions);
                    receiver.OnTakeBallOwnership(result); break;
                }
                case -1048207357: // OnGoal
                {
                    var result = MessagePackSerializer.Deserialize<global::MagicOnionDotNet6.Shared.Player>(data, serializerOptions);
                    receiver.OnGoal(result); break;
                }
                case 532410095: // OnLeave
                {
                    var result = MessagePackSerializer.Deserialize<string>(data, serializerOptions);
                    receiver.OnLeave(result); break;
                }
                default:
                    break;
            }
        }

        protected override void OnResponseEvent(int methodId, object taskCompletionSource, ArraySegment<byte> data)
        {
            switch (methodId)
            {
                case -733403293: // JoinAsync
                {
                    var result = MessagePackSerializer.Deserialize<global::MagicOnionDotNet6.Shared.Player[]>(data, serializerOptions);
                    ((TaskCompletionSource<global::MagicOnionDotNet6.Shared.Player[]>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case 1231733190: // RestartGame
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case -1874578125: // MovePlayerAsync
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case -1709922813: // MoveBallAsync
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case 71373192: // TakeBallOwnership
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case 215317962: // GoalAsync
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case 1368362116: // LeaveAsync
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                default:
                    break;
            }
        }
   
        public global::System.Threading.Tasks.Task<global::MagicOnionDotNet6.Shared.Player[]> JoinAsync(string roomName, string id)
        {
            return WriteMessageWithResponseAsync<DynamicArgumentTuple<string, string>, global::MagicOnionDotNet6.Shared.Player[]> (-733403293, new DynamicArgumentTuple<string, string>(roomName, id));
        }

        public global::System.Threading.Tasks.Task RestartGame()
        {
            return WriteMessageWithResponseAsync<Nil, Nil>(1231733190, Nil.Default);
        }

        public global::System.Threading.Tasks.Task MovePlayerAsync(float position)
        {
            return WriteMessageWithResponseAsync<float, Nil>(-1874578125, position);
        }

        public global::System.Threading.Tasks.Task MoveBallAsync(global::UnityEngine.Vector2 position, global::UnityEngine.Vector2 velocity)
        {
            return WriteMessageWithResponseAsync<DynamicArgumentTuple<global::UnityEngine.Vector2, global::UnityEngine.Vector2>, Nil>(-1709922813, new DynamicArgumentTuple<global::UnityEngine.Vector2, global::UnityEngine.Vector2>(position, velocity));
        }

        public global::System.Threading.Tasks.Task TakeBallOwnership(string id)
        {
            return WriteMessageWithResponseAsync<string, Nil>(71373192, id);
        }

        public global::System.Threading.Tasks.Task GoalAsync(string winnerId)
        {
            return WriteMessageWithResponseAsync<string, Nil>(215317962, winnerId);
        }

        public global::System.Threading.Tasks.Task LeaveAsync()
        {
            return WriteMessageWithResponseAsync<Nil, Nil>(1368362116, Nil.Default);
        }


        class FireAndForgetClient : global::MagicOnionDotNet6.Shared.IGamingHub
        {
            readonly GamingHubClient __parent;

            public FireAndForgetClient(GamingHubClient parentClient)
            {
                this.__parent = parentClient;
            }

            public global::MagicOnionDotNet6.Shared.IGamingHub FireAndForget()
            {
                throw new NotSupportedException();
            }

            public Task DisposeAsync()
            {
                throw new NotSupportedException();
            }

            public Task WaitForDisconnect()
            {
                throw new NotSupportedException();
            }

            public global::System.Threading.Tasks.Task<global::MagicOnionDotNet6.Shared.Player[]> JoinAsync(string roomName, string id)
            {
                return __parent.WriteMessageAsyncFireAndForget<DynamicArgumentTuple<string, string>, global::MagicOnionDotNet6.Shared.Player[]> (-733403293, new DynamicArgumentTuple<string, string>(roomName, id));
            }

            public global::System.Threading.Tasks.Task RestartGame()
            {
                return __parent.WriteMessageAsync<Nil>(1231733190, Nil.Default);
            }

            public global::System.Threading.Tasks.Task MovePlayerAsync(float position)
            {
                return __parent.WriteMessageAsync<float>(-1874578125, position);
            }

            public global::System.Threading.Tasks.Task MoveBallAsync(global::UnityEngine.Vector2 position, global::UnityEngine.Vector2 velocity)
            {
                return __parent.WriteMessageAsync<DynamicArgumentTuple<global::UnityEngine.Vector2, global::UnityEngine.Vector2>>(-1709922813, new DynamicArgumentTuple<global::UnityEngine.Vector2, global::UnityEngine.Vector2>(position, velocity));
            }

            public global::System.Threading.Tasks.Task TakeBallOwnership(string id)
            {
                return __parent.WriteMessageAsync<string>(71373192, id);
            }

            public global::System.Threading.Tasks.Task GoalAsync(string winnerId)
            {
                return __parent.WriteMessageAsync<string>(215317962, winnerId);
            }

            public global::System.Threading.Tasks.Task LeaveAsync()
            {
                return __parent.WriteMessageAsync<Nil>(1368362116, Nil.Default);
            }

        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612