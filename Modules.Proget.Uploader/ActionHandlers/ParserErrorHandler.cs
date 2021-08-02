using CommandLine;
using Modules.Proget.Uploader.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Modules.Proget.Uploader.ActionHandlers
{
    internal sealed class ParserErrorHandler
    {
        internal static void Run(IEnumerable<Error> errs)
        {
            if (errs.Any(x => x is HelpRequestedError || x is VersionRequestedError))
                throw new HelpOrVersionRequestExcepion();
            else
                throw new ParserErrorExcepion(errs);
        }
    }
}
