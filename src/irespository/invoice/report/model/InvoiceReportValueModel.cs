﻿using System.Collections.Generic;

namespace irespository.invoice.report.model
{
    public class InvoiceReportValueModel
    {
        public int Key { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public IList<int> StoreRecordIds { get; set; }
    }
}
