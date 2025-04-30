namespace Eds.Shared.Helper.eInvoice.Library.Model.DocumentControl
{
    public class DocumentControlResult
    {
        public bool ResultStatus { get; set; }

        public byte ResultErrorState { get; set; }

        public string ResultErrorStateDesc { get; set; }

        public string ResultDocumentReferenceNumber { get; set; }

        public virtual object TransferDataHeaderInfo { get; set; }

        public virtual List<object> TransferDataModelList { get; set; }
    }
}
