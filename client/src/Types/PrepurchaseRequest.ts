import ItemOfPurchase from './ItemOfPurchase';

type PrepurchaseRequest = {
  cityId: number;
  purchaseItems: ItemOfPurchase[];
};

export default PrepurchaseRequest;
