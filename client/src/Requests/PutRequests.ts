import axios from 'axios';

async function put<T>(url: string, data: T) {
  return (await axios.put(url, data)).data;
}

async function updateCartItem(count: number, productId: string) {
  return await put(`/api/CustomerLists/cart/`, { count: count, productId: productId });
}

export { updateCartItem };
