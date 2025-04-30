namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.FaultCodes
{
    //VERIBAN TRANSFER WEB SERVICE FAULT EXCEPTION CODES
    public sealed class TransferServiceFaultExeptionCode
    {
        private readonly int _Code;
        private readonly string _Name;
        private readonly string _Description;

        private TransferServiceFaultExeptionCode(int code, string name, string description)
        {
            this._Code = code;
            this._Name = name;
            this._Description = description;
        }

        public static readonly TransferServiceFaultExeptionCode SYSTEM_ERROR = new TransferServiceFaultExeptionCode(5000, "SYSTEM ERROR", "");

        //SYSTEM GENERAL
        public static readonly TransferServiceFaultExeptionCode PARAMETER_ERROR = new TransferServiceFaultExeptionCode(5001, "PARAMETER ERROR", "");
        public static readonly TransferServiceFaultExeptionCode LOGIN_FAIL = new TransferServiceFaultExeptionCode(5002, "LOGIN FAIL", "");
        public static readonly TransferServiceFaultExeptionCode SESSION_ERROR = new TransferServiceFaultExeptionCode(5003, "SESSION FAIL", "");
        public static readonly TransferServiceFaultExeptionCode ACCESS_ERROR = new TransferServiceFaultExeptionCode(5004, "ACCESS ERROR", "");

        //TRANSFER DOCUMENT
        public static readonly TransferServiceFaultExeptionCode HASH_ERROR = new TransferServiceFaultExeptionCode(5101, "HASH ERROR", "");
        public static readonly TransferServiceFaultExeptionCode ADD_ARCHIVE_ERROR = new TransferServiceFaultExeptionCode(5102, "ADD ARCHIVE ERROR", "");
        public static readonly TransferServiceFaultExeptionCode ADD_QUEUE_ERROR = new TransferServiceFaultExeptionCode(5103, "ADD QUEUE ERROR", "");

        //CANCEL DOCUMENT
        public static readonly TransferServiceFaultExeptionCode SET_CANCEL_ERROR = new TransferServiceFaultExeptionCode(5201, "SET CANCEL ERROR", "");

        //QUERY DOCUMENT
        public static readonly TransferServiceFaultExeptionCode QUERY_QUEUE_ERROR = new TransferServiceFaultExeptionCode(5301, "QUERY QUEUE ERROR", "");
        public static readonly TransferServiceFaultExeptionCode QUERY_DOCUMENT_ERROR = new TransferServiceFaultExeptionCode(5302, "QUERY DOCUMENT ERROR", "");

        //DOWNLOAD DOCUMENT
        public static readonly TransferServiceFaultExeptionCode DOWNLOAD_DOCUMENT_ERROR = new TransferServiceFaultExeptionCode(5401, "DOWNLOAD DOCUMENT ERROR", "");

        //OPERATION
        public static readonly TransferServiceFaultExeptionCode OPERATION_ERROR = new TransferServiceFaultExeptionCode(5501, "OPERATION ERROR", "");

        public override string ToString()
        {
            return _Name;
        }
        public string GetName()
        {
            return _Name;
        }
        public int GetCode()
        {
            return _Code;
        }
        public string GetDescription()
        {
            return _Description;
        }
    }
}
