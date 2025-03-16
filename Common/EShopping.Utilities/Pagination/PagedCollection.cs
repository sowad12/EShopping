
using EShopping.Utilities.Sort;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace EShopping.Utilities.Pagination
{
    public class PagedCollection<T>
    {
        public IList<T> Items { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Offset { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; set; }
        public int Size { get; set; }
      
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string[] Sort { get; set; }
        public PagedCollection(IList<T> items, int totalCount, IPagingOptions options, ISortOptions sortOptions = null)
        {
            Items = items;
            Offset = options?.Offset is null?0: options?.Offset;
            Limit = options?.Limit is null ? 10 : options?.Limit;
            Size = totalCount;  
            Sort = sortOptions?.OrderBy;
        }

        //public T First { get; set; }
        //public T Last { get; set; }
        //public T Next { get; set; }
        //public T Previous { get; set; }
    }
}
