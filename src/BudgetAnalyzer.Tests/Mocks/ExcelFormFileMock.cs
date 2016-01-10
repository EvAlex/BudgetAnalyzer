using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BudgetAnalyzer.Tests.Mocks
{
    public class ExcelFormFileMock : FormFileMock
    {
        public ExcelFormFileMock(XDocument xml)
            : base(xml.ToString(), Path.ChangeExtension(Path.GetTempFileName(), "xlsx"))
        {
        }

        public ExcelFormFileMock(XDocument xml, string filename)
            : base(xml.ToString(), filename)
        {
        }
    }
}
