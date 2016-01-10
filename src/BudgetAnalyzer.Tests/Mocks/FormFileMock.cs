using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNet.Http.Internal;

namespace BudgetAnalyzer.Tests.Mocks
{
    public class FormFileMock : IFormFile, IDisposable
    {
        private readonly FileInfo localFile;

        private readonly bool fileIsOwned;

        public FormFileMock(FileInfo localFile)
        {
            this.localFile = localFile;
            fileIsOwned = false;
        }

        public FormFileMock(byte[] data, string filename)
        {
            File.WriteAllBytes(filename, data);
            this.localFile = new FileInfo(filename);
            fileIsOwned = true;
        }

        public FormFileMock(string data, string filename)
        {
            File.WriteAllText(filename, data);
            this.localFile = new FileInfo(filename);
            fileIsOwned = true;
        }

        #region IFormFile

        public string ContentDisposition
        {
            get { return $"form-data; name=\"Attachment\"; filename=\"{localFile.Name}\""; }
        }

        public string ContentType
        {
            get { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
        }

        public IHeaderDictionary Headers
        {
            get
            {
                return new HeaderDictionary
                {
                    { nameof(ContentDisposition), ContentDisposition },
                    { nameof(ContentType), ContentType },
                };
            }
        }

        public long Length
        {
            get { return localFile.Length; }
        }

        public Stream OpenReadStream()
        {
            return localFile.OpenRead();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (fileIsOwned)
                localFile.Delete();
        }

        ~FormFileMock()
        {
            Dispose();
        }

        #endregion
    }
}
