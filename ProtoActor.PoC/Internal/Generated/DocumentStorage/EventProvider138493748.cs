// <auto-generated/>
#pragma warning disable
using Marten.Internal;
using Marten.Internal.Storage;
using Marten.Schema;
using Marten.Schema.Arguments;
using Npgsql;
using ProtoActor.PoC.Persistence.Marten;
using System;
using System.Collections.Generic;
using Weasel.Core;
using Weasel.Postgresql;

namespace Marten.Generated.DocumentStorage
{
    // START: UpsertEventOperation138493748
    public class UpsertEventOperation138493748 : Marten.Internal.Operations.StorageOperation<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>
    {
        private readonly ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> _document;
        private readonly string _id;
        private readonly System.Collections.Generic.Dictionary<string, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public UpsertEventOperation138493748(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, string id, System.Collections.Generic.Dictionary<string, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select public.mt_upsert_eventieventgrainevents(?, ?, ?, ?)";


        public override void Postprocess(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions)
        {
            storeVersion();
        }


        public override System.Threading.Tasks.Task PostprocessAsync(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions, System.Threading.CancellationToken token)
        {
            storeVersion();
            // Nothing
            return System.Threading.Tasks.Task.CompletedTask;
        }


        public override Marten.Internal.Operations.OperationRole Role()
        {
            return Marten.Internal.Operations.OperationRole.Upsert;
        }


        public override string CommandText()
        {
            return COMMAND_TEXT;
        }


        public override NpgsqlTypes.NpgsqlDbType DbType()
        {
            return NpgsqlTypes.NpgsqlDbType.Text;
        }


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Internal.IMartenSession session)
        {
            parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
            parameters[0].Value = session.Serializer.ToJson(_document);
            // .Net Class Type
            parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
            parameters[1].Value = _document.GetType().FullName;
            parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text;

            if (document.Id != null)
            {
                parameters[2].Value = document.Id;
            }

            else
            {
                parameters[2].Value = System.DBNull.Value;
            }

            setVersionParameter(parameters[3]);
        }

    }

    // END: UpsertEventOperation138493748
    
    
    // START: InsertEventOperation138493748
    public class InsertEventOperation138493748 : Marten.Internal.Operations.StorageOperation<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>
    {
        private readonly ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> _document;
        private readonly string _id;
        private readonly System.Collections.Generic.Dictionary<string, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public InsertEventOperation138493748(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, string id, System.Collections.Generic.Dictionary<string, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select public.mt_insert_eventieventgrainevents(?, ?, ?, ?)";


        public override void Postprocess(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions)
        {
            storeVersion();
        }


        public override System.Threading.Tasks.Task PostprocessAsync(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions, System.Threading.CancellationToken token)
        {
            storeVersion();
            // Nothing
            return System.Threading.Tasks.Task.CompletedTask;
        }


        public override Marten.Internal.Operations.OperationRole Role()
        {
            return Marten.Internal.Operations.OperationRole.Insert;
        }


        public override string CommandText()
        {
            return COMMAND_TEXT;
        }


        public override NpgsqlTypes.NpgsqlDbType DbType()
        {
            return NpgsqlTypes.NpgsqlDbType.Text;
        }


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Internal.IMartenSession session)
        {
            parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
            parameters[0].Value = session.Serializer.ToJson(_document);
            // .Net Class Type
            parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
            parameters[1].Value = _document.GetType().FullName;
            parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text;

            if (document.Id != null)
            {
                parameters[2].Value = document.Id;
            }

            else
            {
                parameters[2].Value = System.DBNull.Value;
            }

            setVersionParameter(parameters[3]);
        }

    }

    // END: InsertEventOperation138493748
    
    
    // START: UpdateEventOperation138493748
    public class UpdateEventOperation138493748 : Marten.Internal.Operations.StorageOperation<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>
    {
        private readonly ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> _document;
        private readonly string _id;
        private readonly System.Collections.Generic.Dictionary<string, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public UpdateEventOperation138493748(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, string id, System.Collections.Generic.Dictionary<string, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select public.mt_update_eventieventgrainevents(?, ?, ?, ?)";


        public override void Postprocess(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions)
        {
            storeVersion();
            postprocessUpdate(reader, exceptions);
        }


        public override async System.Threading.Tasks.Task PostprocessAsync(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions, System.Threading.CancellationToken token)
        {
            storeVersion();
            await postprocessUpdateAsync(reader, exceptions, token);
        }


        public override Marten.Internal.Operations.OperationRole Role()
        {
            return Marten.Internal.Operations.OperationRole.Update;
        }


        public override string CommandText()
        {
            return COMMAND_TEXT;
        }


        public override NpgsqlTypes.NpgsqlDbType DbType()
        {
            return NpgsqlTypes.NpgsqlDbType.Text;
        }


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Internal.IMartenSession session)
        {
            parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
            parameters[0].Value = session.Serializer.ToJson(_document);
            // .Net Class Type
            parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
            parameters[1].Value = _document.GetType().FullName;
            parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text;

            if (document.Id != null)
            {
                parameters[2].Value = document.Id;
            }

            else
            {
                parameters[2].Value = System.DBNull.Value;
            }

            setVersionParameter(parameters[3]);
        }

    }

    // END: UpdateEventOperation138493748
    
    
    // START: QueryOnlyEventSelector138493748
    public class QueryOnlyEventSelector138493748 : Marten.Internal.CodeGeneration.DocumentSelectorWithOnlySerializer, Marten.Linq.Selectors.ISelector<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public QueryOnlyEventSelector138493748(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> Resolve(System.Data.Common.DbDataReader reader)
        {

            ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document;
            document = _serializer.FromJson<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>>(reader, 0);
            return document;
        }


        public async System.Threading.Tasks.Task<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {

            ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document;
            document = await _serializer.FromJsonAsync<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>>(reader, 0, token).ConfigureAwait(false);
            return document;
        }

    }

    // END: QueryOnlyEventSelector138493748
    
    
    // START: LightweightEventSelector138493748
    public class LightweightEventSelector138493748 : Marten.Internal.CodeGeneration.DocumentSelectorWithVersions<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>, Marten.Linq.Selectors.ISelector<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public LightweightEventSelector138493748(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<string>(0);

            ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document;
            document = _serializer.FromJson<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            return document;
        }


        public async System.Threading.Tasks.Task<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<string>(0, token);

            ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document;
            document = await _serializer.FromJsonAsync<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            return document;
        }

    }

    // END: LightweightEventSelector138493748
    
    
    // START: IdentityMapEventSelector138493748
    public class IdentityMapEventSelector138493748 : Marten.Internal.CodeGeneration.DocumentSelectorWithIdentityMap<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>, Marten.Linq.Selectors.ISelector<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public IdentityMapEventSelector138493748(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<string>(0);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document;
            document = _serializer.FromJson<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            return document;
        }


        public async System.Threading.Tasks.Task<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<string>(0, token);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document;
            document = await _serializer.FromJsonAsync<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            return document;
        }

    }

    // END: IdentityMapEventSelector138493748
    
    
    // START: DirtyTrackingEventSelector138493748
    public class DirtyTrackingEventSelector138493748 : Marten.Internal.CodeGeneration.DocumentSelectorWithDirtyChecking<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>, Marten.Linq.Selectors.ISelector<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public DirtyTrackingEventSelector138493748(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<string>(0);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document;
            document = _serializer.FromJson<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            StoreTracker(_session, document);
            return document;
        }


        public async System.Threading.Tasks.Task<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<string>(0, token);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document;
            document = await _serializer.FromJsonAsync<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            StoreTracker(_session, document);
            return document;
        }

    }

    // END: DirtyTrackingEventSelector138493748
    
    
    // START: QueryOnlyEventDocumentStorage138493748
    public class QueryOnlyEventDocumentStorage138493748 : Marten.Internal.Storage.QueryOnlyDocumentStorage<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public QueryOnlyEventDocumentStorage138493748(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override string AssignIdentity(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (string.IsNullOrEmpty(document.Id)) throw new InvalidOperationException("Id/id values cannot be null or empty");
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateEventOperation138493748
            (
                document, Identity(document),
                session.Versions.ForType<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertEventOperation138493748
            (
                document, Identity(document),
                session.Versions.ForType<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertEventOperation138493748
            (
                document, Identity(document),
                session.Versions.ForType<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override string Identity(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.QueryOnlyEventSelector138493748(session, _document);
        }


        public override object RawIdentityValue(string id)
        {
            return id;
        }

    }

    // END: QueryOnlyEventDocumentStorage138493748
    
    
    // START: LightweightEventDocumentStorage138493748
    public class LightweightEventDocumentStorage138493748 : Marten.Internal.Storage.LightweightDocumentStorage<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public LightweightEventDocumentStorage138493748(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override string AssignIdentity(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (string.IsNullOrEmpty(document.Id)) throw new InvalidOperationException("Id/id values cannot be null or empty");
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateEventOperation138493748
            (
                document, Identity(document),
                session.Versions.ForType<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertEventOperation138493748
            (
                document, Identity(document),
                session.Versions.ForType<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertEventOperation138493748
            (
                document, Identity(document),
                session.Versions.ForType<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override string Identity(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.LightweightEventSelector138493748(session, _document);
        }


        public override object RawIdentityValue(string id)
        {
            return id;
        }

    }

    // END: LightweightEventDocumentStorage138493748
    
    
    // START: IdentityMapEventDocumentStorage138493748
    public class IdentityMapEventDocumentStorage138493748 : Marten.Internal.Storage.IdentityMapDocumentStorage<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public IdentityMapEventDocumentStorage138493748(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override string AssignIdentity(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (string.IsNullOrEmpty(document.Id)) throw new InvalidOperationException("Id/id values cannot be null or empty");
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateEventOperation138493748
            (
                document, Identity(document),
                session.Versions.ForType<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertEventOperation138493748
            (
                document, Identity(document),
                session.Versions.ForType<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertEventOperation138493748
            (
                document, Identity(document),
                session.Versions.ForType<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override string Identity(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.IdentityMapEventSelector138493748(session, _document);
        }


        public override object RawIdentityValue(string id)
        {
            return id;
        }

    }

    // END: IdentityMapEventDocumentStorage138493748
    
    
    // START: DirtyTrackingEventDocumentStorage138493748
    public class DirtyTrackingEventDocumentStorage138493748 : Marten.Internal.Storage.DirtyCheckedDocumentStorage<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public DirtyTrackingEventDocumentStorage138493748(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override string AssignIdentity(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (string.IsNullOrEmpty(document.Id)) throw new InvalidOperationException("Id/id values cannot be null or empty");
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateEventOperation138493748
            (
                document, Identity(document),
                session.Versions.ForType<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertEventOperation138493748
            (
                document, Identity(document),
                session.Versions.ForType<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertEventOperation138493748
            (
                document, Identity(document),
                session.Versions.ForType<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override string Identity(ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.DirtyTrackingEventSelector138493748(session, _document);
        }


        public override object RawIdentityValue(string id)
        {
            return id;
        }

    }

    // END: DirtyTrackingEventDocumentStorage138493748
    
    
    // START: EventBulkLoader138493748
    public class EventBulkLoader138493748 : Marten.Internal.CodeGeneration.BulkLoader<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string>
    {
        private readonly Marten.Internal.Storage.IDocumentStorage<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string> _storage;

        public EventBulkLoader138493748(Marten.Internal.Storage.IDocumentStorage<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>, string> storage) : base(storage)
        {
            _storage = storage;
        }


        public const string MAIN_LOADER_SQL = "COPY public.mt_doc_eventieventgrainevents(\"mt_dotnet_type\", \"id\", \"mt_version\", \"data\") FROM STDIN BINARY";

        public const string TEMP_LOADER_SQL = "COPY mt_doc_eventieventgrainevents_temp(\"mt_dotnet_type\", \"id\", \"mt_version\", \"data\") FROM STDIN BINARY";

        public const string COPY_NEW_DOCUMENTS_SQL = "insert into public.mt_doc_eventieventgrainevents (\"id\", \"data\", \"mt_version\", \"mt_dotnet_type\", mt_last_modified) (select mt_doc_eventieventgrainevents_temp.\"id\", mt_doc_eventieventgrainevents_temp.\"data\", mt_doc_eventieventgrainevents_temp.\"mt_version\", mt_doc_eventieventgrainevents_temp.\"mt_dotnet_type\", transaction_timestamp() from mt_doc_eventieventgrainevents_temp left join public.mt_doc_eventieventgrainevents on mt_doc_eventieventgrainevents_temp.id = public.mt_doc_eventieventgrainevents.id where public.mt_doc_eventieventgrainevents.id is null)";

        public const string OVERWRITE_SQL = "update public.mt_doc_eventieventgrainevents target SET data = source.data, mt_version = source.mt_version, mt_dotnet_type = source.mt_dotnet_type, mt_last_modified = transaction_timestamp() FROM mt_doc_eventieventgrainevents_temp source WHERE source.id = target.id";

        public const string CREATE_TEMP_TABLE_FOR_COPYING_SQL = "create temporary table mt_doc_eventieventgrainevents_temp as select * from public.mt_doc_eventieventgrainevents limit 0";


        public override string CreateTempTableForCopying()
        {
            return CREATE_TEMP_TABLE_FOR_COPYING_SQL;
        }


        public override string CopyNewDocumentsFromTempTable()
        {
            return COPY_NEW_DOCUMENTS_SQL;
        }


        public override string OverwriteDuplicatesFromTempTable()
        {
            return OVERWRITE_SQL;
        }


        public override void LoadRow(Npgsql.NpgsqlBinaryImporter writer, ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Storage.Tenant tenant, Marten.ISerializer serializer)
        {
            writer.Write(document.GetType().FullName, NpgsqlTypes.NpgsqlDbType.Varchar);
            writer.Write(document.Id, NpgsqlTypes.NpgsqlDbType.Text);
            writer.Write(Marten.Schema.Identity.CombGuidIdGeneration.NewGuid(), NpgsqlTypes.NpgsqlDbType.Uuid);
            writer.Write(serializer.ToJson(document), NpgsqlTypes.NpgsqlDbType.Jsonb);
        }


        public override async System.Threading.Tasks.Task LoadRowAsync(Npgsql.NpgsqlBinaryImporter writer, ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents> document, Marten.Storage.Tenant tenant, Marten.ISerializer serializer, System.Threading.CancellationToken cancellation)
        {
            await writer.WriteAsync(document.GetType().FullName, NpgsqlTypes.NpgsqlDbType.Varchar, cancellation);
            await writer.WriteAsync(document.Id, NpgsqlTypes.NpgsqlDbType.Text, cancellation);
            await writer.WriteAsync(Marten.Schema.Identity.CombGuidIdGeneration.NewGuid(), NpgsqlTypes.NpgsqlDbType.Uuid, cancellation);
            await writer.WriteAsync(serializer.ToJson(document), NpgsqlTypes.NpgsqlDbType.Jsonb, cancellation);
        }


        public override string MainLoaderSql()
        {
            return MAIN_LOADER_SQL;
        }


        public override string TempLoaderSql()
        {
            return TEMP_LOADER_SQL;
        }

    }

    // END: EventBulkLoader138493748
    
    
    // START: EventProvider138493748
    public class EventProvider138493748 : Marten.Internal.Storage.DocumentProvider<ProtoActor.PoC.Persistence.Marten.Event<IEventGrainEvents>>
    {
        private readonly Marten.Schema.DocumentMapping _mapping;

        public EventProvider138493748(Marten.Schema.DocumentMapping mapping) : base(new EventBulkLoader138493748(new QueryOnlyEventDocumentStorage138493748(mapping)), new QueryOnlyEventDocumentStorage138493748(mapping), new LightweightEventDocumentStorage138493748(mapping), new IdentityMapEventDocumentStorage138493748(mapping), new DirtyTrackingEventDocumentStorage138493748(mapping))
        {
            _mapping = mapping;
        }


    }

    // END: EventProvider138493748
    
    
}

