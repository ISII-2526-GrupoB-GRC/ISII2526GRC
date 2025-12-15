using System.Security.Policy;

namespace AppForSEII2526.API.DTOs.ReceiptDTOs
{
    public class ReceiptForCreateDTO
    {

        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Introduce tu nombre")]
        public string username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Introduce tus apellidos")]
        public string usersurname { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Display(Name = "Dirección de envío")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Introduce tu direccion de envio")]
        public string userdeliveryaddress { get; set; }

        [Required]
        public PaymentMethodTypes paymentMethod { get; set; }

        public IList<ReceiptItemDTO> receiptItems { get; set; }

        //Constructores
        public ReceiptForCreateDTO()
        {
            receiptItems = new List<ReceiptItemDTO>();
        }

        public ReceiptForCreateDTO(string username, string usersurname, string userdeliveryaddress, PaymentMethodTypes paymentMethod, IList<ReceiptItemDTO> receiptItems)
        {
            //this.name = name;
            //this.scaleName = scaleName;
            //this.cost = cost;
            //this.model = model;
            this.username = username ?? throw new ArgumentNullException(nameof(username));
            this.usersurname = usersurname ?? throw new ArgumentNullException(nameof(usersurname));
            this.userdeliveryaddress = userdeliveryaddress;
            this.paymentMethod = paymentMethod;
            this.receiptItems = receiptItems ?? throw new ArgumentNullException(nameof(receiptItems));
        }

        public override bool Equals(object? obj)
        {
            return obj is ReceiptForCreateDTO dTO &&
                   username == dTO.username &&
                   usersurname == dTO.usersurname &&
                   userdeliveryaddress == dTO.userdeliveryaddress &&
                   paymentMethod == dTO.paymentMethod &&
                   receiptItems.SequenceEqual(dTO.receiptItems);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(username, usersurname, userdeliveryaddress, paymentMethod, receiptItems);
        }
    }
}