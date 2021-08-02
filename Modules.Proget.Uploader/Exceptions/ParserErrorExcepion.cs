using CommandLine;
using System.Collections.Generic;
using System.Linq;

namespace Modules.Proget.Uploader.Exceptions
{
    public class ParserErrorExcepion : BaseParserErrorException
    {
        public override string Code { get; } = "parser_error";
        public ParserErrorExcepion(IEnumerable<Error> error) : base($"CommandLine errors :[{string.Join(", ", error.Select(x => x.ToString()))}]") { }
    }
}
