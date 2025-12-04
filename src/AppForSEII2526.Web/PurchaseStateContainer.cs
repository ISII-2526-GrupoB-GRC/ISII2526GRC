
using AppForSEII2526.Web.API;


namespace AppForSEII2526.Web
{
    public class PurchaseStateContainer
    {
        //we create an instance of Purchase when an instance of PurchaseStateContainer is created


        public PurchaseForCreateDTO Purchase { get; private set; } = new PurchaseForCreateDTO()
        {


            PurchaseItems = new List<PurchaseItemDTO>()


        };


        //we compute the TotalPrice of the devices we have selected for renting them


        public decimal TotalPrice
        {


            get
            {


                


                return Convert.ToDecimal(Purchase.PurchaseItems.Sum(pi => pi.Price * pi.Quantity));


            }


        }
        public event Action? OnChange;





        private void NotifyStateChanged() => OnChange?.Invoke();


        public void AddDeviceToPurchase(DeviceForPurchaseDTO device, int Quantity, string? Description)
        {


            //before adding a device we checked whether it has been already added


            if (!Purchase.PurchaseItems.Any(pi => pi.NameModel == device.NameModel))


                //we add it if it is not in the list


                Purchase.PurchaseItems.Add(new PurchaseItemDTO()
                {


                    NameModel = device.NameModel,
                    Brand = device.Brand,
                    Colour = device.Colour,
                    Price = device.Price,
                    Quantity = Quantity, //dudas aqui supuestamente solo puedo insertar unos
                    Description = Description
                                        


                }


            );





        }

        //to delete devices from the list of selected devices


        public void RemovePurchaseItemToPurchase(PurchaseItemDTO item)
        {


            Purchase.PurchaseItems.Remove(item);





        }

        //we eliminate all the devices from the list


        public void ClearPurchasingCart()
        {


            Purchase.PurchaseItems.Clear();





        }

        //we have already finished the process of purchasing, thus, we create a new Purchase 


        public void PurchaseProcessed()
        {


            //we have finished the rental process so we create a new object without data


            Purchase = new PurchaseForCreateDTO()
            {


                PurchaseItems = new List<PurchaseItemDTO>()


            };


        }


    }


}

 
