using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using PociagDoZyskow.HtmlReports.Factories.Interfaces;
using PociagDoZyskow.HtmlReports.Model;

namespace PociagDoZyskow.HtmlReports.Factories
{
    public class TemplateInfoFactory : ITemplateInfoFactory
    {
        private readonly IConfiguration _configuration;

        public TemplateInfoFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TemplateInfo Create(string templateInfoName)
        {
            var templatesPath = _configuration["ConnectionStrings:templates"];
            var filePath = string.Concat(templatesPath, "\\", templateInfoName, ".html");

            string contents = File.ReadAllText(filePath);
            var templateInfo = new TemplateInfo
            {
                Content = contents,
                Name = templateInfoName,
                Subject = templateInfoName
            };
            return templateInfo;
        }
    }
}
