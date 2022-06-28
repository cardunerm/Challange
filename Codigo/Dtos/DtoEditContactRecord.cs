using DesafioLaNacion.Dtos.Media;
namespace DesafioLaNacion.Dtos
{
    public class DtoEditContactRecord : DtoBaseModel
    {
        public long ContactRecordId { get; set; }
        public string Ciudad { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public string WorkPhoneNumber { get; set; }
        public string PersonalPhoneNumber { get; set; }
        public RequestAddImage ProfileImage { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }



    }
}
