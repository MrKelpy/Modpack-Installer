namespace ModpackInstallerLauncher.utils
{
    /// <summary>
    /// This struct implements a model useful for carrying around the needed
    /// data about a given modpack.
    /// </summary>
    public struct Modpack
    {
        public string Name { get; set; }
        public string Description  { get; set; }
        public string Url { get; set; }

        /// <summary>
        /// This constructor is used to facilitate the creation of a new modpack
        /// </summary>
        /// <param name="data">The varargs for the modpack properties.</param>
        public Modpack(params string[] data)
        {
            this.Name = data[0];
            this.Description = data[1];
            this.Url = data[2];
        }
    }
}