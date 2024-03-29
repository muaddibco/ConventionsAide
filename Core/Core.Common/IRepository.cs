﻿using System.Collections.Generic;

namespace ConventionsAide.Core.Common
{
    public interface IRepository<T>
    {
        T GetInstance();

        T1 GetInstance<T1>() where T1 : T;
    }

    public interface IBulkRepository<T>
    {
        IEnumerable<T> GetInstances();
    }

    public interface IRepository<T, TKey>
    {
        IEnumerable<string> GetAvailableKeys();

        T GetInstance(TKey key);
    }

    public interface IRepository<T, TKey1, TKey2>
    {
        T GetInstance(TKey1 key1, TKey2 key2);
    }
}
