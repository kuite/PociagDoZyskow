using System;
using System.Collections.Generic;
using System.Text;

namespace PociagDoZyskow.Reports.Reports
{
    public abstract class BaseReport
    {
        public abstract string TemplateName { get; }

        public abstract string GetFilledTemplate();

        protected string GetTemplate()
        {
            return "";
        }
    }
}
