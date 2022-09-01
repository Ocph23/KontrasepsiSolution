namespace MainApp.Models
{

    public enum PosisiRahim { 
        Retrofleksi, Antefleksi
    }

    public enum KeadaanUmum{
        Baik, Sedang, Kurang
    
    }

    public enum Gender
    {
        Pria, Wanita
    }

    public enum Pendidikan { 
        TidakTamatSD,
        TamatSD,
        TamatSLTP,
        TamatSLTA,
        TamatPT
    }


    public enum Pekerjaan
    {
        PegawaiPemeritah,
        PegawaiSwasta,
        Petani,
        Nelayan,
        TidakBekerja,   
        LainLain
    }

    public enum JKN
    {
        JKNPenerimaBantuan,
        JKNBukanPenerimaBantuan,
        BukanPesertaJKN
    }


    public static class JKNExtensions
    {
        public static string ToStringText(this JKN data)
        {
            switch (data)
            {
                case JKN.JKNPenerimaBantuan:
                    return "Peserta JKN Penerima Bantuan Iuran";
                case JKN.JKNBukanPenerimaBantuan:
                    return "Peserta JKN Bukan Penerima Bantuan Iuran";
                default:
                    return "Bukan Penerima JKN";
            }
        }
    }





    public static class PendidikanExtensions
    {
        public static string ToStringText(this Pendidikan data )
        {
            switch (data)
            {
                case Pendidikan.TidakTamatSD:
                    return "Tidak Tamat SD";
                case Pendidikan.TamatSD:
                    return "Tamat SD";
                case Pendidikan.TamatSLTP:
                    return "Tamat SLTP";
                case Pendidikan.TamatSLTA:
                    return "Tamat SLTA";
                case Pendidikan.TamatPT:
                    return "Tamat Perguruan Tinggi";
                default:
                    return "Tidak Tamat SD";
            }
        }
    }



    public static class PekerjaanExtensions
    {
        public static string ToStringText(this Pekerjaan data)
        {
            switch (data)
            {
                case Pekerjaan.PegawaiPemeritah:
                    return "Pegawai Pemerintah";
                case Pekerjaan.PegawaiSwasta:
                    return "Pegawai Swasta";
                case Pekerjaan.Petani:
                    return "Petani";
                case Pekerjaan.Nelayan:
                    return "Nelayan";
                case Pekerjaan.TidakBekerja:
                    return "Tidak Bekerja";
                case Pekerjaan.LainLain:
                    return "Lain-Lain";
                default:
                    return "Lain-Lain";
            }
        }
    }


}
