using System.Diagnostics;
using System.Xml;
using Newtonsoft.Json;
using SchwabApiCS;

namespace SchwabAPICS
{
   public class SchwabTokenService : SchwabTokensBase
    {
        public SchwabTokenService(string programPath)
        {
            if (string.IsNullOrWhiteSpace(programPath)) 
                throw new ArgumentException("Program path cannot be null or empty.", nameof(programPath));

            ExternalProgramPath = programPath;

            //// Initialize tokens by loading them from the provided program path
            //try
            //{
            //    tokens = new SchwabTokensData();
            //    using (var sr = new StreamReader(programPath))
            //    {
            //        var json = sr.ReadToEnd();
            //        tokens = JsonConvert.DeserializeObject<SchwabTokensData>(json) ??
            //                 throw new InvalidOperationException("Failed to deserialize token data.");
            //    }

            //    if (string.IsNullOrEmpty(tokens.AccessToken)
            //      || IsExpired(tokens.AccessTokenExpires)
            //      ||string.IsNullOrEmpty(tokens.Redirect_uri))
            //        throw new InvalidOperationException("Token data is incomplete.");
            //}
            //catch (Exception ex)
            //{
            //    throw new InvalidOperationException("An error occurred while initializing Schwab tokens.", ex);
            //}

            var json = GetTokenInformation();
            tokens = JsonConvert.DeserializeObject<SchwabTokensData>(json) ??
                     throw new InvalidOperationException("Failed to deserialize token data.");
        }
                
        public string ExternalProgramPath { get; }

        public string GetTokenInformation()
        {
            // Define the external program and arguments
            // Initialize the process
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = ExternalProgramPath,
                Arguments = null,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            try
            {
                using Process process = Process.Start(processStartInfo);
                if (process == null)
                {
                    throw new InvalidOperationException("Failed to start the external program.");
                }
                // Read the output from the external program
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                // Check for errors or empty output
                if (string.IsNullOrWhiteSpace(output))
                {
                    throw new InvalidOperationException("The external program did not return any token information.");
                }
                return output.Trim();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new InvalidOperationException("An error occurred while retrieving token information.", ex);
            }
        }

    }
}