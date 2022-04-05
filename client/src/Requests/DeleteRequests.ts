import axios from 'axios';

async function deleteRequest<T>(url: string, data: T) {
  return (await axios.delete(url, data)).data;
}

async function deleteFromCart(productId: string) {
  return await deleteRequest(`/api/CustomerLists/cart/${productId}`, null);
}

async function deleteFromWishlist(productId: string) {
  return await deleteRequest(`/api/CustomerLists/wishlist/${productId}`, null);
}

export { deleteFromCart, deleteFromWishlist };
