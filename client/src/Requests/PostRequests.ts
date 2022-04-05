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

async function createReview(productId: string, mark: number, description: string) {
  return await post(`/api/Review/`, { productId: productId, mark: mark, description: description });
}

export { addToCart, addToWishlist, createReview };
