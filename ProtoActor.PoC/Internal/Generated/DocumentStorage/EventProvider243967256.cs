// <auto-generated/>
#pragma warning disable
using Marten.Internal;
using Marten.Internal.Storage;
using Marten.Schema;
using Marten.Schema.Arguments;
using Npgsql;
using Proto.Persistence.Marten;
using System;
using System.Collections.Generic;
using Weasel.Core;
using Weasel.Postgresql;

namespace Marten.Generated.DocumentStorage
{
    // START: UpsertEventOperation243967256
    public class UpsertEventOperation243967256 : Marten.Internal.Operations.StorageOperation<Proto.Persistence.Marten.Event, string>
    {
        private readonly Proto.Persistence.Marten.Event _document;
        private readonly string _id;
        private readonly System.Collections.Generic.Dictionary<string, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public UpsertEventOperation243967256(Proto.Persistence.Marten.Event document, string id, System.Collections.Generic.Dictionary<string, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select public.mt_upsert_event(?, ?, ?, ?)";


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


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, Proto.Persistence.Marten.Event document, Marten.Internal.IMartenSession session)
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

    // END: UpsertEventOperation243967256
    
    
    // START: InsertEventOperation243967256
    public class InsertEventOperation243967256 : Marten.Internal.Operations.StorageOperation<Proto.Persistence.Marten.Event, string>
    {
        private readonly Proto.Persistence.Marten.Event _document;
        private readonly string _id;
        private readonly System.Collections.Generic.Dictionary<string, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public InsertEventOperation243967256(Proto.Persistence.Marten.Event document, string id, System.Collections.Generic.Dictionary<string, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select public.mt_insert_event(?, ?, ?, ?)";


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


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, Proto.Persistence.Marten.Event document, Marten.Internal.IMartenSession session)
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

    // END: InsertEventOperation243967256
    
    
    // START: UpdateEventOperation243967256
    public class UpdateEventOperation243967256 : Marten.Internal.Operations.StorageOperation<Proto.Persistence.Marten.Event, string>
    {
        private readonly Proto.Persistence.Marten.Event _document;
        private readonly string _id;
        private readonly System.Collections.Generic.Dictionary<string, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public UpdateEventOperation243967256(Proto.Persistence.Marten.Event document, string id, System.Collections.Generic.Dictionary<string, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }


        public const string COMMAND_TEXT = "select public.mt_update_event(?, ?, ?, ?)";


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


        public override void ConfigureParameters(Npgsql.NpgsqlParameter[] parameters, Proto.Persistence.Marten.Event document, Marten.Internal.IMartenSession session)
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

    // END: UpdateEventOperation243967256
    
    
    // START: QueryOnlyEventSelector243967256
    public class QueryOnlyEventSelector243967256 : Marten.Internal.CodeGeneration.DocumentSelectorWithOnlySerializer, Marten.Linq.Selectors.ISelector<Proto.Persistence.Marten.Event>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public QueryOnlyEventSelector243967256(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public Proto.Persistence.Marten.Event Resolve(System.Data.Common.DbDataReader reader)
        {

            Proto.Persistence.Marten.Event document;
            document = _serializer.FromJson<Proto.Persistence.Marten.Event>(reader, 0);
            return document;
        }


        public async System.Threading.Tasks.Task<Proto.Persistence.Marten.Event> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {

            Proto.Persistence.Marten.Event document;
            document = await _serializer.FromJsonAsync<Proto.Persistence.Marten.Event>(reader, 0, token).ConfigureAwait(false);
            return document;
        }

    }

    // END: QueryOnlyEventSelector243967256
    
    
    // START: LightweightEventSelector243967256
    public class LightweightEventSelector243967256 : Marten.Internal.CodeGeneration.DocumentSelectorWithVersions<Proto.Persistence.Marten.Event, string>, Marten.Linq.Selectors.ISelector<Proto.Persistence.Marten.Event>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public LightweightEventSelector243967256(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public Proto.Persistence.Marten.Event Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<string>(0);

            Proto.Persistence.Marten.Event document;
            document = _serializer.FromJson<Proto.Persistence.Marten.Event>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            return document;
        }


        public async System.Threading.Tasks.Task<Proto.Persistence.Marten.Event> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<string>(0, token);

            Proto.Persistence.Marten.Event document;
            document = await _serializer.FromJsonAsync<Proto.Persistence.Marten.Event>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            return document;
        }

    }

    // END: LightweightEventSelector243967256
    
    
    // START: IdentityMapEventSelector243967256
    public class IdentityMapEventSelector243967256 : Marten.Internal.CodeGeneration.DocumentSelectorWithIdentityMap<Proto.Persistence.Marten.Event, string>, Marten.Linq.Selectors.ISelector<Proto.Persistence.Marten.Event>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public IdentityMapEventSelector243967256(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public Proto.Persistence.Marten.Event Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<string>(0);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            Proto.Persistence.Marten.Event document;
            document = _serializer.FromJson<Proto.Persistence.Marten.Event>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            return document;
        }


        public async System.Threading.Tasks.Task<Proto.Persistence.Marten.Event> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<string>(0, token);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            Proto.Persistence.Marten.Event document;
            document = await _serializer.FromJsonAsync<Proto.Persistence.Marten.Event>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            return document;
        }

    }

    // END: IdentityMapEventSelector243967256
    
    
    // START: DirtyTrackingEventSelector243967256
    public class DirtyTrackingEventSelector243967256 : Marten.Internal.CodeGeneration.DocumentSelectorWithDirtyChecking<Proto.Persistence.Marten.Event, string>, Marten.Linq.Selectors.ISelector<Proto.Persistence.Marten.Event>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public DirtyTrackingEventSelector243967256(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public Proto.Persistence.Marten.Event Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<string>(0);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            Proto.Persistence.Marten.Event document;
            document = _serializer.FromJson<Proto.Persistence.Marten.Event>(reader, 1);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            StoreTracker(_session, document);
            return document;
        }


        public async System.Threading.Tasks.Task<Proto.Persistence.Marten.Event> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<string>(0, token);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            Proto.Persistence.Marten.Event document;
            document = await _serializer.FromJsonAsync<Proto.Persistence.Marten.Event>(reader, 1, token).ConfigureAwait(false);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            StoreTracker(_session, document);
            return document;
        }

    }

    // END: DirtyTrackingEventSelector243967256
    
    
    // START: QueryOnlyEventDocumentStorage243967256
    public class QueryOnlyEventDocumentStorage243967256 : Marten.Internal.Storage.QueryOnlyDocumentStorage<Proto.Persistence.Marten.Event, string>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public QueryOnlyEventDocumentStorage243967256(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override string AssignIdentity(Proto.Persistence.Marten.Event document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(Proto.Persistence.Marten.Event document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateEventOperation243967256
            (
                document, Identity(document),
                session.Versions.ForType<Proto.Persistence.Marten.Event, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(Proto.Persistence.Marten.Event document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertEventOperation243967256
            (
                document, Identity(document),
                session.Versions.ForType<Proto.Persistence.Marten.Event, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(Proto.Persistence.Marten.Event document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertEventOperation243967256
            (
                document, Identity(document),
                session.Versions.ForType<Proto.Persistence.Marten.Event, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(Proto.Persistence.Marten.Event document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override string Identity(Proto.Persistence.Marten.Event document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.QueryOnlyEventSelector243967256(session, _document);
        }


        public override object RawIdentityValue(string id)
        {
            return id;
        }

    }

    // END: QueryOnlyEventDocumentStorage243967256
    
    
    // START: LightweightEventDocumentStorage243967256
    public class LightweightEventDocumentStorage243967256 : Marten.Internal.Storage.LightweightDocumentStorage<Proto.Persistence.Marten.Event, string>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public LightweightEventDocumentStorage243967256(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override string AssignIdentity(Proto.Persistence.Marten.Event document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(Proto.Persistence.Marten.Event document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateEventOperation243967256
            (
                document, Identity(document),
                session.Versions.ForType<Proto.Persistence.Marten.Event, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(Proto.Persistence.Marten.Event document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertEventOperation243967256
            (
                document, Identity(document),
                session.Versions.ForType<Proto.Persistence.Marten.Event, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(Proto.Persistence.Marten.Event document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertEventOperation243967256
            (
                document, Identity(document),
                session.Versions.ForType<Proto.Persistence.Marten.Event, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(Proto.Persistence.Marten.Event document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override string Identity(Proto.Persistence.Marten.Event document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.LightweightEventSelector243967256(session, _document);
        }


        public override object RawIdentityValue(string id)
        {
            return id;
        }

    }

    // END: LightweightEventDocumentStorage243967256
    
    
    // START: IdentityMapEventDocumentStorage243967256
    public class IdentityMapEventDocumentStorage243967256 : Marten.Internal.Storage.IdentityMapDocumentStorage<Proto.Persistence.Marten.Event, string>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public IdentityMapEventDocumentStorage243967256(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override string AssignIdentity(Proto.Persistence.Marten.Event document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(Proto.Persistence.Marten.Event document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateEventOperation243967256
            (
                document, Identity(document),
                session.Versions.ForType<Proto.Persistence.Marten.Event, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(Proto.Persistence.Marten.Event document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertEventOperation243967256
            (
                document, Identity(document),
                session.Versions.ForType<Proto.Persistence.Marten.Event, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(Proto.Persistence.Marten.Event document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertEventOperation243967256
            (
                document, Identity(document),
                session.Versions.ForType<Proto.Persistence.Marten.Event, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(Proto.Persistence.Marten.Event document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override string Identity(Proto.Persistence.Marten.Event document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.IdentityMapEventSelector243967256(session, _document);
        }


        public override object RawIdentityValue(string id)
        {
            return id;
        }

    }

    // END: IdentityMapEventDocumentStorage243967256
    
    
    // START: DirtyTrackingEventDocumentStorage243967256
    public class DirtyTrackingEventDocumentStorage243967256 : Marten.Internal.Storage.DirtyCheckedDocumentStorage<Proto.Persistence.Marten.Event, string>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public DirtyTrackingEventDocumentStorage243967256(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override string AssignIdentity(Proto.Persistence.Marten.Event document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(Proto.Persistence.Marten.Event document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpdateEventOperation243967256
            (
                document, Identity(document),
                session.Versions.ForType<Proto.Persistence.Marten.Event, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Insert(Proto.Persistence.Marten.Event document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertEventOperation243967256
            (
                document, Identity(document),
                session.Versions.ForType<Proto.Persistence.Marten.Event, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(Proto.Persistence.Marten.Event document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.UpsertEventOperation243967256
            (
                document, Identity(document),
                session.Versions.ForType<Proto.Persistence.Marten.Event, string>(),
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(Proto.Persistence.Marten.Event document, Marten.Internal.IMartenSession session, string tenant)
        {
            throw new System.NotSupportedException();
        }


        public override string Identity(Proto.Persistence.Marten.Event document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.DirtyTrackingEventSelector243967256(session, _document);
        }


        public override object RawIdentityValue(string id)
        {
            return id;
        }

    }

    // END: DirtyTrackingEventDocumentStorage243967256
    
    
    // START: EventBulkLoader243967256
    public class EventBulkLoader243967256 : Marten.Internal.CodeGeneration.BulkLoader<Proto.Persistence.Marten.Event, string>
    {
        private readonly Marten.Internal.Storage.IDocumentStorage<Proto.Persistence.Marten.Event, string> _storage;

        public EventBulkLoader243967256(Marten.Internal.Storage.IDocumentStorage<Proto.Persistence.Marten.Event, string> storage) : base(storage)
        {
            _storage = storage;
        }


        public const string MAIN_LOADER_SQL = "COPY public.mt_doc_event(\"mt_dotnet_type\", \"id\", \"mt_version\", \"data\") FROM STDIN BINARY";

        public const string TEMP_LOADER_SQL = "COPY mt_doc_event_temp(\"mt_dotnet_type\", \"id\", \"mt_version\", \"data\") FROM STDIN BINARY";

        public const string COPY_NEW_DOCUMENTS_SQL = "insert into public.mt_doc_event (\"id\", \"data\", \"mt_version\", \"mt_dotnet_type\", mt_last_modified) (select mt_doc_event_temp.\"id\", mt_doc_event_temp.\"data\", mt_doc_event_temp.\"mt_version\", mt_doc_event_temp.\"mt_dotnet_type\", transaction_timestamp() from mt_doc_event_temp left join public.mt_doc_event on mt_doc_event_temp.id = public.mt_doc_event.id where public.mt_doc_event.id is null)";

        public const string OVERWRITE_SQL = "update public.mt_doc_event target SET data = source.data, mt_version = source.mt_version, mt_dotnet_type = source.mt_dotnet_type, mt_last_modified = transaction_timestamp() FROM mt_doc_event_temp source WHERE source.id = target.id";

        public const string CREATE_TEMP_TABLE_FOR_COPYING_SQL = "create temporary table mt_doc_event_temp as select * from public.mt_doc_event limit 0";


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


        public override void LoadRow(Npgsql.NpgsqlBinaryImporter writer, Proto.Persistence.Marten.Event document, Marten.Storage.Tenant tenant, Marten.ISerializer serializer)
        {
            writer.Write(document.GetType().FullName, NpgsqlTypes.NpgsqlDbType.Varchar);
            writer.Write(document.Id, NpgsqlTypes.NpgsqlDbType.Text);
            writer.Write(Marten.Schema.Identity.CombGuidIdGeneration.NewGuid(), NpgsqlTypes.NpgsqlDbType.Uuid);
            writer.Write(serializer.ToJson(document), NpgsqlTypes.NpgsqlDbType.Jsonb);
        }


        public override async System.Threading.Tasks.Task LoadRowAsync(Npgsql.NpgsqlBinaryImporter writer, Proto.Persistence.Marten.Event document, Marten.Storage.Tenant tenant, Marten.ISerializer serializer, System.Threading.CancellationToken cancellation)
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

    // END: EventBulkLoader243967256
    
    
    // START: EventProvider243967256
    public class EventProvider243967256 : Marten.Internal.Storage.DocumentProvider<Proto.Persistence.Marten.Event>
    {
        private readonly Marten.Schema.DocumentMapping _mapping;

        public EventProvider243967256(Marten.Schema.DocumentMapping mapping) : base(new EventBulkLoader243967256(new QueryOnlyEventDocumentStorage243967256(mapping)), new QueryOnlyEventDocumentStorage243967256(mapping), new LightweightEventDocumentStorage243967256(mapping), new IdentityMapEventDocumentStorage243967256(mapping), new DirtyTrackingEventDocumentStorage243967256(mapping))
        {
            _mapping = mapping;
        }


    }

    // END: EventProvider243967256
    
    
}

