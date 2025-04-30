
namespace Eds.Shared.Helper.VeribanGlobal.Library.Common.ConstRepository
{
    public class UnitType
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }



        private static List<UnitType> _unitTypes;

        private static object lockSys = new object();

        static UnitType()
        {
            lock (lockSys)
            {
                BuildUnitType();
            }
        }

        public static List<UnitType> GetUnitTypeList()
        {
            return _unitTypes;
        }

        private static void BuildUnitType()
        {
            _unitTypes = new List<UnitType>();
            _unitTypes.Add(new UnitType() { Code = "DAY", Name = "Gün" });
            _unitTypes.Add(new UnitType() { Code = "MON", Name = "Ay" });
            _unitTypes.Add(new UnitType() { Code = "ANN", Name = "Yil" });
            _unitTypes.Add(new UnitType() { Code = "HUR", Name = "Saat" });
            _unitTypes.Add(new UnitType() { Code = "MIN", Name = "Dakika" });
            _unitTypes.Add(new UnitType() { Code = "SEC", Name = "Saniye" });
            _unitTypes.Add(new UnitType() { Code = "NIU", Name = "Adet" });
            _unitTypes.Add(new UnitType() { Code = "C62", Name = "AdetC62" });
            _unitTypes.Add(new UnitType() { Code = "PK", Name = "Paket" });
            _unitTypes.Add(new UnitType() { Code = "PR", Name = "Çift" });
            _unitTypes.Add(new UnitType() { Code = "BX", Name = "Kutu" });
            _unitTypes.Add(new UnitType() { Code = "MGM", Name = "MG" });
            _unitTypes.Add(new UnitType() { Code = "KGM", Name = "KG" });
            _unitTypes.Add(new UnitType() { Code = "LTR", Name = "LT" });
            _unitTypes.Add(new UnitType() { Code = "TNE", Name = "Ton" });
            _unitTypes.Add(new UnitType() { Code = "Net", Name = "Net" });
            _unitTypes.Add(new UnitType() { Code = "GT", Name = "GT" });
            _unitTypes.Add(new UnitType() { Code = "MMT", Name = "MM" });
            _unitTypes.Add(new UnitType() { Code = "CMT", Name = "CM" });
            _unitTypes.Add(new UnitType() { Code = "MTR", Name = "M" });
            _unitTypes.Add(new UnitType() { Code = "KTM", Name = "KM" });
            _unitTypes.Add(new UnitType() { Code = "ML", Name = "ML" });
            _unitTypes.Add(new UnitType() { Code = "MMQ", Name = "MM3" });
            _unitTypes.Add(new UnitType() { Code = "CM3", Name = "CM3" });
            _unitTypes.Add(new UnitType() { Code = "MTK", Name = "M2" });
            _unitTypes.Add(new UnitType() { Code = "MTQ", Name = "M3" });
            _unitTypes.Add(new UnitType() { Code = "KJ", Name = "KJ" });
            _unitTypes.Add(new UnitType() { Code = "CL", Name = "CL" });
            _unitTypes.Add(new UnitType() { Code = "KWH", Name = "KWH" });
            _unitTypes.Add(new UnitType() { Code = "SET", Name = "Set" });

            _unitTypes.Add(new UnitType() { Code = "GRM", Name = "Gram" });
            _unitTypes.Add(new UnitType() { Code = "YRD", Name = "Yarda" });
            _unitTypes.Add(new UnitType() { Code = "RO", Name = "Rulo" });
            _unitTypes.Add(new UnitType() { Code = "CTM", Name = "Karat" });

            _unitTypes.Add(new UnitType() { Code = "TN", Name = "Teneke" });
            _unitTypes.Add(new UnitType() { Code = "CI", Name = "Teneke Kutu" });

            _unitTypes.Add(new UnitType() { Code = "2A", Name = "Koli" });
            _unitTypes.Add(new UnitType() { Code = "PAL", Name = "Palet" });
            _unitTypes.Add(new UnitType() { Code = "EA", Name = "Ayrı Birimler" });

            //B32             KG-METRE KARE
            //C62             ADET(UNIT)
            //CCT             TON BAŞINA TAŞIMA KAPASİTESİ
            //D30          BRÜT KALORİ DEĞERİ
            //D40          BİN LİTRE
            //GFI          FISSILE İZOTOP GRAMI
            //GRM          GRAM
            //GT               GROSS TON
            //H62             YÜZ ADET
            //K20             KİLOGRAM POTASYUM OKSİT
            //K58             KURUTULMUŞ NET AĞIRLIKLI KİLOGRAMI
            //K62             KİLOGRAM - ADET
            //KGM          KİLOGRAM
            //KMA           METİL AMİNLERİN KİLOGRAMI
            //KNI             AZOTUN KİLOGRAMI
            //KPH            Kg POTASYUM OKSİD
            //KSD        % 90 KURU ÜRÜN KİLOGRAMI
            //KSH             SODYUM HİDROKSİT KİLOGRAMI
            //KUR            URANYUM KİLOGRAMI
            //KWH           KİLOWATT SAAT
            //KWT           KİLOWATT
            //LPA             SAF ALKOL LİTRESİ
            //LTR              LİTRE
            //MTK           METRE KARE
            //MTQ           METRE KÜP
            //MTR           METRE
            //NCL             HÜCRE ADEDİ
            //CTM KARAT
            //PR               ÇİFT
            //R9                BİN METRE KÜP
            //SET              SET
            //T3                BİN ADET


            //AYR ALTIN AYARI
            //B32 KG - METRE KARE
            //  BAS BAS
            //  C62 ADET(UNIT)
            //CCT TON BAŞINA TAŞIMA KAPASİTESİ
            //CPR ADET - ÇİFT
            //CTM GROSS TON
            //D30 BRÜT KALORİ DEĞERİ
            //D40 BİN LİTRE
            //GFI FISSILE İZOTOP GRAMI
            //GMS GÜMÜŞ
            //GRM GRAM
            //GT GROSS TON
            //H62 YÜZ ADET
            //HAGR HAS AĞIRLIK
            //K20 KİLOGRAM POTASYUM OKSİT
            //K58 KURUTULMUŞ NET AĞIRLIKLI KİLOGRAMI
            //K62 KİLOGRAM-ADET
            //KFO DİFOSFOR PENTAOKSİT KİLOGRAMI
            //KGM KİLOGRAM
            //KH6 KİLOGRAM-BAŞ
            //KHO HİDROJEN PEROKSİT KİLOGRAMI
            //KMA METİL AMİNLERİN KİLOGRAMI
            //KNI AZOTUN KİLOGRAMI
            //KOH KİLOGRAM POTASYUM HİDROKSİT
            //KPH Kg POTASYUM OKSİD
            //KPR KİLOGRAM - ÇİFT
            //KSD % 90 KURU ÜRÜN KİLOGRAMI
            // KSH SODYUM HİDROKSİT KİLOGRAMI
            // KUR URANYUM KİLOGRAMI
            //KWH KİLOWATT SAAT
            //KWT KİLOWATT
            //LPA SAF ALKOL LİTRESİ
            //LTR LİTRE
            //MTK METRE KARE
            //MTQ METRE KÜP
            //MTR METRE
            //NCL HÜCRE ADEDİ
            //NCR KARAT
            //OMV OTV Maktu Vergi
            //OTB OTV birim fiyatı
            //PR ÇİFT
            //R9 BİN METRE KÜP
            //SET SET
            //T3 BİN ADET
            //TWH BİN KİLOWATT SAAT






        }
    }
}
