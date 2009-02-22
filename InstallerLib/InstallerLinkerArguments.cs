using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;
using System.IO;

namespace InstallerLib
{
    public class InstallerLinkerArguments
    {
        [Argument(ArgumentType.Required, HelpText = "Generated installer file", LongName = "Output", ShortName = "o")]
        public string output;
        [Argument(ArgumentType.Required, HelpText = "Template installer executable", LongName = "Template", ShortName = "t")]
        public string template;
        [Argument(ArgumentType.Required, HelpText = "Picture for the banner", LongName = "Banner", ShortName = "b")]
        public string banner;
        [Argument(ArgumentType.Required, HelpText = "XML configuration file", LongName = "Configuration", ShortName = "c")]
        public string config;
        [Argument(ArgumentType.AtMostOnce, HelpText = "Embed support files", LongName = "Embed", ShortName = "e")]
        public bool embed;
        [Argument(ArgumentType.AtMostOnce, HelpText = "Location of support files for embedding", LongName = "AppPath", ShortName = "a")]
        public string apppath;
        [Argument(ArgumentType.AtMostOnce, HelpText = "Verbose output", LongName = "Verbose", ShortName = "v")]
        public bool verbose;
        [Argument(ArgumentType.MultipleUnique, HelpText = "Additional files to embed", LongName = "EmbedFile", ShortName = "f")]
        public string[] embedFiles;

        public void Validate()
        {
            template = Path.GetFullPath(template);
            banner = Path.GetFullPath(banner);
            config = Path.GetFullPath(config);
            output = Path.GetFullPath(output);
            
            if (!string.IsNullOrEmpty(apppath))
            {
                apppath = Path.GetFullPath(apppath);
            }

            if (!File.Exists(template))
                throw new FileNotFoundException(template);

            if (!File.Exists(banner))
                throw new FileNotFoundException(banner);

            if (!File.Exists(config))
                throw new FileNotFoundException(config);

            if (!string.IsNullOrEmpty(apppath) && !Directory.Exists(apppath))
                throw new DirectoryNotFoundException(apppath);

            if (embedFiles != null)
            {
                foreach (string filename in embedFiles)
                {
                    string combinedFilename = Path.Combine(apppath, filename);
                    if (!File.Exists(combinedFilename))
                        throw new FileNotFoundException(combinedFilename);
                }
            }
        }

        public void WriteLine(string s)
        {
            if (verbose)
            {
                Console.WriteLine(s);
            }
        }
    }
}
