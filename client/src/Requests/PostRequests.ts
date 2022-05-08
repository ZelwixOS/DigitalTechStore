import axios from 'axios';

import ItemOfPurchase from 'src/Types/ItemOfPurchase';
import PurchaseRequest from 'src/Types/PurchaseRequest';

async function post<T>(url: string, data: T) {
  return (await axios.post(url, data)).data;
}

async function addToCart(productId: string, count: number) {
  return await post('/api/CustomerLists/cart/', { productId: productId, count: count });
}

async function addToWishlist(productId: string) {
  return await post(`/api/CustomerLists/wishlist/${productId}/`, null);
}

async function createReview(productId: string, mark: number, description: string) {
  return await post(`/api/Review/`, { productId: productId, mark: mark, description: description });
}

async function getPrepurchaseInfo(items: ItemOfPurchase[]) {
  const cityId = localStorage.getItem('cityId');
  const data = { cityId: cityId, purchaseItems: items };
  return await post(`/api/Purchase/preinfo`, data);
}

async function createPurchase(data: PurchaseRequest) {
  const cityId = localStorage.getItem('cityId');
  data.cityId = parseInt(cityId as string);
  if (data.delivery) {
    data.delivery.cityId = parseInt(cityId as string);
  }

  return await post(`/api/Purchase/`, data);
}

export { addToCart, addToWishlist, createReview, getPrepurchaseInfo, createPurchase };
