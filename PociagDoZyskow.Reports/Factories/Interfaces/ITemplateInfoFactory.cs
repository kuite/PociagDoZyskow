using System;
using System.Collections.Generic;
using System.Text;
using PociagDoZyskow.EmailReports.Model;

namespace PociagDoZyskow.EmailReports.Factories.Interfaces
{
    public interface ITemplateInfoFactory
    {
        TemplateInfo Create(string templateInfoName);
    }
}
