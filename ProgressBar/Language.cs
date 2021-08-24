namespace ProgressBar
{
    public class Language
    {
        public string x;
        public string y;
        public string width = "";
        public string height = "";
    }


    public class Korean : Language
    {
        public Korean()
        {
            x = "x 좌표";
            y = "y 좌표";
            width = "가로 크기";
            height = "세로 길이";
        }
    }
    
    public class English : Language
    {
        public English()
        {
            x = "X Coordinates";
            y = "Y Coordinates";
            width = "Set Width";
            height = "Set Height";
        }
    }
}