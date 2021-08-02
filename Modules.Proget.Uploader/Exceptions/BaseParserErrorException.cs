using System;

namespace Modules.Proget.Uploader.Exceptions
{
    public abstract class BaseParserErrorException : Exception
    {
        public virtual string Code { get; }

        protected BaseParserErrorException(string message) : base(message) { }
    }
}
