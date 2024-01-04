namespace AiPrompt.I18n; 

public static class Languages {
    public const string Default = SimplifiedChinese;
    public const string SimplifiedChinese = "zh_cn";
    public const string English = "en";
    /// <summary>
    /// 键
    /// </summary>
    public static class Key {
        public const string Register = "Register";
        public const string Login = "Login";
        public const string Username = "Username";
        public const string Password = "Password";
        public const string OK = "OK";
        public const string Cancel = "Cancel";
        public const string Email = "Emial";
        public const string Nickname = "Nickname";

        /// <summary>
        /// 导航
        /// </summary>
        public static class Shell {
            public const string Home = "Home";
            public const string Song = "Song";
            public const string Artist = "Artist";
            public const string Album = "Album";
            public const string Search = "Search";
            public const string Genre = "Genre";
            public const string Local = "Local";
            public const string Upload = "Upload";
            public const string Setting = "Setting";
        }

        public static class Uploader {
            public const string Select = "Select";
            public const string Upload = "Upload";
            public const string TrackName = "TrackName";
        }

        public static class Album {
            public const string PlayAll = "PlayAll";
        }
    }
}