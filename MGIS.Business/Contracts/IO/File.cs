using System;

namespace NGAT.Business.Contracts.IO
{
    /// <summary>
    /// Represents a file.
    /// </summary>
    public abstract class File
    {
        /// <summary>
        /// The upper case name or ID of this class of file. ex: "PBF", "GRF", ...
        /// </summary>
        public string FormatID { get; private set; }

        /// <summary>
        /// The extension of the file. ex ".pbf", ".grf", ...
        /// </summary>
        public string Extension { get => "." + FormatID.ToLower(); }

        /// <summary>
        /// The Uri of the file.
        /// </summary>
        public Uri FileUri { get; set; }

        public File(string formatID, Uri fileUri)
        {
            FormatID = formatID;
            FileUri = fileUri;
        }

        public override string ToString()
        {
            return FormatID + " file";
        }

    }
}
