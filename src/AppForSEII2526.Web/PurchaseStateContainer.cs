using AppForSEII2526.API.DTOs.DeviceDTOs;
using AppForSEII2526.API.DTOs.PurchaseDTOs;
using AppForSEII2526.API.DTOs.RentDTOs;
using AppForSEII2526.API.Models;
using AppForSEII2526.Web.API;


namespace AppForSEII2526.Web
{
    public class PurchaseStateContainer
    {
        //we create an instance of Purchase when an instance of PurchaseStateContainer is created


        public PurchaseForCreateDTO Purchase { get; private set; } = new PurchaseForCreateDTO()
        {


            purchaseItems = new List<PurchaseItemDTO>()


        };


        //we compute the TotalPrice of the devices we have selected for renting them


        public decimal TotalPrice
        {


            get
            {


                


                return Convert.ToDecimal(Purchase.purchaseItems.Sum(pi => pi.price * pi.quantity));


            }


        }
        public event Action? OnChange;





        private void NotifyStateChanged() => OnChange?.Invoke();


        public void AddDeviceToPurchase(DeviceForPurchaseDTO device, int Quantity, string? Description)
        {


            //before adding a device we checked whether it has been already added


            if (!Purchase.purchaseItems.Any(pi => pi.nameModel == device.nameModel))


                //we add it if it is not in the list


                Purchase.purchaseItems.Add(new PurchaseItemDTO()
                {


                    nameModel = device.nameModel,
                    brand = device.Brand,
                    colour = device.colour,
                    price = device.price,
                    quantity = Quantity, //dudas aqui supuestamente solo puedo insertar unos
                    description = Description
                                        


                }


            );





        }

        //to delete devices from the list of selected devices


        public void RemovePurchaseItemToPurchase(PurchaseItemDTO item)
        {


            Purchase.purchaseItems.Remove(item);





        }

        //we eliminate all the devices from the list


        public void ClearPurchasingCart()
        {


            Purchase.purchaseItems.Clear();





        }

        //we have already finished the process of purchasing, thus, we create a new Purchase 


        public void RentalProcessed()
        {


            //we have finished the rental process so we create a new object without data


            Purchase = new PurchaseForCreateDTO()
            {


                purchaseItems = new List<PurchaseItemDTO>()


            };


        }


    }


}

 
