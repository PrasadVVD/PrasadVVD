using Akavache;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ArtosApp.Helpers
{
    public static class LocalStorage
    {
        private static readonly string _testguid = "FCDCA567-1FAE-45FE-B764-79BD34A5F1DB";
        private static IBlobCache _db;
        private static IBlobCache _dbLocalMachine;

        public static void Init()
        {
            BlobCache.ApplicationName = "ArtosApp" + _testguid;

            _db = BlobCache.Secure;
            _dbLocalMachine = BlobCache.LocalMachine;

            BlobCache.EnsureInitialized();
        }

        public static void Shutdown()
        {
            BlobCache.Shutdown().Wait();
        }

        public static void ClearLocalMachineStorage()
        {
            _dbLocalMachine.InvalidateAll();
        }

        // currently used to save last sync change Id
        public static void SaveByKeyToCache<T>(string key, T data)
        {
            if (data != null && !string.IsNullOrEmpty(key))
            {
                try
                {
                    _dbLocalMachine.InsertObject(key, data);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error in SaveByKeyToCache: " + ex);
                }
            }
        }

        // currently used to retrieve last sync change Id
        public static async Task<T> GetByKeyFromCacheAsync<T>(string key)
        {
            try
            {
                T value = await _dbLocalMachine.GetObject<T>(key);
                return value;
            }
            catch (KeyNotFoundException)
            {
                return default(T);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in GetByKeyFromCacheAsync: " + ex);
                return default(T);
            }
        }

        // currently used to remove last sync change Id
        public static IObservable<Unit> DeleteByKeyFromCache(string key)
        {
            // N.B. According to documentation this should NOT be used for items inserted with InsertObject
            // TODO: This is being used for items inserted with InsertObject
            return _db.Invalidate(key);
        }
    }
}
