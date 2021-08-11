using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace file_ingest_db.Annotations
{
    public class DateMinValue : RangeAttribute
    {
        public DateMinValue()
        : base(typeof(DateTime),
                DateTime.Now.ToShortDateString(),
                DateTime.MaxValue.ToShortDateString()
                )
        { }
    }
}
