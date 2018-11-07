﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using MasterData.Dto;

namespace MasterData.Storage
{
    /// <summary>
    /// Concern: To handle data in the applications uptime
    /// </summary>
    public class InMemoryStorage : IStorage
    {
        // Simulate table in database
        private static Dictionary<string, CowData> _dataStore = new Dictionary<string, CowData>();

        public void Store(string earTag, string name, DateTime birthday)
        {
            Debug.WriteLine(string.Format("Tag value = {0}", earTag));

            if(_dataStore.ContainsKey(earTag))
                throw new Exception("EarTag already exists");

            // Try implement factory pattern here/
            _dataStore.Add(earTag, new CowData
            {
                Name = name,
                Birthday = birthday,
                EarTag = earTag,
                CreateTimestamp = DateTime.Now///dmmk
            });
        }

        public (string Name, DateTime Birthday) Read(string earTag)
        {
            return (_dataStore[earTag].Name, _dataStore[earTag].Birthday);
        }
    }

    // Simulate table content in database
    public class CowData
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string EarTag { get; set; }
        public DateTime CreateTimestamp { get; set; }
    }
}
