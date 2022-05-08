import Outlet from './Outlet';
import PurchaseItem from './PurchaseItem';

type PrepurchaseInfo = {
  purchaseItems: PurchaseItem[];
  outlets: Outlet[];
  deliveryPrice: number;
  sum: number;
};

export default PrepurchaseInfo;
