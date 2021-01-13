using System;
using System.Collections.Generic;
using System.Text;

namespace AzureReveneraRestCalls
{
    public class ReqDevice
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class SoldToAcctId
        {
            public string searchType { get; set; }
            public string value { get; set; }
        }

        public class QueryParams
        {
            public SoldToAcctId soldToAcctId { get; set; }
            public bool hosted { get; set; }
        }

        public class ResponseConfig
        {
            public bool addOnActivationId { get; set; }
            public bool addOnCounts { get; set; }
            public bool addOnEntitlementId { get; set; }
            public bool addOnLicense { get; set; }
            public bool addOnPartNumber { get; set; }
            public bool addOnProduct { get; set; }
            public bool addOnProductLine { get; set; }
            public bool bufferLicense { get; set; }
            public bool description { get; set; }
            public bool hasLicense { get; set; }
            public bool hasUpdates { get; set; }
            public bool hostTypeName { get; set; }
            public bool hosted { get; set; }
            public bool lastRequestTime { get; set; }
            public bool lastSyncTime { get; set; }
            public bool machineType { get; set; }
            public bool name { get; set; }
            public bool parent { get; set; }
            public bool publisherIdentity { get; set; }
            public bool servedStatus { get; set; }
            public bool soldTo { get; set; }
            public bool status { get; set; }
            public bool userInfo { get; set; }
            public bool vmDetails { get; set; }
        }

        public class SortBy
        {
            public bool ascending { get; set; }
            public string sortKey { get; set; }
        }

        public class SortBys
        {
            public List<SortBy> sortBy { get; set; }
        }

        public class Root
        {
            public int batchSize { get; set; }
            public int pageNumber { get; set; }
            public QueryParams queryParams { get; set; }
            public ResponseConfig responseConfig { get; set; }
            public SortBys sortBys { get; set; }
        }
    }
}
