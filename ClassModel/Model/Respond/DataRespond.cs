using System;

namespace ClassModel.Model.Respond
{
    public class DataRespond
    {
        public Boolean success { get; set; }
        public dynamic data { get; set; }
        public string message { get; set; }
        public dynamic error { get; set; }
    }

    public class totalrecorded
    {
        public string total { get; set; }

    }
    public class monthFromTo
    {
        public string monthFrom { get; set; }
        public string monthTo { get; set; }
    }
}
