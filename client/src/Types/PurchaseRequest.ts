import CustomerInfo from './CustomerInfo';
import DeliveryRequest from './DeliveryRequest';
import ItemOfPurchase from './ItemOfPurchase';

type PurchaseRequest = CustomerInfo & {
  deliveryOutletId?: number;
  cityId: number;
  purchaseItems: ItemOfPurchase[];
  delivery?: DeliveryRequest;
};

export default PurchaseRequest;
