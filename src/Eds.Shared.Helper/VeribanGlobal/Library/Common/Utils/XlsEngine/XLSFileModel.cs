using System.Data;
using System.Globalization;
using System.Text;

namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.Utils.XlsEngine
{
    /// <summary>
    /// Main class implementation
    /// </summary>
    public class XlsFile
    {
        private readonly XlsBiffStream m_stream = null;
        private XlsWorkbookGlobals m_globals = null;
        private readonly List<XlsWorksheet> m_sheets = new List<XlsWorksheet>();
        private readonly DataSet m_workbookData = null;
        private ushort m_version = 0x0600;
        private Encoding m_encoding = Encoding.Default;



        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="file">Stream with source data</param>
        public XlsFile(Stream file)
        {
            Stream m_file = file;
            XlsHeader m_hdr = XlsHeader.ReadHeader(m_file);
            XlsRootDirectory dir = new XlsRootDirectory(m_hdr);
            XlsDirectoryEntry workbookEntry = dir.FindEntry("Workbook");
            if (workbookEntry == null)
                workbookEntry = dir.FindEntry("Book");
            if (workbookEntry == null)
                throw new FileNotFoundException("Neither stream 'Workbook' nor 'Book' was found in file");
            if (workbookEntry.EntryType != Stgty.STGTY_STREAM)
                throw new FormatException("Workbook directory entry is not a Stream");
            m_stream = new XlsBiffStream(m_hdr, workbookEntry.StreamFirstSector);
            ReadWorkbookGlobals();
            m_workbookData = new DataSet();
            for (int i = 0; i < m_sheets.Count; i++)
                if (ReadWorksheet(m_sheets[i]))
                    m_workbookData.Tables.Add(m_sheets[i].Data);
            m_globals.Sst = null;
            m_globals = null;
            m_sheets = null;
            m_stream = null;
        }
        public XlsFile(String filename)
        {
            if (IsFileUsedbyAnotherProcess(filename))
            {
                throw new ArgumentException("Bu dosya şu anda açık. Lütfen kapatıp tekrar deneyiniz.");
            }

            FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            Stream m_file = file;
            XlsHeader m_hdr = XlsHeader.ReadHeader(m_file);
            XlsRootDirectory dir = new XlsRootDirectory(m_hdr);
            XlsDirectoryEntry workbookEntry = dir.FindEntry("Workbook") ?? dir.FindEntry("Book");
            if (workbookEntry == null)
                throw new FileNotFoundException("Neither stream 'Workbook' nor 'Book' was found in file");
            if (workbookEntry.EntryType != Stgty.STGTY_STREAM)
                throw new FormatException("Workbook directory entry is not a Stream");
            m_stream = new XlsBiffStream(m_hdr, workbookEntry.StreamFirstSector);
            ReadWorkbookGlobals();
            m_workbookData = new DataSet();
            for (int i = 0; i < m_sheets.Count; i++)
                if (ReadWorksheet(m_sheets[i]))
                    m_workbookData.Tables.Add(m_sheets[i].Data);
            m_globals.Sst = null;
            m_globals = null;
            m_sheets = null;
            m_stream = null;
        }

        private bool IsFileUsedbyAnotherProcess(string filename)
        {
            try
            {
                File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (IOException)
            {
                return true;
            }
            return false;

        }


        /// <summary>
        /// DataSet with workbook data, Tables represent Sheets
        /// </summary>
        public DataSet WorkbookData
        {
            get { return m_workbookData; }
        }

        /// <summary>
        /// Private method, reads Workbook Globals section
        /// </summary>
        private void ReadWorkbookGlobals()
        {
            m_globals = new XlsWorkbookGlobals();
            m_stream.Seek(0, SeekOrigin.Begin);
            XlsBiffRecord rec = m_stream.Read();
            XlsBiffBof bof = rec as XlsBiffBof;
            if (bof == null || bof.Type != BiffType.WorkbookGlobals)
                throw new InvalidDataException("Stream has invalid data");
            m_version = bof.Version;
            m_encoding = Encoding.Unicode;
            bool isV8 = (m_version >= 0x600);
            bool sst = false;
            while ((rec = m_stream.Read()) != null)
            {
                switch (rec.ID)
                {
                    case BiffRecordType.INTERFACEHDR:
                        m_globals.InterfaceHdr = (XlsBiffInterfaceHdr)rec;
                        break;
                    case BiffRecordType.BOUNDSHEET:
                        XlsBiffBoundSheet sheet = (XlsBiffBoundSheet)rec;
                        if (sheet.Type != XlsBiffBoundSheet.SheetType.Worksheet) break;
                        sheet.IsV8 = isV8;
                        sheet.UseEncoding = m_encoding;
                        m_sheets.Add(new XlsWorksheet(m_globals.Sheets.Count, sheet));
                        m_globals.Sheets.Add(sheet);
                        break;
                    case BiffRecordType.MMS:
                        m_globals.Mms = rec;
                        break;
                    case BiffRecordType.COUNTRY:
                        m_globals.Country = rec;
                        break;
                    case BiffRecordType.CODEPAGE:
                        m_globals.CodePage = (XlsBiffSimpleValueRecord)rec;
                        m_encoding = Encoding.GetEncoding(m_globals.CodePage.Value);
                        break;
                    case BiffRecordType.FONT:
                    case BiffRecordType.FONT_V34:
                        m_globals.Fonts.Add(rec);
                        break;
                    case BiffRecordType.FORMAT:
                    case BiffRecordType.FORMAT_V23:
                        m_globals.Formats.Add(rec);
                        break;
                    case BiffRecordType.XF:
                    case BiffRecordType.XF_V4:
                    case BiffRecordType.XF_V3:
                    case BiffRecordType.XF_V2:
                        m_globals.ExtendedFormats.Add(rec);
                        break;
                    case BiffRecordType.SST:
                        m_globals.Sst = (XlsBiffSst)rec;
                        sst = true;
                        break;
                    case BiffRecordType.CONTINUE:
                        if (!sst) break;
                        XlsBiffContinue contSST = (XlsBiffContinue)rec;
                        m_globals.Sst.Append(contSST);
                        break;
                    case BiffRecordType.EXTSST:
                        m_globals.ExtSst = rec;
                        sst = false;
                        break;
                    case BiffRecordType.EOF:
                        if (m_globals.Sst != null)
                            m_globals.Sst.ReadStrings();
                        return;
                    default:
                        continue;
                }
            }
        }

        /// <summary>
        /// private method, reads sheet data
        /// </summary>
        /// <param name="sheet">Sheet object, whose data to read</param>
        /// <returns>True if sheet was read successfully, otherwise False</returns>
        private bool ReadWorksheet(XlsWorksheet sheet)
        {
            m_stream.Seek((int)sheet.DataOffset, SeekOrigin.Begin);
            XlsBiffBof bof = m_stream.Read() as XlsBiffBof;
            if (bof == null || bof.Type != BiffType.Worksheet)
                return false;
            XlsBiffIndex idx = m_stream.Read() as XlsBiffIndex;
            bool isV8 = (m_version >= 0x600);
            if (idx != null)
            {
                idx.IsV8 = isV8;
                DataTable dt = new DataTable(sheet.Name);

                XlsBiffRecord trec;
                XlsBiffDimensions dims = null;
                do
                {
                    trec = m_stream.Read();
                    if (trec.ID == BiffRecordType.DIMENSIONS)
                    {
                        dims = (XlsBiffDimensions)trec;
                        break;
                    }
                }
                while (trec.ID != BiffRecordType.ROW);
                int maxCol = 256;
                if (dims != null)
                {
                    dims.IsV8 = isV8;
                    maxCol = dims.LastColumn;
                    sheet.Dimensions = dims;
                }


                for (int i = 0; i < maxCol; i++)
                {
                    dt.Columns.Add("Column" + (i + 1), typeof(string));
                }


                sheet.Data = dt;
                uint maxRow = idx.LastExistingRow;
                if (idx.LastExistingRow <= idx.FirstExistingRow)
                    return true;
                dt.BeginLoadData();
                for (int i = 0; i <= maxRow; i++)
                    dt.Rows.Add(dt.NewRow());
                uint[] dbCellAddrs = idx.DbCellAddresses;
                for (int i = 0; i < dbCellAddrs.Length; i++)
                {
                    XlsBiffDbCell dbCell = (XlsBiffDbCell)m_stream.ReadAt((int)dbCellAddrs[i]);
                    XlsBiffRow row = null;
                    int offs = dbCell.RowAddress;
                    do
                    {
                        row = m_stream.ReadAt(offs) as XlsBiffRow;
                        if (row == null) break;
                        offs += row.Size;
                    }
                    while (row != null);
                    while (true)
                    {
                        XlsBiffRecord rec = m_stream.ReadAt(offs);
                        offs += rec.Size;
                        if (rec is XlsBiffDbCell) break;
                        if (rec is XlsBiffEof) break;
                        XlsBiffBlankCell cell = rec as XlsBiffBlankCell;
                        if (cell == null)
                        {
                            continue;
                        }
                        if (cell.ColumnIndex >= maxCol) continue;
                        if (cell.RowIndex > maxRow) continue;



                        switch (cell.ID)
                        {
                            case BiffRecordType.INTEGER:
                            case BiffRecordType.INTEGER_OLD:
                                dt.Rows[cell.RowIndex][cell.ColumnIndex] = ((XlsBiffIntegerCell)cell).Value.ToString();
                                break;
                            case BiffRecordType.NUMBER:
                            case BiffRecordType.NUMBER_OLD:
                                dt.Rows[cell.RowIndex][cell.ColumnIndex] = FormatNumber(((XlsBiffNumberCell)cell).Value);
                                break;
                            case BiffRecordType.LABEL:
                            case BiffRecordType.LABEL_OLD:
                            case BiffRecordType.RSTRING:
                                dt.Rows[cell.RowIndex][cell.ColumnIndex] = ((XlsBiffLabelCell)cell).Value;
                                break;
                            case BiffRecordType.LABELSST:
                                {
                                    string tmp = m_globals.Sst.GetString(((XlsBiffLabelSstCell)cell).SSTIndex);
                                    dt.Rows[cell.RowIndex][cell.ColumnIndex] = tmp;
                                }
                                break;
                            case BiffRecordType.RK:
                                dt.Rows[cell.RowIndex][cell.ColumnIndex] = FormatNumber(((XlsBiffRKCell)cell).Value);
                                break;
                            case BiffRecordType.MULRK:
                                for (ushort j = cell.ColumnIndex; j <= ((XlsBiffMulRKCell)cell).LastColumnIndex; j++)
                                    dt.Rows[cell.RowIndex][j] = FormatNumber(((XlsBiffMulRKCell)cell).GetValue(j));
                                break;
                            case BiffRecordType.BLANK:
                            case BiffRecordType.BLANK_OLD:
                            case BiffRecordType.MULBLANK:
                                // Skip blank cells
                                break;
                            case BiffRecordType.FORMULA:
                            case BiffRecordType.FORMULA_OLD:
                                ((XlsBiffFormulaCell)cell).UseEncoding = m_encoding;
                                object val = ((XlsBiffFormulaCell)cell).Value;
                                if (val != null)
                                {
                                    var xxx = val as Nullable<FormulaError>;
                                    if (val is FormulaError && xxx.HasValue)
                                        val = "#" + xxx.Value.ToString();
                                    else if (val is double)
                                        val = FormatNumber((double)val);
                                }
                                else
                                {
                                    val = string.Empty;
                                }

                                dt.Rows[cell.RowIndex][cell.ColumnIndex] = val.ToString();
                                break;
                            default:
                                break;
                        }

                        //set captions
                        if (cell.RowIndex == 0)
                        {
                            try
                            {
                                if (dt.Rows[cell.RowIndex][cell.ColumnIndex] != null && dt.Rows[cell.RowIndex][cell.ColumnIndex] != DBNull.Value)
                                    dt.Columns[cell.ColumnIndex].Caption = dt.Rows[cell.RowIndex][cell.ColumnIndex].ToString();
                            }
                            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
                        }
                    }
                }
                dt.EndLoadData();
            }
            else
            {
                return false;
            }

            return true;
        }

        private string FormatNumber(double x)
        {
            if (Math.Floor(x) == x)
                return Math.Floor(x).ToString();
            else
                return Math.Round(x, 2).ToString(CultureInfo.InvariantCulture);
        }

    }
}
