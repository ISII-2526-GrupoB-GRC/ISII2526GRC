using AppForSEII2526.API.DTOs.RepairDTOs;

namespace AppForSEII2526.API.DTOs.ReceiptDTOs
{
    public class ReceiptDetailDTO
    {
        public int id { get; set; }
        public string username { get; set; }
        public string surname { get; set; }
        public string address { get; set; }
        public DateTime date { get; set; }
        public decimal totalPrice { get; set; }
        public IList<ReceiptItemDTO> receiptitems { get; set; }

        public ReceiptDetailDTO(int id, string name, string surname, string address, DateTime date, decimal totalPrice, IList<ReceiptItemDTO> receiptitem)
        {
            this.id = id;
            this.username = name;
            this.surname = surname;
            this.address = address;
            this.date = date;
            this.totalPrice = totalPrice;
            this.receiptitems = receiptitem;
        }

        public override bool Equals(object? obj)
        {
            return obj is ReceiptDetailDTO dTO &&
                   id == dTO.id &&
                   username == dTO.username &&
                   surname == dTO.surname &&
                   address == dTO.address &&
                   CompareDate(date, dTO.date) &&
                   totalPrice == dTO.totalPrice &&
                   receiptitems.SequenceEqual(dTO.receiptitems);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, username, surname, address, date, totalPrice, receiptitems);
        }

        protected bool CompareDate(DateTime date1, DateTime date2)
        {
            return (date1.Subtract(date2) < new TimeSpan(0, 1, 0));
        }
    }
}