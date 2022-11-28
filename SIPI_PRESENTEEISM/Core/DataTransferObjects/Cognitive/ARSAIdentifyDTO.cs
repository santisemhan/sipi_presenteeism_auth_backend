namespace SIPI_PRESENTEEISM.Core.DataTransferObjects.Cognitive
{
    public class ARSAIdentifyDTO
    {
        public string Status { get; set; }

        public List<ARSAPersonDTO> Recognition_Result { get; set; }
    }

    public class ARSAPersonDTO
    {
        public string Recognition_Uidresult { get; set; }

        public ARSALivenessDTO Liveness { get; set; }
    }

    public class ARSALivenessDTO
    {
        public bool Is_Real_Face { get; set; }
    }
}
