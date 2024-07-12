using Marten;
using Proto.Persistence;

namespace ProtoActor.PoC.Persistence.Marten
{
    public class MartenStoreProvider<TEvent, TSnapshot> : IProvider
    {
        private readonly IDocumentStore _store;

        public MartenStoreProvider(IDocumentStore store)
        {
            _store = store;
        }

        public async Task<long> GetEventsAsync(string actorName, long indexStart, long indexEnd, Action<object> callback)
        {
            var session = _store.IdentitySession();
            await using var _ = session.ConfigureAwait(false);

            var events = await session.Query<Event<TEvent>>()
                .Where(x => x.ActorName == actorName)
                .Where(x => x.Index >= indexStart && x.Index <= indexEnd)
                .OrderBy(x => x.Index)
                .ToListAsync().ConfigureAwait(false);

            foreach (var @event in events)
            {
                callback(@event.Data);
            }

            return events switch 
            {
                [.., { Id: not "" and not null } last] => last.Index,
                _ => -1
            };
        }

        public async Task<(object? Snapshot, long Index)> GetSnapshotAsync(string actorName)
        {
            var session = _store.IdentitySession();
            await using var _ = session.ConfigureAwait(false);

            var snapshot = await session.Query<Snapshot<TSnapshot>>()
                .Where(x => x.ActorName == actorName)
                .OrderByDescending(x => x.Index)
                .FirstOrDefaultAsync().ConfigureAwait(false);

            return snapshot is not { Id: "" or null } ? (snapshot.Data, snapshot.Index) : (null, 0);
        }

        public async Task<long> PersistEventAsync(string actorName, long index, object @event)
        {
            var session = _store.IdentitySession();

            session.Store(new Event<TEvent> { ActorName = actorName, Data = (TEvent)@event, Id = $"{actorName}-event-{index}", Index = index });

            await session.SaveChangesAsync().ConfigureAwait(false);

            return index++;
        }

        public async Task PersistSnapshotAsync(string actorName, long index, object snapshot)
        {
            var session = _store.IdentitySession();

            session.Store(new Snapshot<TSnapshot> { ActorName = actorName, Data = (TSnapshot)snapshot, Id = $"{actorName}-snapshot-{index}", Index = index });

            await session.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteEventsAsync(string actorName, long inclusiveToIndex)
        {
            var session = _store.IdentitySession();

            session.DeleteWhere<Event<TEvent>>(x =>
                x.ActorName == actorName &&
                x.Index <= inclusiveToIndex
            );

            await session.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteSnapshotsAsync(string actorName, long inclusiveToIndex)
        {
            var session = _store.IdentitySession();

            session.DeleteWhere<Snapshot<TSnapshot>>(x =>
                x.ActorName == actorName &&
                x.Index <= inclusiveToIndex
            );

            await session.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
