using System;
using System.Collections.Generic;
using System.Text;
using PociagDoZyskow.EmailReports.Factories.Interfaces;
using PociagDoZyskow.EmailReports.Model;

namespace PociagDoZyskow.EmailReports.Reports
{
    public abstract class BaseReport<T>
    {
        protected ITemplateInfoFactory _templateInfoFactory;

        protected BaseReport(ITemplateInfoFactory templateInfoFactory)
        {
            _templateInfoFactory = templateInfoFactory;
        }

        public abstract string GetFilledTemplate(T algorithmResult);
    }
}
