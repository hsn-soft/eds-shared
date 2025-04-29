namespace Eds.Shared.Helper.VeribanGlobal.Library.Model.GibServiceDto.GibBook
{
    public class BookGibResponse
    {
        public bool State { get; set; }
        public string Description { get; set; }
        public bool DontReQuery { get; set; }
        public int StatusCode { get; set; }
        public string FilePath { get; set; }

    }
}
