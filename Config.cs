using System.IO;

namespace Advent {
    public class Config {
        /// <summary>
        /// The default year to use if one is not provided on the command line.
        /// </summary>
        public int DefaultYear { get; set; } = 2022;

        /// <summary>
        /// The session cookie from adventofcode.com.
        /// </summary>
        public string SessionCookie { get; set; } = "";

        /// <summary>
        /// Root directory that other paths are calculated from.
        /// </summary>
        public string ApplicationDirectory { get; set; } = "";

        /// <summary>
        /// Where to look for puzzle input. Defaults to the Input directory in the project dir.
        /// </summary>
        public string InputDirectory { get; set; } = Path.Combine(Resources.ProjectDirectory.Trim(), "Input");

        public void SanityCheck() {
            if (DefaultYear < 2015) {
                throw new ArgumentOutOfRangeException(nameof(DefaultYear), "Config: Default year must be defined and >= 2015");
            }

            if (SessionCookie == null) {
                throw new ArgumentOutOfRangeException(nameof(SessionCookie), "Config: SessionCookie must be defined");
            }

            if (ApplicationDirectory == null) {
                throw new ArgumentOutOfRangeException(nameof(ApplicationDirectory), "Config: ApplicationDirectory must be defined");
            }

            if (InputDirectory == null) {
                throw new ArgumentOutOfRangeException(nameof(InputDirectory), "Config: InputDirectory must be defined");
            }
        }
    }
}
