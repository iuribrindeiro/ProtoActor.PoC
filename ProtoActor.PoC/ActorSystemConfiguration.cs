using Dumpify;
using Marten;
using Proto;
using Proto.Cluster;
using Proto.Cluster.Consul;
using Proto.Cluster.PartitionActivator;
using Proto.Context;
using Proto.DependencyInjection;
using Proto.Mailbox;
using Proto.Persistence.Marten;
using Proto.Remote;
using Proto.Remote.GrpcNet;
using ProtoActor.PoC.Persistence.Marten;
using ProtoCluster;

public static class ActorSystemConfiguration
{
    public static void AddActorSystem(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddSingleton(provider =>
        {
            // actor system configuration
            var actorSystemConfig = new ActorSystemConfig().WithMetrics();

            // remote configuration
            var remoteConfig = GrpcNetRemoteConfig
                .BindToLocalhost()
                .WithProtoMessages(GrainsReflection.Descriptor);

            // Persistence configuration
            var psqlProvider =
                new MartenStoreProvider<IEventGrainEvents, Event>(provider.GetRequiredService<IDocumentStore>());

            // cluster configuration
            var clusterConfig = ClusterConfig
                .Setup(
                    "EventsCluster",
                    new ConsulProvider(new ConsulProviderConfig(),
                        c => c.Address = new Uri("http://localhost:8500")),
                    new PartitionActivatorLookup())
                .WithClusterKind(
                    new ClusterKind(
                        EventGrainActor.Kind,
                        Props.FromProducer(() =>
                            new EventGrainActor(
                                (context, clusterIdentity)
                                    => new EventGrain(psqlProvider, context, clusterIdentity,
                                        provider.GetRequiredService<ILogger<EventGrain>>()))
                        )
                    )
                );

            // create the actor system
            return new ActorSystem(actorSystemConfig)
                .WithServiceProvider(provider)
                .WithRemote(remoteConfig)
                .WithCluster(clusterConfig);
        }).AddSingleton(p => p.GetRequiredService<ActorSystem>().Cluster());
    }
}