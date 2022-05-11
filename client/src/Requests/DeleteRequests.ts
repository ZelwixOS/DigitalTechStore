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

async function deleteCommonCategory(id: string) {
  return await deleteRequest(`/api/CommonCategory/${id}`, null);
}

async function deleteCategory(id: string) {
  return await deleteRequest(`/api/Category/${id}`, null);
}

async function deleteParameterBlock(id: string) {
  return await deleteRequest(`/api/ParameterBlock/${id}`, null);
}

async function deleteParameter(id: string) {
  return await deleteRequest(`/api/TechParameter/${id}`, null);
}

async function deleteParameterValue(id: string) {
  return await deleteRequest(`/api/ParameterValue/${id}`, null);
}

export {
  deleteFromCart,
  deleteFromWishlist,
  deleteCommonCategory,
  deleteCategory,
  deleteParameterBlock,
  deleteParameter,
  deleteParameterValue,
};
