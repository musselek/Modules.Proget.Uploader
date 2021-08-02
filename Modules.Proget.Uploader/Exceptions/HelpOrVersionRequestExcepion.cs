namespace Modules.Proget.Uploader.Exceptions
{
    public class HelpOrVersionRequestExcepion : BaseParserErrorException
    {
        public override string Code { get; } = "help_or_version_error";
        public HelpOrVersionRequestExcepion() : base("Help or version request") { }
    }
}
