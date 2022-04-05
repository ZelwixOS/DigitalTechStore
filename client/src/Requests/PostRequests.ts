import axios from 'axios';

async function post<T>(url: string, data: T) {
  return (await axios.post(url, data)).data;
}

async function addToCart(productId: string, count: number) {
  return await post('/api/CustomerLists/cart/', { productId: productId, count: count });
}

async function addToWishlist(productId: string) {
  return await post(`/api/CustomerLists/wishlist/${productId}/`, null);
}

export { addToCart, addToWishlist };