using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    internal class Journal
    {
        public List<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();

        public void OnColectionEvent(object source, CollectionHandlerEventArgs args)
        {
            if (source is MyNewCollection)
            {
                JournalEntries.Add(new JournalEntry
                { 
                    CollectionName = args.CollectionName,
                    NameOfChange = args.NameOfChange,
                    ObjectInfo = args?.ChangedObject?.ToString()
                });
            }
        }
        public override string ToString()
        {
            string result = "";
            if (JournalEntries != null)
            {
                foreach (var entry in JournalEntries)
                {
                    result += entry.ToString() + '\n';
                }
            }
            return result;
        }
    }

    internal class JournalEntry
    {
        public string? CollectionName { get; set; }
        public string? NameOfChange { get; set; }
        public string? ObjectInfo { get; set; }

        public override string ToString()
        {
            return CollectionName + " " + NameOfChange + " " + ObjectInfo;
        }
    }
}
