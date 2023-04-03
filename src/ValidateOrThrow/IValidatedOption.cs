namespace ValidateOrThrow
{
    public interface IValidatedOption
    {
        /// <summary>
        /// Name of the section in the appsettings.json (or secrets) file.
        /// </summary>
        public string SectionName { get; }
    }
}