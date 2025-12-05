using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class ReceiptStateContainer
    {
        public ReceiptForCreateDTO Receipt { get; private set; } = new ReceiptForCreateDTO()
        {
            ReceiptItems = new List<ReceiptItemDTO>()  
        };

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public double TotalCost()
        {
            double total = 0;
            foreach (var item in Receipt.ReceiptItems)
            {
                total = total + item.Cost;
            }
            return total;
        }

        public void AddReceiptItem(RepairDTO item)
        {
            //Antes de añadir un item comprobamos si ya está añadido
            if (!Receipt.ReceiptItems.Any(ri => ri.Name == item.Name))
            {
                Receipt.ReceiptItems.Add(new ReceiptItemDTO()
                {
                    Name = item.Name,
                    Scale = item.ScaleName,
                    Cost = item.Cost
                });
                NotifyStateChanged();
            }
        }

        public void RemoveReceiptItem(ReceiptItemDTO item)
        {
            Receipt.ReceiptItems.Remove(item);
        }

        public void ClearReceipt()
        {
            Receipt.ReceiptItems.Clear();
        }

        public void ReceiptProcessed()
        {
            Receipt = new ReceiptForCreateDTO()
            {
                ReceiptItems = new List<ReceiptItemDTO>()    
            };
        }
    }
}
