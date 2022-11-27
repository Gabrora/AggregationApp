using Newtonsoft.Json;

namespace AggregationApp.Helpers
{
    public static class JsonHelper
    {
        public static List<string> LoadUrls()
        {
            var jsonUrls = (new FileInfo("DataSetUrls.json")).OpenText().ReadToEnd();
            var urlList = JsonConvert.DeserializeObject<List<string>>(jsonUrls);
            return urlList;
        }
    }
}
