import Delivery from './Delivery';
import Outlet from './Outlet';
import PurchaseItem from './PurchaseItem';

type Purchase = {
  id: string;
  code: string;
  customerId?: string;
  sellerId?: string;
  sellerName?: string;
  customerName: string;
  status: number;
  createdDate: string;
  totalCost: number;
  delivery: Delivery;
  outlet: Outlet;
  deliveryOutlet: Outlet;
  purchaseItems: PurchaseItem[];
};

export default Purchase;
