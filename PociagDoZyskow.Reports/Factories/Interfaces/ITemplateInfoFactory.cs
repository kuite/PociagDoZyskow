using System;
using System.Collections.Generic;
using System.Text;
using PociagDoZyskow.HtmlReports.Model;

namespace PociagDoZyskow.HtmlReports.Factories.Interfaces
{
    public interface ITemplateInfoFactory
    {
        TemplateInfo Create(string templateInfoName);
    }
}
