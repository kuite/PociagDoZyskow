using System;
using System.Collections.Generic;
using System.Text;
using PociagDoZyskow.HtmlReports.Factories.Interfaces;
using PociagDoZyskow.HtmlReports.Model;

namespace PociagDoZyskow.HtmlReports.Reports
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
